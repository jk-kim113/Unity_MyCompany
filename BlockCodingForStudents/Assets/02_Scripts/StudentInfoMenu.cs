using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentInfoMenu : MonoBehaviour
{
    public enum eMenuState
    {
        ChangeStudentInfo,
        CurrentStateOfMission,

        max
    }

    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameObject _checkContentPrefab;

    [SerializeField]
    Image _characterIcon;
    [SerializeField]
    Text _studentInfoTxt;

    [SerializeField]
    GameObject _checkListObj;
    [SerializeField]
    GameObject _changeStudentInfo;
    [SerializeField]
    GameObject _missionCheckObj;

    private void OnEnable()
    {
        ShowStudentInfoMenu();
    }

    private void Start()
    {
        // Temp
        CheckContent checkContent = Instantiate(_checkContentPrefab, _scrollTarget).GetComponent<CheckContent>();
        checkContent.InitCheckContent("1. 학생 정보 변경", ChangeMenu, 0);

        checkContent = Instantiate(_checkContentPrefab, _scrollTarget).GetComponent<CheckContent>();
        checkContent.InitCheckContent("2. 미션 진행 상황", ChangeMenu, 1);
    }

    public void InitStudentMenuInfo(Sprite icon, int number, string name)
    {
        _characterIcon.sprite = icon;
        _studentInfoTxt.text = number.ToString() + "번 " + name;
    }

    void ChangeMenu(int menu)
    {
        switch((eMenuState)menu)
        {
            case eMenuState.ChangeStudentInfo:
                _checkListObj.SetActive(false);
                _changeStudentInfo.SetActive(true);
                _missionCheckObj.SetActive(false);
                break;

            case eMenuState.CurrentStateOfMission:
                _checkListObj.SetActive(false);
                _changeStudentInfo.SetActive(false);
                _missionCheckObj.SetActive(true);
                break;
        }

        TeacherMainUI._instance.AssignFunctionToBackBtn(ShowStudentInfoMenu);
    }

    void ShowStudentInfoMenu()
    {
        _checkListObj.SetActive(true);
        _changeStudentInfo.SetActive(false);
        _missionCheckObj.SetActive(false);

        if(TeacherMainUI._instance != null)
            TeacherMainUI._instance.AssignFunctionToBackBtn(ClassInfo._instance.ShowPersonalInfo);
    }
}
