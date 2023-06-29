using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image currentImage;
    public Image nextImage;
    public Image thirdImage;
    public float transitionDuration = 2f;

    private bool isTransitioning = false; // Added flag to track if transition is already in progress

    private void Start()
    {
        // Ensure the nextImage and thirdImage are disabled at the start
        currentImage.gameObject.SetActive(true);
        nextImage.gameObject.SetActive(false);
        thirdImage.gameObject.SetActive(false);
    }

    public void TransitionToNextImage()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionCoroutine());
        }
    }

    private IEnumerator TransitionCoroutine()
    {
        isTransitioning = true; // Set flag to indicate transition is in progress

        // Crossfade from currentImage to nextImage
        nextImage.gameObject.SetActive(true);
        nextImage.canvasRenderer.SetAlpha(0f);
        currentImage.CrossFadeAlpha(0f, transitionDuration, false);
        nextImage.CrossFadeAlpha(1f, transitionDuration, false);

        yield return new WaitForSeconds(transitionDuration);

        // Crossfade from nextImage to thirdImage
        thirdImage.gameObject.SetActive(true);
        thirdImage.canvasRenderer.SetAlpha(0f);
        nextImage.CrossFadeAlpha(0f, transitionDuration, false);
        thirdImage.CrossFadeAlpha(1f, transitionDuration, false);

        yield return new WaitForSeconds(transitionDuration);

        // Disable the currentImage and nextImage, and set their alpha to 1
        currentImage.gameObject.SetActive(false);
        currentImage.canvasRenderer.SetAlpha(1f);
        nextImage.gameObject.SetActive(false);
        nextImage.canvasRenderer.SetAlpha(1f);

        isTransitioning = false; // Reset flag to indicate transition is complete
    }
}
