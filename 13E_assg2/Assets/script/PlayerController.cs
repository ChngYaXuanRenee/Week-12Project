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
    //public GameObject head;
    //int score = 0;

    //private void OnCollisionEnter(Collision collision)
    //{
    //if (collision.gameObject.tag == "collectables")
    //{
    //Debug.Log("Enter : " + collision.gameObject.name);
    //Destroy(collision.gameObject);
    //}
    //}

    void OnLook(InputValue value)
    {
        rotationInput = value.Get<Vector2>();
    }

    void OnMove(InputValue value)
    {
        moveData = value.Get<Vector2>();

        Debug.Log(moveData);

    }

    public AudioSource bgm;
    public bool playing = false;

    void onFire()
    {
        Debug.Log(transform.position);
        Debug.Log(transform.rotation);
        Debug.Log(transform.localScale);

        transform.position += new Vector3(2f, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Assign the background music audio clip to the background audio source
        backgroundAudioSource.clip = backgroundMusic;

        // Assign the footstep sound effect audio clip to the footsteps audio source
        footstepsAudioSource.clip = footstepSound;

        // Play the background music
        backgroundAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotationInput.x) * rotationSpeed);
        head.rotation = Quaternion.Euler(head.rotation.eulerAngles + new Vector3(-rotationInput.y, 0) * rotationSpeed);

        Vector3 forwardDir = transform.forward;
        Vector3 rightDir = transform.right;

        var moveForward = forwardDir * moveData.y;
        var moveRight = rightDir * moveData.x;

        GetComponent<Rigidbody>().MovePosition(transform.position + (moveForward + moveRight) * moveSpeed);


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
    }

    void OnJump()
    {
        //jump 
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
    }
}
