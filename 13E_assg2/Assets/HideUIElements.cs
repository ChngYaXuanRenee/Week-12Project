using UnityEngine;
using UnityEngine.UI;

public class HideUIElements : MonoBehaviour
{
    public Text[] textsToHide;
    public Button buttonToHide;

    private void Start()
    {
        // Add a click event listener to the button
        buttonToHide.onClick.AddListener(HideElements);
    }

    private void HideElements()
    {
        // Hide the text objects
        foreach (Text text in textsToHide)
        {
            text.gameObject.SetActive(false);
        }

        // Hide the button
        buttonToHide.gameObject.SetActive(false);
    }
}
