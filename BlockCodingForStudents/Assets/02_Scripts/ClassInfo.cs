using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassInfo : MonoBehaviour
{
    static ClassInfo _uniqueInstance;
    public static ClassInfo _instance { get { return _uniqueInstance; } }

    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameObject _studentBoxPrefab;
    [SerializeField]
    GameObject _classMemberObj;
    [SerializeField]
    StudentInfoMenu _studentInfoMenu;

    List<StudentBox> _studentBoxList = new List<StudentBox>();

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void OnEnable()
    {
        ShowPersonalInfo();
    }

    public void InitClassInfo(List<StudentInfo> students)
    {
        if (students.Count > _studentBoxList.Count)
        {
            for (int n = 0; n < students.Count; n++)
            {
                if (n < _studentBoxList.Count)
                {
                    StudentBox box = _studentBoxList[n];
                    box.InitStudentBox(null, students[n]._Number, students[n]._Name, this);
                    box.gameObject.SetActive(true);
                }
                else
                {
                    StudentBox box = Instantiate(_studentBoxPrefab, _scrollTarget).GetComponent<StudentBox>();
                    box.InitStudentBox(null, students[n]._Number, students[n]._Name, this);
                    _studentBoxList.Add(box);
                }
            }
        }
        else
        {
            for (int n = 0; n < _studentBoxList.Count; n++)
            {
                if (n < students.Count)
                {
                    StudentBox box = _studentBoxList[n];
                    box.InitStudentBox(null, students[n]._Number, students[n]._Name, this);
                }
                else
                    _studentBoxList[n].gameObject.SetActive(false);
            }
        }
    }

    public void ClickStudentBox(Sprite icon, int num, string name)
    {
        _studentInfoMenu.InitStudentMenuInfo(icon, num, name);

        _classMemberObj.SetActive(false);
        _studentInfoMenu.gameObject.SetActive(true);

        TeacherMainUI._instance.AssignFunctionToBackBtn(ShowPersonalInfo);
    }

    public void ShowPersonalInfo()
    {
        _classMemberObj.SetActive(true);
        _studentInfoMenu.gameObject.SetActive(false);

        if (TeacherMainUI._instance != null)
            TeacherMainUI._instance.AssignFunctionToBackBtn(TeacherMainUI._instance.ShowClassTab);
    }
}
