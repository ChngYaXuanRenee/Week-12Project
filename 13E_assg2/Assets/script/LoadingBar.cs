using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image currentImage;
    public Image nextImage;
    public Image thirdImage;
    public float transitionDuration = 4f;

    private void Start()
    {
        // Disable all images at the start
        currentImage.gameObject.SetActive(false);
        nextImage.gameObject.SetActive(false);
        thirdImage.gameObject.SetActive(false);

        // Find the ObjectDestroyer script component
        ObjectDestroyer objectDestroyer = FindObjectOfType<ObjectDestroyer>();

        // Subscribe to the gun collected event
        if (objectDestroyer != null && objectDestroyer.loadingBar == this)
        {
            objectDestroyer.OnGunCollected += HandleGunCollected;
        }
    }

    private void HandleGunCollected()
    {
        StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        // Enable the currentImage and set its alpha to 0
        currentImage.gameObject.SetActive(true);
        currentImage.canvasRenderer.SetAlpha(0f);

        // Crossfade the images over the specified duration
        nextImage.CrossFadeAlpha(0f, transitionDuration, false);
        thirdImage.CrossFadeAlpha(0f, transitionDuration, false);
        currentImage.CrossFadeAlpha(1f, transitionDuration, false);

        // Wait for the transition to complete
        yield return new WaitForSeconds(transitionDuration);

        // Disable the nextImage and thirdImage
        nextImage.gameObject.SetActive(false);
        thirdImage.gameObject.SetActive(false);

        // Enable the nextImage and set its alpha to 0
        nextImage.gameObject.SetActive(true);
        nextImage.canvasRenderer.SetAlpha(0f);

        // Crossfade the images over the specified duration
        currentImage.CrossFadeAlpha(0f, transitionDuration, false);
        nextImage.CrossFadeAlpha(1f, transitionDuration, false);

        // Wait for the transition to complete
        yield return new WaitForSeconds(transitionDuration);

        // Disable the currentImage
        currentImage.gameObject.SetActive(false);

        // Enable the thirdImage and set its alpha to 0
        thirdImage.gameObject.SetActive(true);
        thirdImage.canvasRenderer.SetAlpha(0f);

        // Crossfade the images over the specified duration
        nextImage.CrossFadeAlpha(0f, transitionDuration, false);
        thirdImage.CrossFadeAlpha(1f, transitionDuration, false);

        // Wait for the transition to complete
        yield return new WaitForSeconds(transitionDuration);

        // Disable the nextImage
        nextImage.gameObject.SetActive(false);

        // Destroy the thirdImage
        Destroy(thirdImage.gameObject);
    }
}
