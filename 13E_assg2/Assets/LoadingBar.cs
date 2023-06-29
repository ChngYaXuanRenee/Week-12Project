using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image currentImage;
    public Image nextImage;
    public float transitionDuration = 2f;

    private void Start()
    {
        // Ensure the nextImage is disabled at the start
        nextImage.gameObject.SetActive(false);
    }

    public void TransitionToNextImage()
    {
        StartCoroutine(TransitionCoroutine());
    }

    private System.Collections.IEnumerator TransitionCoroutine()
    {
        // Enable the nextImage and set its alpha to 0
        nextImage.gameObject.SetActive(true);
        nextImage.canvasRenderer.SetAlpha(0f);

        // Crossfade the images over the specified duration
        currentImage.CrossFadeAlpha(0f, transitionDuration, false);
        nextImage.CrossFadeAlpha(1f, transitionDuration, false);

        // Wait for the transition to complete
        yield return new WaitForSeconds(transitionDuration);

        // Disable the currentImage and set its alpha to 1
        currentImage.gameObject.SetActive(false);
        currentImage.canvasRenderer.SetAlpha(1f);
    }
}
