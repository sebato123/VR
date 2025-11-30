using UnityEngine;

public class DestructibleEnemy : MonoBehaviour
{
    [Header("Física de ruptura")]
    public float explosionForce = 300f;
    public float explosionRadius = 2f;
    public float upwardsModifier = 0.2f;
    public bool destroyAfterBreak = false;
    public float destroyDelay = 5f;

    private bool broken = false;
    private Rigidbody[] pieceRigidbodies;

    private void Awake()
    {
        // Tomar todos los rigidbodies hijos
        pieceRigidbodies = GetComponentsInChildren<Rigidbody>();

        // Empezar inmóviles / kinematic
        foreach (Rigidbody rb in pieceRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    public void Break(Vector3 hitPoint)
    {
        if (broken) return;
        broken = true;

        foreach (Rigidbody rb in pieceRigidbodies)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(explosionForce, hitPoint, explosionRadius, upwardsModifier, ForceMode.Impulse);
        }

        if (destroyAfterBreak)
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
