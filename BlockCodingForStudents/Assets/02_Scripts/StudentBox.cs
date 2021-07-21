using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StudentBox : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Image _characImg;
    [SerializeField]
    Text _studentInfoTxt;

    ClassInfo _classInfo;

    int _myNum;
    string _myName;

    public void InitStudentBox(Sprite characImg, int number, string name, ClassInfo classInfo)
    {
        _characImg.sprite = characImg;
        _myNum = number;
        _myName = name;
        _studentInfoTxt.text = number.ToString() + "번 " + name;
        _classInfo = classInfo;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _classInfo.ClickStudentBox(_characImg.sprite, _myNum, _myName);
    }
}
