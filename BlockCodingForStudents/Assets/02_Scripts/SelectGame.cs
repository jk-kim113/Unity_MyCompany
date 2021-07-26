using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGame : MonoBehaviour
{
    static SelectGame _uniqueInstance;
    public static SelectGame _instance { get { return _uniqueInstance; } }

    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameObject _gameListPrefab;
    [SerializeField]
    GameObject _gameIconPrefab;

    int _currentActiveList = -1;
    Dictionary<int, GameList> _gameListDic = new Dictionary<int, GameList>();

    private void Awake()
    {
        _uniqueInstance = this;
    }

    public void InitSelectGame(Dictionary<int, List<int>> allGameInfoDic)
    {
        foreach (int key in _gameListDic.Keys)
            Destroy(_gameListDic[key].gameObject);
        _gameListDic.Clear();

        foreach (int key in allGameInfoDic.Keys)
        {
            GameList gameList = Instantiate(_gameListPrefab, _scrollTarget).GetComponent<GameList>();
            gameList.InitGameList(key, allGameInfoDic[key], _gameIconPrefab);
            _gameListDic.Add(key, gameList);
        }
    }

    public void InformActiveList(int index)
    {
        _currentActiveList = index;

        foreach (int key in _gameListDic.Keys)
        {
            if (key == index)
                continue;

            _gameListDic[key].OffActiveIcon();
        }
    }
}
