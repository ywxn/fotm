using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BulletController : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    public float despawnDistance = 200f; // Distance at which the bullet should despawn
    private Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(speed * Time.deltaTime * Vector3.forward);

        if (Vector3.Distance(transform.position, spawnPoint) > despawnDistance)
        {
            // Destroy the bullet if it has traveled beyond the despawn distance
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Check if the collider is not null and if the bullet has hit a shootable object
        if (other != null)
        {
            // Check if the collision is with a player object (adjust "Player" layer name accordingly)
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                // Ignore collisions with the player
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(gameObject);
            }


            // Destroy the bullet in any case
            Destroy(gameObject);
        }
    }
}
