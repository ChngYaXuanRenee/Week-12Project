using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image currentImage;
    public Image nextImage;
    public Image thirdImage;
    public float transitionDuration = 2f;

    private void Start()
    {

        // Ensure the nextImage and thirdImage are disabled at the start
        currentImage.gameObject.SetActive(false);
        nextImage.gameObject.SetActive(false);
        thirdImage.gameObject.SetActive(false);

        // Trigger the loading bar transition after 2 seconds (for demonstration purposes)
        TransitionToNextImage();
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

        // Enable the thirdImage and set its alpha to 0
        thirdImage.gameObject.SetActive(true);
        thirdImage.canvasRenderer.SetAlpha(0f);

        // Crossfade the images over the specified duration
        nextImage.CrossFadeAlpha(0f, transitionDuration, false);
        thirdImage.CrossFadeAlpha(1f, transitionDuration, false);

        // Wait for the transition to complete
        yield return new WaitForSeconds(transitionDuration);

        // Disable the nextImage and set its alpha to 1
        nextImage.gameObject.SetActive(false);
        nextImage.canvasRenderer.SetAlpha(1f);
    }
}
