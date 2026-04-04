using UnityEngine;

public class Faceball : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.Reflect(rb.linearVelocity, collision.contacts[0].normal);
    }
}
