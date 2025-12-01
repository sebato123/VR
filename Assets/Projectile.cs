using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [Header("Daño / Comportamiento")]
    public float damage = 10f;
    private Collider col;

    private void Awake()
    {
      
        col = GetComponent<Collider>();

        // Asegurar buena detección de colisiones
       
        
        col.isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile hit: " + collision.gameObject.name);

        // Buscar un enemigo destructible en el objeto golpeado o sus padres
        DestructibleEnemy destructible = collision.collider.GetComponentInParent<DestructibleEnemy>();
        if (destructible != null)
        {
            Vector3 hitPoint = collision.contacts.Length > 0
                ? collision.contacts[0].point
                : transform.position;

            destructible.Break(hitPoint);
        }

        // Destruir el proyectil al chocar con algo
        //Destroy(gameObject);
    }
}
