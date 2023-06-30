using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Health playerHealth;
    public Animator playerAnimation;
    public KeyCounterUI keyCounterUI;
    public Button restartButton;

    public void OnRestartButtonClicked()
    {
        // Reset player health
        playerHealth.SetHealth(100);

        // Stop player animation
        playerAnimation.StopAnimation();

        // Update key counter to 3
        keyCounterUI.SetKeyCount(3);

        // Disable the restart button UI
        restartButton.interactable = false;
    }
}
