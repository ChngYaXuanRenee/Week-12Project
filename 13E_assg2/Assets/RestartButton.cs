using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestartButton : MonoBehaviour
{
    public Health playerHealth;
    public Animator playerAnimator;
    public TextMeshProUGUI keyCounterUI;
    public Button restartButton;
    public Canvas restartCanvas;

    public int desiredHealth = 100; // Set the desired health value here

    public void OnRestartButtonClicked()
    {
        // Disable the restart canvas
        restartCanvas.enabled = false;

        // Set the player's health to the desired value
        playerHealth.currentHealth = desiredHealth;

        // Stop player animation
        playerAnimator.enabled = false;

        // Update key counter UI text
        keyCounterUI.text = "Keys: 3/3";

        // Disable the restart button UI
        restartButton.interactable = false;
    }
}