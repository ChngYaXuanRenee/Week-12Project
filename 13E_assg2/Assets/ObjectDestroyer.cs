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

                    if (hit.collider.CompareTag("GreenPotion") || hit.collider.CompareTag("YellowPotion") || hit.collider.CompareTag("PinkPotion"))
                    {
                        CollectPotion(hit.collider.gameObject);
                    }
                    else if (hit.collider.CompareTag("Gun"))
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

    private void CollectPotion(GameObject potionObject)
    {
        // Check the type of the collected potion
        if (potionObject.CompareTag("GreenPotion"))
        {
            greenPotionCount++;
            UpdatePotionCountUI("Green potion:", greenPotionCount);
        }
        else if (potionObject.CompareTag("YellowPotion"))
        {
            yellowPotionCount++;
            UpdatePotionCountUI("Yellow potion:", yellowPotionCount);
        }
        else if (potionObject.CompareTag("PinkPotion"))
        {
            pinkPotionCount++;
            UpdatePotionCountUI("Pink potion:", pinkPotionCount);
        }

        Destroy(potionObject);
    }

    private void CollectGun(GameObject gunObject)
    {
        if (!gunCollected)
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

    private void UpdatePotionCountUI(string potionType, int count)
    {
        if (messageText != null)
        {
            // Update the UI text with the potion type and count
            messageText.text = potionType + " " + count.ToString();
        }
    }
}
