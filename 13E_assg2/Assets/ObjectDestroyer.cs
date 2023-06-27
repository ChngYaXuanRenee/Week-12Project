using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    //public string objectTagToDestory = "Destroyable";
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp (0))
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));
            Vector3 direction = worldMousePosition - Camera.main.transform.position;

            RaycastHit hit;

            if (Physics.Raycast (Camera.main.transform.position, direction, out hit, 100f))
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green, 0.5f);
                if (hit.collider.CompareTag("Destroyable"))
                {
                    Destroy(hit.collider.gameObject);
                }
                
            }
            else
            {
                Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.red, 0.5f);
            }
        }
    }
}
