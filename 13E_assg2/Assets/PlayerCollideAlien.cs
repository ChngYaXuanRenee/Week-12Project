using UnityEngine;

public class PlayerCollideAlien : MonoBehaviour
{
    private Health playerHealth;  // Reference to the player's health component

    private void Start()
    {
        // Get the player's health component
        playerHealth = GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alien"))
        {
            // Access the alien's damage amount
            Alien alien = other.GetComponent<Alien>();
            if (alien != null)
            {
                // Inflict damage on the player
                playerHealth.TakeDamage(alien.damageAmount);
            }
        }
    }
}
