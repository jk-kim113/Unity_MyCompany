using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoinContent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Text _studentTypeTxt;
    [SerializeField]
    Image _selectedIcon;
    [SerializeField]
    Button _acceptBtn;
    [SerializeField]
    Button _refuseBtn;

    [SerializeField]
    Text _schoolNameTxt;
    [SerializeField]
    Text _gradeGroupTxt;
    [SerializeField]
    Text _numStudentNameTxt;

    [SerializeField]
    GameObject _infoObj;

    bool _isInfoObjOn = false;

    private void Start()
    {
        _acceptBtn.onClick.AddListener(() => { AcceptBtn(); });
        _refuseBtn.onClick.AddListener(() => { RefuseBtn(); });

        _infoObj.SetActive(_isInfoObjOn);
    }

    public void InitJoinContent(Sprite icon, string studentType, string schoolName, int grade, int group, int num, string studentName)
    {
        //_selectedIcon.sprite = icon;
        _studentTypeTxt.text = studentType;

        _schoolNameTxt.text = schoolName;
        _gradeGroupTxt.text = string.Format("{0}학년 {1}반", grade, group);
        _numStudentNameTxt.text = string.Format("{0}번 {1}", num, studentName);
    }

    public void AcceptBtn()
    {
        Debug.Log("Accept");

        gameObject.SetActive(false);
    }

    public void RefuseBtn()
    {
        Debug.Log("Refuse");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _infoObj.SetActive(!_isInfoObjOn);
        _isInfoObjOn = !_isInfoObjOn;
    }
}
