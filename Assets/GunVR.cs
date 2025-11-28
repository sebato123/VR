using UnityEngine;

public class GunVR : MonoBehaviour
{
    [Header("Disparo")]
    public Transform shootingPoint;          // Punto de salida del proyectil y del láser
    public GameObject projectilePrefab;      // Prefab del proyectil
    public float projectileSpeed = 50f;      // Velocidad del proyectil
    public float projectileLifeTime = 5f;    // Tiempo antes de destruir el proyectil

    [Header("Láser")]
    public LineRenderer lineRenderer;        // LineRenderer del láser
    public float laserMaxDistance = 50f;     // Distancia máxima del láser
    public LayerMask laserCollisionMask;     // Capas con las que el láser puede chocar

    [Header("General")]
    public bool weaponActive = true;         // Activar/desactivar arma/láser

    void Update()
    {
        // Actualizamos el láser constantemente mientras el arma esté activa
        if (weaponActive)
        {
            UpdateLaser();
        }
        else if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    public void Fire()
    {
        if (!weaponActive || projectilePrefab == null || shootingPoint == null)
            return;

        // Crear el proyectil en el ShootingPoint, con la misma rotación
        GameObject projectileInstance = Instantiate(
            projectilePrefab,
            shootingPoint.position,
            shootingPoint.rotation
        );

        // Darle dirección y velocidad al proyectil
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootingPoint.forward * projectileSpeed;
        }

        // Destruir el proyectil después de X segundos para no llenar la escena
        Destroy(projectileInstance, projectileLifeTime);
    }

    private void UpdateLaser()
    {
        if (lineRenderer == null || shootingPoint == null)
            return;

        lineRenderer.enabled = true;

        Vector3 origin = shootingPoint.position;
        Vector3 direction = shootingPoint.forward;

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        Vector3 endPoint;

        // Si el láser choca con algo en la máscara
        if (Physics.Raycast(ray, out hit, laserMaxDistance, laserCollisionMask))
        {
            endPoint = hit.point;
        }
        else
        {
            // No chocó nada: el láser se corta a la distancia máxima
            endPoint = origin + direction * laserMaxDistance;
        }

        // LineRenderer en espacio mundial
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, endPoint);
    }
}
