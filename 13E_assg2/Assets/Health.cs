using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health value
    public int currentHealth;   // Current health value
    public Text healthText;     // Reference to the UI text element for displaying health

    private void Start()
    {
        currentHealth = maxHealth;  // Set the initial health to the maximum value
        UpdateHealthText();         // Update the UI text to display the initial health
    }

    private void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString();  // Update the UI text with the current health value
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;  // Decrease the current health by the damage amount

        // Check if the health value is below zero
        if (currentHealth <= 0)
        {
            currentHealth = 0;  // Clamp the health value to zero to prevent negative values
            // Implement any additional logic for player death or game over

            // Example: Call a GameOver function on a GameManager script
            GameManager.instance.GameOver();
        }

        UpdateHealthText();  // Update the UI text to display the updated health value
    }
}
