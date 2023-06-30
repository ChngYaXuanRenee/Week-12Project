using UnityEngine;

public class Alien : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float moveSpeed = 5f;  // Speed at which the alien moves towards the player
    public int damageAmount = 10; // Amount of damage inflicted on the player

    private void Update()
    {
        // Calculate the direction from the alien to the player
        Vector3 direction = player.position - transform.position;
        direction.Normalize();  // Normalize the direction to get a unit vector

        // Move the alien towards the player
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Access the player's health component and inflict damage
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            // Destroy the alien object after colliding with the player
            Destroy(gameObject);
        }
    }
}
