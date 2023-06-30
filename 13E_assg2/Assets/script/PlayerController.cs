using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Vector2 rotationInput = Vector2.zero;
    Vector2 moveData = Vector2.zero;

    float rotationSpeed = 1f;
    float moveSpeed = 0.19f;

    public TextMeshProUGUI displayText;
    public GameObject obj;
    public Transform head;

    public AudioSource backgroundAudioSource; // Reference to the AudioSource component for background audio
    public AudioSource footstepsAudioSource; // Reference to the AudioSource component for footsteps
    public AudioClip backgroundMusic; // Background music audio clip
    public AudioClip footstepSound; // Footstep sound effect audio clip

    public int requiredKeys = 3;
    public AudioClip doorUnlockSound;
    public GameObject door;

    private int collectedKeys = 0;
    private bool isDoorUnlocked = false;

    private float speedMultiplier = 1f; // Default speed multiplier

    public SpeedBoostController speedBoostController; // Reference to the SpeedBoostController component

    public float GetSpeedMultiplier()
    {
        return speedMultiplier;
    }

    private void Start()
    {
        // Assign the background music audio clip to the background audio source
        backgroundAudioSource.clip = backgroundMusic;

        // Assign the footstep sound effect audio clip to the footsteps audio source
        footstepsAudioSource.clip = footstepSound;

        // Play the background music
        backgroundAudioSource.Play();

        // Get the SpeedBoostController component
        speedBoostController = GetComponent<SpeedBoostController>();
    }

    private void CollectSpeed(GameObject speedObject)
    {
        SpeedBoostController speedBoostController = speedObject.GetComponent<SpeedBoostController>();

        if (speedBoostController != null)
        {
            speedBoostController.ApplySpeedBoost();
        }

        Destroy(speedObject);
    }


    private void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotationInput.x) * rotationSpeed);
        head.rotation = Quaternion.Euler(head.rotation.eulerAngles + new Vector3(-rotationInput.y, 0) * rotationSpeed);

        Vector3 forwardDir = transform.forward;
        Vector3 rightDir = transform.right;

        var moveForward = forwardDir * moveData.y;
        var moveRight = rightDir * moveData.x;

        GetComponent<Rigidbody>().MovePosition(transform.position + (moveForward + moveRight) * (moveSpeed * speedMultiplier)); // Multiply moveSpeed by speedMultiplier

        // Play footstep sounds when moving
        if (moveData.magnitude > 0)
        {
            if (!footstepsAudioSource.isPlaying)
            {
                footstepsAudioSource.Play();
            }
        }
        else
        {
            if (footstepsAudioSource.isPlaying)
            {
                footstepsAudioSource.Stop();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name); // Check if the raycast hits the key object

                if (hit.collider.CompareTag("Key"))
                {
                    Key key = hit.collider.GetComponent<Key>();
                    if (key != null)
                    {
                        key.Collect();
                        CollectKey();
                    }
                }
                else if (hit.collider.CompareTag("Speed"))
                {
                    if (speedBoostController != null)
                    {
                        speedBoostController.ApplySpeedBoost();
                    }
                }
            }
        }
    }

    public void CollectKey()
    {
        collectedKeys++;
        Debug.Log("Key collected. Total keys: " + collectedKeys);

        if (collectedKeys >= requiredKeys)
        {
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        isDoorUnlocked = true;
        Debug.Log("Door unlocked!");

        // Play door unlocking sound
        AudioSource.PlayClipAtPoint(doorUnlockSound, door.transform.position);

        // Open the door
        door.SetActive(false);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void ResetSpeedMultiplier()
    {
        speedMultiplier = 1f; // Reset the speed multiplier to its default value
    }

    void OnLook(InputValue value)
    {
        rotationInput = value.Get<Vector2>();
    }

    void OnMove(InputValue value)
    {
        moveData = value.Get<Vector2>();

        Debug.Log(moveData);
    }

    void OnJump()
    {
        // Jump
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
    }
}
