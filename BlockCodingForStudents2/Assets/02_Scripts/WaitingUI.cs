using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingUI : MonoBehaviour
{
    [SerializeField]
    Text _stateTxt;

    string _baseWaitTxt = "연결중";

    private void Start()
    {
        _stateTxt.text = _baseWaitTxt;
    }

    
}
