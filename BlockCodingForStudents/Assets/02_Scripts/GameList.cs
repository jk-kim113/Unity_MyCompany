using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameList : MonoBehaviour
{
    [SerializeField]
    Text _levelTxt;
    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameActiveIcon _activeIcon;

    int _myIndex;
    List<GameIcon> _gameIconList = new List<GameIcon>();

    int _selectedOderIndex;
    int _selectedGameIndex;

    private void OnEnable()
    {
        _activeIcon.gameObject.SetActive(false);
    }

    public void InitGameList(int level, List<int> gameIndexList, GameObject gameIconPrefab) 
    {
        _myIndex = level;
        _levelTxt.text = level.ToString();

        if(gameIndexList.Count > _gameIconList.Count)
        {
            for(int n = 0; n < gameIndexList.Count; n++)
            {
                if(n < _gameIconList.Count)
                {
                    GameIcon gameIcon = _gameIconList[n];
                    gameIcon.InitGameIcon(gameIndexList[n], this, n);
                    gameIcon.gameObject.SetActive(true);
                }
                else
                {
                    GameIcon gameIcon = Instantiate(gameIconPrefab, _scrollTarget).GetComponent<GameIcon>();
                    gameIcon.InitGameIcon(gameIndexList[n], this, n);
                    _gameIconList.Add(gameIcon);
                }
            }
        }
        else
        {
            for(int n = 0; n < _gameIconList.Count; n++)
            {
                if (n < gameIndexList.Count)
                {
                    GameIcon gameIcon = _gameIconList[n];
                    gameIcon.InitGameIcon(gameIndexList[n], this, n);
                }
                else
                    _gameIconList[n].gameObject.SetActive(false);
            }
        }
    }

    public void ShowActiveIcon(Transform showPos, bool isOn, int orderIndex, int gameIndex)
    {
        _selectedOderIndex = orderIndex;
        _selectedGameIndex = gameIndex;

        _activeIcon.gameObject.transform.position = showPos.position;
        _activeIcon.InitGameActiveIcon(isOn, this);
        _activeIcon.gameObject.SetActive(true);

        SelectGame._instance.InformActiveList(_myIndex);
    }

    public void OffActiveIcon()
    {
        _activeIcon.gameObject.SetActive(false);
    }

    public void OnOffGame(bool isOn)
    {
        _gameIconList[_selectedOderIndex]._IsOn = isOn;
        TeacherClient._instance.SendOnOffGame(_selectedGameIndex, _myIndex, isOn);
    }

    public void DownloadGame()
    {
        TeacherClient._instance.SendDownloadInfo(_selectedGameIndex, _myIndex);
    }
}
