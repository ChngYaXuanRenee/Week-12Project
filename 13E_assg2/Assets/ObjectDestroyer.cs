using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public AudioClip destroySoundClip; // Sound clip to play when an object is destroyed
    private AudioSource audioSource;
    private KeyCounterUI keyCounterUI;
    private MyDoorController raycastObj;

    private void Start()
    {
        // Create an empty game object as a child of the main camera
        GameObject audioObject = new GameObject("DestroyAudio");
        audioObject.transform.parent = Camera.main.transform;

        // Add an AudioSource component to the audio object
        audioSource = audioObject.AddComponent<AudioSource>();

        // Find the KeyCounterUI script component
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
                }
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100f, Color.red, 0.5f);
            }
        }
    }
}
