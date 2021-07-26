using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinContent : MonoBehaviour
{
    [SerializeField]
    Text _studentTypeTxt;
    [SerializeField]
    Image _selectedIcon;
    [SerializeField]
    Button _acceptBtn;
    [SerializeField]
    Button _refuseBtn;

    private void Start()
    {
        _acceptBtn.onClick.AddListener(() => { AcceptBtn(); });
        _refuseBtn.onClick.AddListener(() => { RefuseBtn(); });
    }

    public void InitJoinContent(Sprite icon, string studentType)
    {
        //_selectedIcon.sprite = icon;
        _studentTypeTxt.text = studentType;
    }

    public void AcceptBtn()
    {
        Debug.Log("Accept");
    }

    public void RefuseBtn()
    {
        Debug.Log("Refuse");
    }
}
