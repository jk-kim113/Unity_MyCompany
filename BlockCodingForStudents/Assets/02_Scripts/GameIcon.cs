using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Image _clickedImg;
    [SerializeField]
    Transform _activeIconPos;

    Image _myGameIcon;
    int _myGameIndex;

    GameList _myGameList;
    bool _isOn = false;
    public bool _IsOn { set { _isOn = value; } }

    private void Awake()
    {
        _myGameIcon = GetComponent<Image>();
    }

    public void InitGameIcon(int gameIndex, GameList gameList)
    {
        _myGameIndex = gameIndex;
        _myGameList = gameList;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_myGameList != null)
            _myGameList.ShowActiveIcon(_activeIconPos, _isOn, _myGameIndex);
    }
}
