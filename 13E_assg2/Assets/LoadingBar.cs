using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image currentImage;
    public Image nextImage;
    public float transitionDuration = 10f; // Increase the transition duration for a slower transition

    private void Start()
    {
        nextImage.gameObject.SetActive(false);
    }

    public void TransitionToNextImage()
    {
        StartCoroutine(TransitionCoroutine());
    }

    private System.Collections.IEnumerator TransitionCoroutine()
    {
        nextImage.gameObject.SetActive(true);
        nextImage.canvasRenderer.SetAlpha(0f);

        currentImage.CrossFadeAlpha(0f, transitionDuration, false);
        nextImage.CrossFadeAlpha(1f, transitionDuration, false);

        yield return new WaitForSeconds(transitionDuration);

        currentImage.gameObject.SetActive(false);
        currentImage.canvasRenderer.SetAlpha(1f);
    }
}
