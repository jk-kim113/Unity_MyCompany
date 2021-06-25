using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameListGroup : MonoBehaviour
{
    public enum eLevelType
    {
        Level1,
        Level2,
        Level3,

        max
    }

    public enum eGameName
    {
        비행기게임,
        요리게임,
        정글슬러그,
        올림픽게임,

        max
    }

    [SerializeField]
    Transform[] _scorllTargetPosArr;
    [SerializeField]
    GameObject _gameContentObj;
    [SerializeField]
    Sprite[] _gameImageArr;

    public Sprite[] _GameImageArr { get { return _gameImageArr; } }

    int _currentDownGame;
    int _currnentDownGameLevel;

    public void DownGameContent(int game, int level)
    {
        _currentDownGame = game;
        _currnentDownGameLevel = level;
    }

    public void StartDownGame()
    {
        GameContent gameContent = Instantiate(_gameContentObj, _scorllTargetPosArr[_currnentDownGameLevel - 1]).GetComponent<GameContent>();
        gameContent.InitContent(_gameImageArr[_currentDownGame], ((eGameName)_currentDownGame).ToString(), _currentDownGame, _currnentDownGameLevel);
        gameContent.DownLoadGame();
    }
}
