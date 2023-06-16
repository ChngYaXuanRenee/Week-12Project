using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Animator playerAnimator;

    private bool isAnimationPlaying = false;

    private void Start()
    {
        // Disable animation at the start
        playerAnimator.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isAnimationPlaying)
        {
            // Disable player movement script or other relevant scripts

            // Play death animation
            playerAnimator.enabled = true;
            playerAnimator.SetTrigger("Death");
            isAnimationPlaying = true;

            // Perform other actions when the player dies
            // (e.g., game over screen, reset level, etc.)
        }
    }
}
