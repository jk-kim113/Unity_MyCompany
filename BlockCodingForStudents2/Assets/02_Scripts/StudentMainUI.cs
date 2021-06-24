using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentMainUI : MonoBehaviour
{
    static StudentMainUI _uniqueInstance;
    public static StudentMainUI _instance { get { return _uniqueInstance; } }

    public enum eSchoolName
    {
        중동고등학교,
        휘문고등학교,
        경기고등학교,
        숙명여자고등학교,

        max
    }

    [SerializeField]
    Dropdown _schoolListDropdown;
    [SerializeField]
    InputField _gradeInputField;
    [SerializeField]
    InputField _groupInputField;
    [SerializeField]
    InputField _numberInputField;

    [SerializeField]
    Animator _explainPanelAnim;
    [SerializeField]
    GameObject _initSetting;
    [SerializeField]
    GameObject _gameListGroup;

    public GameObject _InitSettingObj { get { return _initSetting; } }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        List<string> temp = new List<string>();

        for (int n = 0; n < (int)eSchoolName.max; n++)
            temp.Add(((eSchoolName)n).ToString());

        _schoolListDropdown.ClearOptions();
        _schoolListDropdown.AddOptions(temp);

        _initSetting.SetActive(false);
        _gameListGroup.SetActive(false);
    }

    public void EnterBtn()
    {
        if (!string.IsNullOrEmpty(_gradeInputField.text) && !string.IsNullOrEmpty(_groupInputField.text) && !string.IsNullOrEmpty(_numberInputField.text))
        {
            StudentClient._instance.SendClientInfo(_schoolListDropdown.value, int.Parse(_gradeInputField.text), int.Parse(_groupInputField.text),
                int.Parse(_numberInputField.text));
        }
    }

    public void MoveExplainPanel()
    {
        _explainPanelAnim.SetTrigger("IsMove");
    }

    public void ShowGameList()
    {
        _initSetting.SetActive(false);
        _gameListGroup.SetActive(true);
    }
}
