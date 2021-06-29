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
    Animator _systemMessageAnim;
    [SerializeField]
    GameObject _initSetting;
    [SerializeField]
    GameObject _gameListGroup;
    [SerializeField]
    PersonalInfo _personalInfo;

    [SerializeField]
    GameObject _loadingUIObj;
    [SerializeField]
    GameObject _blindImgObj;

    public GameObject _BlindImgObj { get { return _blindImgObj; } }

    public GameObject _InitSettingObj { get { return _initSetting; } }

    private void Awake()
    {
        _uniqueInstance = this;

        Screen.orientation = ScreenOrientation.Portrait;
        _blindImgObj.gameObject.SetActive(false);
    }

    private void Start()
    {
        List<string> temp = new List<string>();

        for (int n = 0; n < (int)eSchoolName.max; n++)
            temp.Add(((eSchoolName)n).ToString());

        _schoolListDropdown.ClearOptions();
        _schoolListDropdown.AddOptions(temp);

        _initSetting.SetActive(false);
        _gameListGroup.SetActive(true);
    }

    public void EnterBtn()
    {
        if (!string.IsNullOrEmpty(_gradeInputField.text) && !string.IsNullOrEmpty(_groupInputField.text) && !string.IsNullOrEmpty(_numberInputField.text))
        {
            StudentClient._instance.SendClientInfo(_schoolListDropdown.value, int.Parse(_gradeInputField.text), int.Parse(_groupInputField.text),
                int.Parse(_numberInputField.text));

            _personalInfo.InitPersonalInfo(_schoolListDropdown.options[_schoolListDropdown.value].ToString(), int.Parse(_gradeInputField.text), int.Parse(_groupInputField.text),
                int.Parse(_numberInputField.text));
        }
    }

    public void MoveExplainPanel(int gameIndex)
    {
        if(gameIndex >= 0)
        {
            ExplainPanel ep = _explainPanelAnim.gameObject.GetComponent<ExplainPanel>();
            GameListGroup gameListGroup = _gameListGroup.GetComponent<GameListGroup>();
            ep.InitPanel(gameListGroup._GameImageArr[gameIndex], "a");
        }

        _explainPanelAnim.SetTrigger("IsMove");
    }

    public void MoveSystemMessage(int game, int level)
    {
        SystemMessage sm = _systemMessageAnim.gameObject.GetComponent<SystemMessage>();
        sm.ShowGameTitle(game, level);

        _systemMessageAnim.SetTrigger("IsMove");
    }

    public void ShowGameList()
    {
        _initSetting.SetActive(false);
        _gameListGroup.SetActive(true);
    }

    public void DownGame(int game, int level)
    {
        GameListGroup gameListGroup = _gameListGroup.GetComponent<GameListGroup>();
        gameListGroup.DownGameContent(game, level);
    }

    public void StartDownGame()
    {
        _systemMessageAnim.SetTrigger("IsMove");
        GameListGroup gameListGroup = _gameListGroup.GetComponent<GameListGroup>();
        gameListGroup.StartDownGame();
    }

    public void ShowPersonalInfo()
    {

    }

    public LoadingUI GetUI(Transform myTr)
    {
        LoadingUI wnd = Instantiate(_loadingUIObj, myTr).GetComponent<LoadingUI>();
        wnd.OpenLoadingWnd();

        return wnd;
    }
}
