using UnityEngine;
using UnityEngine.UI;

public class HideUIElements : MonoBehaviour
{
    public GameObject canvasMenu;
    public Button buttonToHide;

    private void Start()
    {
        // Add a click event listener to the button
        buttonToHide.onClick.AddListener(HideElements);
    }

    private void HideElements()
    {
        // Hide the canvas menu
        canvasMenu.SetActive(false);
    }
}
