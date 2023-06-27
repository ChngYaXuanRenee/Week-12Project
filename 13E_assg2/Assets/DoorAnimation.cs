using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : StateMachineBehaviour
{
    private Animator animator; // Reference to the Animator component

    // This method is called when the script is loaded or the component is added to a game object
    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    public void TriggerDoorAnimation()
    {
        // Trigger the door animation by setting the "Open" trigger parameter
        animator.SetTrigger("Open");
    }
}
