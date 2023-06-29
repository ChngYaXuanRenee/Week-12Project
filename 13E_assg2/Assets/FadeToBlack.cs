using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public float fadeDuration = 1f; // Duration of the fade effect in seconds
    private float currentDuration = 0f; // Current duration of the fade effect
    private Image image; // Reference to the image component

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (currentDuration < fadeDuration)
        {
            currentDuration += Time.deltaTime;
            float fillAmount = Mathf.Clamp01(currentDuration / fadeDuration);
            image.fillAmount = fillAmount;
        }
    }
}
