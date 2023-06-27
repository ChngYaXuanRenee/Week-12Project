using UnityEngine;

public class DoorInteractions : MonoBehaviour
{
    public LayerMask doorLayer; // Layer mask to specify the door layer
    private DoorAnimation doorAnimation; // Reference to the DoorAnimation script

    private void Start()
    {
        doorAnimation = GetComponent<DoorAnimation>(); // Get the DoorAnimation component
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, doorLayer))
            {
                // Check if the hit object has the DoorAnimation script attached
                DoorAnimation door = hit.collider.GetComponent<DoorAnimation>();
                if (door != null)
                {
                    // Call the TriggerDoorAnimation method on the DoorAnimation script
                    door.TriggerDoorAnimation();
                }
            }
        }
    }
}
