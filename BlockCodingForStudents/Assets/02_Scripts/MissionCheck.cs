using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCheck : MonoBehaviour
{
    static MissionCheck _uniqueInstance;
    public static MissionCheck _instance { get { return _uniqueInstance; } }

    [SerializeField]
    GameObject _missionListPrefab;
    [SerializeField]
    GameObject _missionIconPrefab;

    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameObject _missionCategoryObj;
    [SerializeField]
    MissionBoard _missionBoard;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void OnEnable()
    {
        ShowMissionCategory();
    }

    private void Start()
    {
        InitMissionCheck();
    }

    public void InitMissionCheck()
    {
        List<int> gameList = new List<int>();
        for(int n = 0; n < Random.Range(2, 6); n++)
            gameList.Add(1);
        MissionList missionList = Instantiate(_missionListPrefab, _scrollTarget).GetComponent<MissionList>();
        missionList.InitGameList(1, gameList, _missionIconPrefab);

        gameList.Clear();
        for (int n = 0; n < Random.Range(2, 6); n++)
            gameList.Add(1);
        missionList = Instantiate(_missionListPrefab, _scrollTarget).GetComponent<MissionList>();
        missionList.InitGameList(2, gameList, _missionIconPrefab);

        gameList.Clear();
        for (int n = 0; n < Random.Range(2, 6); n++)
            gameList.Add(1);
        missionList = Instantiate(_missionListPrefab, _scrollTarget).GetComponent<MissionList>();
        missionList.InitGameList(3, gameList, _missionIconPrefab);
    }

    void ShowMissionCategory()
    {
        _missionCategoryObj.SetActive(true);
        _missionBoard.gameObject.SetActive(false);

        if (TeacherMainUI._instance != null)
            TeacherMainUI._instance.AssignFunctionToBackBtn(StudentInfoMenu._instance.ShowStudentInfoMenu);
    }

    public void ShowMissionBoard(int level, int gameIndex)
    {
        _missionCategoryObj.SetActive(false);
        _missionBoard.gameObject.SetActive(true);

        // CreateMissionBoard
        List<MissionData> missionDataList = new List<MissionData>();
        for(int n = 0; n < Random.Range(5, 15); n++)
        {
            MissionData mD = new MissionData(n + 1, "미션이 채워질 부분입니다.", Random.Range(0, 2) == 0 ? true : false);
            missionDataList.Add(mD);
        }
        _missionBoard.InitMissionBoard(level, "블록 코딩", missionDataList);

        if (TeacherMainUI._instance != null)
            TeacherMainUI._instance.AssignFunctionToBackBtn(ShowMissionCategory);
    }
}
