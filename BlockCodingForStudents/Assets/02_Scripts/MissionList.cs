using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionList : MonoBehaviour
{
    [SerializeField]
    Text _levelTxt;
    [SerializeField]
    Transform _scrollTarget;

    public void InitGameList(int level, List<int> gameList, GameObject missionIconPrefab)
    {
        _levelTxt.text = level.ToString();

        for(int n = 0; n < gameList.Count; n++)
        {
            MissionIcon missionIcon = Instantiate(missionIconPrefab, _scrollTarget).GetComponent<MissionIcon>();
            missionIcon.InitMissionIcon(null, level, gameList[n]);
        }
    }
}
