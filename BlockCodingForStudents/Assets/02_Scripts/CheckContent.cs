using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckContent : MonoBehaviour
{
    [SerializeField]
    Text _checkTxt;

    Button _myBtn;
    public delegate void CheckListFunction(int menu);

    private void Awake()
    {
        _myBtn = GetComponent<Button>();
    }

    public void InitCheckContent(string check, CheckListFunction function, int menu)
    {
        _checkTxt.text = check;
        _myBtn.onClick.AddListener(() => { function(menu); });
    }
}
