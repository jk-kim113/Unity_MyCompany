using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStudentInfo : MonoBehaviour
{
    [SerializeField]
    Dropdown _cityInfoDropdown;
    [SerializeField]
    Dropdown _districtInfoDropdown;
    [SerializeField]
    Dropdown _schoolKindInfoDropdown;
    [SerializeField]
    Dropdown _schoolNameInfoDropdown;
    [SerializeField]
    Dropdown _gradeDropdown;
    [SerializeField]
    Dropdown _groupDropdown;
    [SerializeField]
    Dropdown _numberDropdown;

    [SerializeField]
    InputField _studentNameInputField;

    private void Start()
    {
        #region dropbox init
        List<string> temp = new List<string>();

        temp.Add("===");
        //for (int n = 0; n < TableManager._instance.Get(eTableType.CityData)._datas.Count; n++)
        //    temp.Add(TableManager._instance.Get(eTableType.CityData).ToS(n + 1, CityData.Index.Name.ToString()));

        _cityInfoDropdown.ClearOptions();
        _cityInfoDropdown.AddOptions(temp);

        temp.Clear();
        temp.Add("===");
        _districtInfoDropdown.ClearOptions();
        _districtInfoDropdown.AddOptions(temp);

        temp.Clear();
        temp.Add("===");
        _schoolKindInfoDropdown.ClearOptions();
        _schoolKindInfoDropdown.AddOptions(temp);

        temp.Clear();
        temp.Add("===");
        _schoolNameInfoDropdown.ClearOptions();
        _schoolNameInfoDropdown.AddOptions(temp);

        temp.Clear();
        temp.Add("===");
        _gradeDropdown.ClearOptions();
        _gradeDropdown.AddOptions(temp);

        temp.Clear();
        temp.Add("===");
        _groupDropdown.ClearOptions();
        _groupDropdown.AddOptions(temp);

        temp.Clear();
        temp.Add("===");
        _numberDropdown.ClearOptions();
        _numberDropdown.AddOptions(temp);
        #endregion
    }

    public void ApplyBtn()
    {

    }
}
