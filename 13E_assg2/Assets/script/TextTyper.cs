using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextTyper : MonoBehaviour
{
    public float letterPause = 0.2f;
    // public AudioClip typeSound1;
    // public AudioClip typeSound2;

    public string[] messages; // An array to hold multiple messages
    TextMeshProUGUI textComp;

    int currentMessageIndex = 0; // Index to keep track of the current message being displayed

    // Use this for initialization
    void Start()
    {
        textComp = GetComponent<TextMeshProUGUI>(); // Use TextMeshProUGUI instead of Text

        // Find the button component and attach a click event listener
        Button button = GameObject.Find("YourButtonName").GetComponent<Button>();
        button.onClick.AddListener(StartTextTyper);

        // Hide the text initially
        textComp.text = "";
    }

    void StartTextTyper()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textComp.text = ""; // Clear the previous message

        string message = messages[currentMessageIndex]; // Get the current message

        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;

            yield return new WaitForSeconds(letterPause);
        }

        yield return new WaitForSeconds(2f); // Wait for 2 seconds before clearing the message

        currentMessageIndex++; // Move to the next message

        if (currentMessageIndex < messages.Length)
        {
            StartCoroutine(TypeText()); // Start typing the next message
        }
    }
}