using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRaycast : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    private MyDoorController raycastObj;

    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;

    private const string interactableTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!raycastObj)
                {
                    raycastObj = hit.collider.gameObject.GetComponent<MyDoorController>();
                }

                if (Input.GetKeyDown(openDoorKey))
                {
                    raycastObj.PlayAnimation();
                }
            }
        }
    }
}

