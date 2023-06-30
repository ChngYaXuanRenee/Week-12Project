using UnityEngine;

public class SpeedBoostController : MonoBehaviour
{
    public float boostDuration = 10f;
    public float boostMultiplier = 10f;

    private PlayerController playerMovement;
    private bool isBoostActive = false;
    private float previousSpeedMultiplier;

    private void Start()
    {
        playerMovement = GetComponent<PlayerController>();
    }

    public void ApplySpeedBoost()
    {
        if (!isBoostActive)
        {
            Debug.Log("Applying speed boost");
            // Save the current speed multiplier value
            previousSpeedMultiplier = playerMovement.GetSpeedMultiplier();

            // Apply the speed boost effect
            playerMovement.SetSpeedMultiplier(boostMultiplier);

            // Start a timer to revert the speed boost effect after a certain duration
            Invoke("RevertSpeedBoost", boostDuration);

            isBoostActive = true;
        }
    }

    private void RevertSpeedBoost()
    {
        // Revert the speed boost effect by setting the previous speed multiplier value
        playerMovement.SetSpeedMultiplier(previousSpeedMultiplier);

        isBoostActive = false;
    }

}
