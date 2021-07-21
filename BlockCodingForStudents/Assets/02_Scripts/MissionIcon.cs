using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionIcon : MonoBehaviour, IPointerClickHandler
{
    Image _missionIcon;
    int _myLevel;
    int _myGameIndex;

    private void Awake()
    {
        _missionIcon = GetComponent<Image>();
    }

    public void InitMissionIcon(Sprite icon, int level, int gameIndex)
    {
        //_missionIcon.sprite = 
        _myLevel = level;
        _myGameIndex = gameIndex;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MissionCheck._instance.ShowMissionBoard(_myLevel, _myGameIndex);
    }
}
