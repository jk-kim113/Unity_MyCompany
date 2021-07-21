using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    Button _myBtn;

    public delegate void BackFunction();

    private void Awake()
    {
        _myBtn = GetComponent<Button>();
    }

    public void AssignFunction(BackFunction function)
    {
        if(_myBtn == null)
            _myBtn = GetComponent<Button>();

        _myBtn.onClick.RemoveAllListeners();
        _myBtn.onClick.AddListener(() => { function(); });
    }
}
