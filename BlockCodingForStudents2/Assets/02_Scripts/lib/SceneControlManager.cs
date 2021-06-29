using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : TSingleton<SceneControlManager>
{
    public enum eTypeScene
    {
        Test = 0,
    }

    public enum eStateLoadding
    {
        none = 0,
        UnLoad,
        Load,
        Loading,
        LoadingDone,
        EndLoad
    }

    eTypeScene _currentScene;
    public eTypeScene _nowScene { get { return _currentScene; } }
    eTypeScene _prevScene;
    eStateLoadding _loadState;

    protected override void Init()
    {
        base.Init();
    }

    private void Update()
    {
        if (_loadState == eStateLoadding.EndLoad)
        {
            StopCoroutine("LoadingPrecess");
            _loadState = eStateLoadding.none;
        }
    }

    IEnumerator LoadingPrecess(string sceneName, ScreenOrientation ori)
    {
        _loadState = eStateLoadding.Load;

        while(Screen.orientation != ori)
        {
            yield return null;
        }

        LoadingUI loadingUI = StudentMainUI._instance.GetUI(transform);
        float time = Random.Range(2.0f, 5.0f);

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        _loadState = eStateLoadding.Loading;
        while (!asyncOp.isDone)
        {
            loadingUI.SettingLoadRate(asyncOp.progress);
            yield return null;
        }

        yield return new WaitForSeconds(time);

        loadingUI.SettingLoadRate(1);
        yield return new WaitForSeconds(1.8f);

        _loadState = eStateLoadding.LoadingDone;
        //UIManager._instance.Close(UIManager.eKindWindow.LoadingUI);
        loadingUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        _loadState = eStateLoadding.EndLoad;
    }

    public void StartLoadTestScene()
    {
        StudentMainUI._instance._BlindImgObj.gameObject.SetActive(true);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        _prevScene = _currentScene;
        _currentScene = eTypeScene.Test;
        StartCoroutine(LoadingPrecess("TestScene", ScreenOrientation.LandscapeLeft));
    }
}
