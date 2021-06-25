using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalInfo : MonoBehaviour
{
    [SerializeField]
    Text _schoolNameTxt;
    [SerializeField]
    Text _classInfoTxt;

    public void InitPersonalInfo(string schoolName, int grade, int group, int number)
    {
        _schoolNameTxt.text = schoolName;
        _classInfoTxt.text = grade.ToString() + "학년 " + grade.ToString() + "반 " + number.ToString() + "번";
    }
}
