
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Vector3 rotationInput = Vector3.zero;
    Vector3 moveData = Vector3.zero;

    float rotationSpeed = 1f;
    float moveSpeed = 0.19f;

    public TextMeshProUGUI displayText;
    public GameObject obj;
    public Transform head;
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
        rotationInput.y = value.Get<Vector2>().x;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotationInput.y) * rotationSpeed);
        head.rotation = Quaternion.Euler(head.rotation.eulerAngles + new Vector3(rotationInput.x, 0) * rotationSpeed);
        //headTransform.rotation = Quaternion.Euler(headTransform.rotation.eulerAngles + new Vector3(rotationInput.x, 0) * rotationSpeed);
        //headGameObject.transform.rotation = Quaternion.Euler(headGameObject.transform.rotation.eulerAngles + new Vector3(rotationInput.x, 0) * rotationSpeed);
        // transform.rotation =
        //  Quaternion.Euler(transform.rotation.eulerAngles + rotationInput * rotationSpeed);


        Vector3 forwardDir = transform.forward;
        Vector3 rightDir = transform.right;

        var moveForward = forwardDir * moveData.y;
        var moveRight = forwardDir * moveData.x;

        GetComponent<Rigidbody>().MovePosition(transform.position
            + (moveForward + moveRight) * moveSpeed);



        transform.position +=
       (forwardDir * moveData.y
        + rightDir * moveData.x)
        * moveSpeed;

    }
    void OnJump()
    {
        //jump 
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
    }
}