using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCounterUI : MonoBehaviour
{
    private Text keyCountText;
    private int keysCollected;

    private void Start()
    {
        keyCountText = GetComponent<Text>();
        keysCollected = 0;
        UpdateKeyCountUI();
    }

    public void CollectKey()
    {
        keysCollected++;
        UpdateKeyCountUI();
    }

    private void UpdateKeyCountUI()
    {
        keyCountText.text = "Keys: " + keysCollected.ToString();
    }
}
