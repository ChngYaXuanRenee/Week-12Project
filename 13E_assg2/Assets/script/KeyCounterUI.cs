using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCounterUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI keyCountText;
    private int keysCollected;
    public TextTyper textTyper;

    private void Start()
    {
        keysCollected = 0;
        UpdateKeyCountUI();
    }

    public void CollectKey()
    {
        keysCollected++;
        UpdateKeyCountUI();

        if (keysCollected == 3)
        {
            textTyper.StartNewMessage();
        }
    }

    private void UpdateKeyCountUI()
    {
        keyCountText.text = "Keys: " + keysCollected.ToString() + "/3";
    }
}
