using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionData
{
    int _index;
    string _mission;
    bool _isCorrect;

    public int _Index { get { return _index; } }
    public string _Mission { get { return _mission; } }
    public bool _IsCorrect { get { return _isCorrect; } }

    public MissionData(int index, string mission, bool isCorrect)
    {
        _index = index;
        _mission = mission;
        _isCorrect = isCorrect;
    }
}

public class MissionBoard : MonoBehaviour
{
    [SerializeField]
    Text _levelTxt;
    [SerializeField]
    Text _gameNameTxt;
    [SerializeField]
    Text _scoreTxt;
    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameObject _missionContentPrefab;

    List<MissionContent> _missionContentList = new List<MissionContent>();

    public void InitMissionBoard(int level, string gameName, List<MissionData> missionDataList)
    {
        _levelTxt.text = level.ToString();
        _gameNameTxt.text = gameName;

        int correctNum = 0;
        if (missionDataList.Count > _missionContentList.Count)
        {
            for(int n = 0; n < missionDataList.Count; n++)
            {
                if(n < _missionContentList.Count)
                {
                    MissionContent missionContent = _missionContentList[n];
                    missionContent.InitMissionContent(missionDataList[n]._Index + "번 " + missionDataList[n]._Mission, missionDataList[n]._IsCorrect);
                    missionContent.gameObject.SetActive(true);
                }
                else
                {
                    MissionContent missionContent = Instantiate(_missionContentPrefab, _scrollTarget).GetComponent<MissionContent>();
                    missionContent.InitMissionContent(missionDataList[n]._Index + "번 " + missionDataList[n]._Mission, missionDataList[n]._IsCorrect);
                    _missionContentList.Add(missionContent);
                }

                if (missionDataList[n]._IsCorrect)
                    correctNum++;
            }
        }
        else
        {
            for(int n = 0; n < _missionContentList.Count; n++)
            {
                if (n < missionDataList.Count)
                {
                    MissionContent missionContent = _missionContentList[n];
                    missionContent.InitMissionContent(missionDataList[n]._Index + "번 " + missionDataList[n]._Mission, missionDataList[n]._IsCorrect);

                    if (missionDataList[n]._IsCorrect)
                        correctNum++;
                }
                else
                    _missionContentList[n].gameObject.SetActive(false);
            }
        }

        _scoreTxt.text = string.Format("{0:F1}%", (((float)correctNum / missionDataList.Count) * 100));
    }
}
