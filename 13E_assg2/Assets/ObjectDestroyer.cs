using System.Collections;
using UnityEngine;
using TMPro;

public class ObjectDestroyer : MonoBehaviour
{
    public event System.Action OnGunCollected;

    public AudioClip destroySoundClip;
    private AudioSource audioSource;
    private KeyCounterUI keyCounterUI;
    private MyDoorController raycastObj;

    public AudioClip gunCollectedSoundClip;
    public TextMeshProUGUI messageText;
    private bool gunCollected;

    public LoadingBar loadingBar;

    // Potion counts
    private int greenPotionCount;
    private int yellowPotionCount;
    private int pinkPotionCount;
    public TextMeshProUGUI greenPotionText;
    public TextMeshProUGUI yellowPotionText;
    public TextMeshProUGUI pinkPotionText;

    private bool interactiveObjectActivated = false; // Flag to track if the interactive object has been activated

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        keyCounterUI = FindObjectOfType<KeyCounterUI>();
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
                    if (destroySoundClip != null)
                    {
                        audioSource.PlayOneShot(destroySoundClip);
                    }

                    Destroy(hit.collider.gameObject);

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
                    ActivateInteractiveObject(); // Activate the interactive object
                }
                else if (hit.collider.CompareTag("Gun"))
                {
                    if (interactiveObjectActivated) // Check if the interactive object has been activated
                    {
                        CollectGun(hit.collider.gameObject);
                    }
                }
                else if (hit.collider.CompareTag("GreenPotion") || hit.collider.CompareTag("YellowPotion") || hit.collider.CompareTag("PinkPotion"))
                {
                    if (interactiveObjectActivated) // Check if the interactive object has been activated
                    {
                        CollectPotion(hit.collider.gameObject);
                    }
                }
                else
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100f, Color.red, 0.5f);
                }
            }
        }
    }

    private void UpdatePotionCountUI(TextMeshProUGUI potionText, string potionType, int count)
    {
        if (potionText != null)
        {
            // Update the UI text with the potion type and count
            potionText.text = potionType + ": " + count.ToString();
        }
    }

    private void CollectPotion(GameObject potionObject)
    {
        if (interactiveObjectActivated)
        {
            // Only allow potion collection if the interactive object has been activated
            if (potionObject.CompareTag("GreenPotion"))
            {
                greenPotionCount++;
                UpdatePotionCountUI(greenPotionText, "Green potion", greenPotionCount);
            }
            else if (potionObject.CompareTag("YellowPotion"))
            {
                yellowPotionCount++;
                UpdatePotionCountUI(yellowPotionText, "Yellow potion", yellowPotionCount);
            }
            else if (potionObject.CompareTag("PinkPotion"))
            {
                pinkPotionCount++;
                UpdatePotionCountUI(pinkPotionText, "Pink potion", pinkPotionCount);
            }

            Destroy(potionObject);
        }
    }

    private void CollectGun(GameObject gunObject)
    {
        if (interactiveObjectActivated && !gunCollected)
        {
            gunCollected = true;

            if (gunCollectedSoundClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(gunCollectedSoundClip);
            }

            if (messageText != null)
            {
                messageText.text = "Gun Collected";
            }

            if (loadingBar != null)
            {
                OnGunCollected?.Invoke();
            }

            Destroy(gunObject);
        }
    }

    private void UpdatePotionCountUI(TextMeshProUGUI potionText, int count)
    {
        if (potionText != null)
        {
            // Update the UI text with the potion type and count
            potionText.text = count.ToString();
        }
    }

    public void ActivateInteractiveObject()
    {
        interactiveObjectActivated = true;
        // Perform any additional actions when the interactive object is activated
    }
}
