using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Daño / Comportamiento")]
    public float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        // Aquí puedes manejar daño, efectos, etc.
        // Ejemplo básico:
        // if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     collision.gameObject.GetComponent<Health>()?.TakeDamage(damage);
        // }

        // Destruir el proyectil al chocar con algo
        Destroy(gameObject);
    }
}
