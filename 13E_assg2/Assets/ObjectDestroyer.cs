using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectDestroyer : MonoBehaviour
{
    public AudioClip destroySoundClip; // Sound clip to play when an object is destroyed
    private AudioSource audioSource;
    private KeyCounterUI keyCounterUI;
    private MyDoorController raycastObj;

    // FOR GUN
    public AudioClip gunCollectedSoundClip;
    public TextMeshProUGUI messageText;
    private bool gunCollected; // Corrected variable declaration

    //FADE TO BLACK
    public float fadeDuration = 1f;
    private float currentDuration = 0f;
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        // Get the AudioSource component attached to this game object
        audioSource = GetComponent<AudioSource>();

        // Find the KeyCounterUI script component
        keyCounterUI = FindObjectOfType<KeyCounterUI>();

        // Find the "FadeEffect" GameObject and assign its Image component to the fadeImage variable
        GameObject fadeEffectObject = GameObject.Find("FadeEffect");
        if (fadeEffectObject != null)
        {
            fadeImage = fadeEffectObject.GetComponent<Image>();
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green, 0.5f);

                if (hit.collider.CompareTag("Destroyable"))
                {
                    // Play the destroy sound clip
                    if (destroySoundClip != null)
                    {
                        audioSource.PlayOneShot(destroySoundClip);
                    }

                    Destroy(hit.collider.gameObject);

                    // Increment key count if KeyCounterUI script is available
                    if (keyCounterUI != null)
                    {
                        keyCounterUI.CollectKey();
                    }
                }
                else if (hit.collider.CompareTag("InteractiveObject"))
                {
                    if (!raycastObj)
                    {
                        raycastObj = hit.collider.gameObject.GetComponent<MyDoorController>();
                    }

                    raycastObj.PlayAnimation();

                    // GUN
                    if (hit.collider.name == "Gun")
                    {
                        CollectGun(hit.collider.gameObject);
                    }
                }
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100f, Color.red, 0.5f);
            }
        }
    }

    private void CollectGun(GameObject gunObject)
    {
        if (!gunCollected)
        {
            gunCollected = true;

            // Play the gun collected sound clip using the audio source attached to this game object
            if (gunCollectedSoundClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(gunCollectedSoundClip);
            }

            // Update the UI message
            if (messageText != null)
            {
                messageText.text = "Gun Collected";
            }

            // Destroy the gun object and fade effect
            FadeToBlack fadeToBlack = fadeImage.GetComponent<FadeToBlack>();
            fadeToBlack.enabled = true;
            fadeToBlack.fadeDuration = fadeDuration;

            // Destroy the gun object after the fade effect completes
            Destroy(gunObject, fadeDuration);
        }
    }
}
