using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    static TestManager _uniqueInstance;
    public static TestManager _instance { get { return _uniqueInstance; } }

    [SerializeField]
    InputField _inputField;
    [SerializeField]
    Transform _chatTr;
    [SerializeField]
    GameObject _chatPrefab;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(_inputField.text))
            SendMsgFunc();
    }

    public void ReceiveMsg(string chat)
    {
        GameObject chatObj = Instantiate(_chatPrefab, _chatTr);
        TextBlank textBlank = chatObj.GetComponent<TextBlank>();
        textBlank.ShowText(chat);
    }

    public void SendMsg()
    {
        if(!string.IsNullOrEmpty(_inputField.text))
            SendMsgFunc();
    }

    void SendMsgFunc()
    {
        ClientManager._instance.SendMsg(_inputField.text);
        _inputField.text = string.Empty;
    }
}
