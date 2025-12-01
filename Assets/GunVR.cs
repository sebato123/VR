using UnityEngine;

public class GunVR : MonoBehaviour
{
    [Header("Disparo")]
    public Transform shootingPoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40f;
    public float projectileLifeTime = 5f;

    public bool weaponActive = true;

    void Update()
    {
        // Debug visual de hacia dónde apunta de verdad
        if (shootingPoint != null)
        {
            Debug.DrawRay(shootingPoint.position, shootingPoint.forward * 0.5f, Color.red);
        }

        // Aquí iría el input del gatillo para llamar Fire()
    }

    public void Fire()
    {
        if (!weaponActive) return;

        if (projectilePrefab == null || shootingPoint == null)
        {
            Debug.LogWarning("Falta projectilePrefab o shootingPoint en GunVR.");
            return;
        }

        // Instanciar bala
        GameObject bullet = Instantiate(
            projectilePrefab,
            shootingPoint.position,
            shootingPoint.rotation
        );

        // Darle velocidad en la dirección del cañón
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootingPoint.forward * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("El proyectil necesita un Rigidbody.");
        }

        Destroy(bullet, projectileLifeTime);
    }
}
