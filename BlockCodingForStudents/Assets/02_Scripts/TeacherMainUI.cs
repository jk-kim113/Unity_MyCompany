using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentInfo
{
    int _spriteIndex;
    int _number;
    string _name;
    bool _isOnline;

    public int _SpriteIndex { get { return _spriteIndex; } }
    public int _Number { get { return _number; } }
    public string _Name { get { return _name; } }
    public bool _IsOnline { get { return _isOnline; } }

    public StudentInfo(int spriteIndex, int number, string name, bool isOnline)
    {
        _spriteIndex = spriteIndex;
        _number = number;
        _name = name;
        _isOnline = isOnline;
    }
}

public class TeacherMainUI : MonoBehaviour
{
    static TeacherMainUI _uniqueInstance;
    public static TeacherMainUI _instance { get { return _uniqueInstance; } }

    public enum eTabState
    {
        Home,
        Class,
        Game,
        Join,
        Record,

        max
    }

    [SerializeField]
    TabGroup _tabGroup;
    [SerializeField]
    MyClass _myClass;
    [SerializeField]
    PersonalInfo _personalInfo;
    [SerializeField]
    ClassInfo _classInfo;
    [SerializeField]
    GameObject _selectGameObj;
    [SerializeField]
    GameObject[] _tabObjArr;
    [SerializeField]
    BackButton _backBtn;

    [SerializeField]
    SelectGame _selectGame;

    eTabState _currentTabState = eTabState.max;

    Dictionary<int, Dictionary<int, List<StudentInfo>>> _classInfoDic = new Dictionary<int, Dictionary<int, List<StudentInfo>>>();

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        _tabGroup.InitBtn(ChangeTab);

        #region Temp Init Personal Info
        Dictionary<int, int[]> classInfoDic = new Dictionary<int, int[]>();

        int[] groupArr = new int[5];
        for (int n = 0; n < groupArr.Length; n++)
            groupArr[n] = n + 1;
        classInfoDic.Add(1, groupArr);

        groupArr = new int[5];
        for (int n = 0; n < groupArr.Length; n++)
            groupArr[n] = n + 5;
        classInfoDic.Add(2, groupArr);

        groupArr = new int[5];
        for (int n = 0; n < groupArr.Length; n++)
            groupArr[n] = n + 3;
        classInfoDic.Add(3, groupArr);
        
        _personalInfo.InitClasGroup("Reality_Reality", classInfoDic);
        #endregion

        List<StudentInfo> student = new List<StudentInfo>();
        for (int n = 0; n < 20; n++)
            student.Add(new StudentInfo(n ,n + 1, "학생" + (n + 1).ToString(), true));

        Dictionary<int, List<StudentInfo>> group = new Dictionary<int, List<StudentInfo>>();
        for (int n = 0; n < 5; n++)
            group.Add(n + 1, student);

        ChangeTab(0);
    }

    void ChangeTab(int tabState)
    {
        if (_currentTabState == (eTabState)tabState)
            return;

        _currentTabState = (eTabState)tabState;
        _tabGroup.ShowClickedImg(tabState);

        switch ((eTabState)tabState)
        {
            case eTabState.Home:
                _myClass.gameObject.SetActive(false);
                _classInfo.gameObject.SetActive(false);
                _personalInfo.gameObject.SetActive(false);
                break;
            case eTabState.Class:
                ShowClassTab();
                break;
            case eTabState.Game:
                _myClass.gameObject.SetActive(false);
                _classInfo.gameObject.SetActive(false);
                _personalInfo.gameObject.SetActive(true);
                _selectGameObj.SetActive(false);
                break;
            case eTabState.Join:
                _myClass.gameObject.SetActive(false);
                _classInfo.gameObject.SetActive(false);
                _personalInfo.gameObject.SetActive(false);
                break;
            case eTabState.Record:
                _myClass.gameObject.SetActive(false);
                _classInfo.gameObject.SetActive(false);
                _personalInfo.gameObject.SetActive(false);
                break;
        }

        for (int n = 0; n < _tabObjArr.Length; n++)
        {
            if (n == tabState)
            {
                _tabObjArr[n].SetActive(true);
                continue;
            }

            _tabObjArr[n].SetActive(false);
        }
    }

    public void GetClassInfo(int selectedClass, int selectedGroup)
    {
        _myClass.InitMyClass(selectedClass, selectedGroup);
        _myClass.gameObject.SetActive(true);
        _personalInfo.gameObject.SetActive(false);

        switch(_currentTabState)
        {
            case eTabState.Class:

                List<StudentInfo> temp = new List<StudentInfo>();
                for (int n = 0; n < Random.Range(18, 30); n++)
                    temp.Add(new StudentInfo(n, n + 1, "학생" + (n + 1).ToString(), true));
                _classInfo.InitClassInfo(temp);

                _classInfo.gameObject.SetActive(true);
                _backBtn.AssignFunction(ShowClassTab);

                break;

            case eTabState.Game:

                Dictionary<int, List<int>> allGameDic = new Dictionary<int, List<int>>();
                List<int> gameIdx = new List<int>();
                for (int n = 0; n < Random.Range(1, 4); n++)
                    gameIdx.Add(n);
                allGameDic.Add(1, gameIdx);

                gameIdx = new List<int>();
                for (int n = 0; n < Random.Range(1, 4); n++)
                    gameIdx.Add(n);
                allGameDic.Add(2, gameIdx);

                gameIdx = new List<int>();
                for (int n = 0; n < Random.Range(1, 4); n++)
                    gameIdx.Add(n);
                allGameDic.Add(3, gameIdx);

                _selectGame.InitSelectGame(allGameDic);
                _selectGame.gameObject.SetActive(true);

                break;
        }
    }

    public void AssignFunctionToBackBtn(BackButton.BackFunction function)
    {
        _backBtn.AssignFunction(function);
    }

    public void ShowClassTab()
    {
        _myClass.gameObject.SetActive(false);
        _personalInfo.gameObject.SetActive(true);
        _classInfo.gameObject.SetActive(false);
    }

    public void ExitProgram()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
