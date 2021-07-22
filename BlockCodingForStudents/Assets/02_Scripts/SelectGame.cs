using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGame : MonoBehaviour
{
    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameObject _gameListPrefab;
    [SerializeField]
    GameObject _gameIconPrefab;

    public void InitSelectGame(Dictionary<int, List<int>> allGameInfoDic)
    {
        foreach(int key in allGameInfoDic.Keys)
        {
            GameList gameList = Instantiate(_gameListPrefab, _scrollTarget).GetComponent<GameList>();
            gameList.InitGameList(key, allGameInfoDic[key], _gameIconPrefab);
        }
    }
}
