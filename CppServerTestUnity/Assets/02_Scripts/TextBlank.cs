using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlank : MonoBehaviour
{
    [SerializeField]
    Text _chatText;

    public void ShowText(string chat)
    {
        _chatText.text = chat;
    }
}
