using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private bool isPaused = false;

    public void OnPauseButtonClicked()
    {
        if (isPaused)
        {
            Time.timeScale = 1f; // Resume the game
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f; // Pause the game
            isPaused = true;
        }
    }
}
