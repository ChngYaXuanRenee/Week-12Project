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

    public Button button; // Reference to the button object

    // Use this for initialization
    void Start()
    {
        textComp = GetComponent<TextMeshProUGUI>(); // Use TextMeshProUGUI instead of Text

        button.onClick.AddListener(StartTextTyper); // Attach the click event listener

        // Hide the text initially
        textComp.text = "";
    }

    void StartTextTyper()
    {
        StartCoroutine(TypeText());
        button.interactable = false; // Disable the button after it's clicked
    }

    public void StartNewMessage()
    {
        // Add your code here to start a new typewriter message
        // You can reset the currentMessageIndex or trigger a new message in some other way

        // Display the messages
        StartCoroutine(DisplayMessages());
    }

    private IEnumerator DisplayMessages()
    {
        // Display the first message
        yield return StartCoroutine(DisplayMessage("Door is ready to open. Click on door to open it."));

        // Display the second message
        yield return StartCoroutine(DisplayMessage("Collect gun to start fighting zombies, and collect potions that helped to fix your spacecraft too!"));
    }

    private IEnumerator DisplayMessage(string message)
    {
        textComp.text = ""; // Clear the previous message

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
