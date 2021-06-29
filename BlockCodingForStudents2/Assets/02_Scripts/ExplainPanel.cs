using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplainPanel : MonoBehaviour
{
    [SerializeField]
    Image _gameIcon;
    [SerializeField]
    Text _gameExplainTxt;
    [SerializeField]
    Button _gameStartBtn;

    private void Start()
    {
        _gameStartBtn.onClick.AddListener(() => { SceneControlManager._instance.StartLoadTestScene(); });
    }

    public void InitPanel(Sprite icon, string txt)
    {
        _gameIcon.sprite = icon;
    }
}
