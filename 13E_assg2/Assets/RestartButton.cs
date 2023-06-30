using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public GameObject restartObject;  // Reference to the UI canvas to be shown on restart

    public void RestartGame()
    {
        // Add any additional logic you need before restarting the game,
        // such as resetting game state or player progress.

        // Enable the restart canvas
        restartObject.SetActive(true);
        Debug.Log("its been clicked");

        // Disable other UI canvases if necessary
        // canvas1.gameObject.SetActive(false);
        // canvas2.gameObject.SetActive(false);
    }
}
