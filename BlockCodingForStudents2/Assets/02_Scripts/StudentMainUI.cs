using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentMainUI : MonoBehaviour
{
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

    private void Start()
    {
        List<string> temp = new List<string>();

        for (int n = 0; n < (int)eSchoolName.max; n++)
            temp.Add(((eSchoolName)n).ToString());

        _schoolListDropdown.ClearOptions();
        _schoolListDropdown.AddOptions(temp);
    }

    public void EnterBtn()
    {
        if (!string.IsNullOrEmpty(_gradeInputField.text) && !string.IsNullOrEmpty(_groupInputField.text) && !string.IsNullOrEmpty(_numberInputField.text))
        {
            StudentClient._instance.SendClientInfo(_schoolListDropdown.value, int.Parse(_gradeInputField.text), int.Parse(_groupInputField.text),
                int.Parse(_numberInputField.text));
        }
    }
}
