using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health value
    public int currentHealth;   // Current health value
    public TextMeshProUGUI healthText;     // Reference to the UI text element for displaying health
    public Animator deathAnimator;  // Reference to the death animator component

    private bool isGameOver = false;  // Flag to track game over state

    private void Start()
    {
        currentHealth = maxHealth;  // Set the initial health to the maximum value
        UpdateHealthText();         // Update the UI text to display the initial health

        // Disable the death animator if the initial health is at its maximum value
        if (currentHealth == maxHealth)
        {
            if (deathAnimator != null)
            {
                deathAnimator.enabled = false;
            }
        }
    }


    private void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString();  // Update the UI text with the current health value
    }

    public void TakeDamage(int damageAmount)
    {
        // Check if the game is already over
        if (isGameOver)
            return;

        currentHealth -= damageAmount;  // Decrease the current health by the damage amount

        // Check if the health value is below zero
        if (currentHealth <= 0)
        {
            currentHealth = 0;  // Clamp the health value to zero to prevent negative values
                                // Implement any additional logic for player death or game over

            //GameOver();
        }

        UpdateHealthText();  // Update the UI text to display the updated health value

        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth);

        // Check if the health value is exactly 80
        if (currentHealth == 0)
        {
            if (deathAnimator != null)
            {
                deathAnimator.enabled = true;
                deathAnimator.SetTrigger("Die");
            }

            // Implement any additional logic for player death or game over

            //GameOver();
        }


    }
}
