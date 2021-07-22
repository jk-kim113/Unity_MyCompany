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
    GameObject _activeIconObj;

    List<GameIcon> _gameIconList = new List<GameIcon>();

    public void InitGameList(int level, List<int> gameIndexList, GameObject gameIconPrefab) 
    {
        _levelTxt.text = level.ToString();

        if(gameIndexList.Count > _gameIconList.Count)
        {
            for(int n = 0; n < gameIndexList.Count; n++)
            {
                if(n < _gameIconList.Count)
                {
                    GameIcon gameIcon = _gameIconList[n];
                    gameIcon.InitGameIcon(gameIndexList[n], this);
                    gameIcon.gameObject.SetActive(true);
                }
                else
                {
                    GameIcon gameIcon = Instantiate(gameIconPrefab, _scrollTarget).GetComponent<GameIcon>();
                    gameIcon.InitGameIcon(gameIndexList[n], this);
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
                    gameIcon.InitGameIcon(gameIndexList[n], this);
                }
                else
                    _gameIconList[n].gameObject.SetActive(false);
            }
        }
    }

    public void ShowActiveIcon(Transform showPos)
    {
        _activeIconObj.transform.position = showPos.position;
        _activeIconObj.SetActive(true);
    }
}
