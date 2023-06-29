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

            if (gunObject != null)
            {
                Destroy(gunObject);
            }
        }
    }
}
