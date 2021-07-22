using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameActiveIcon : MonoBehaviour
{
    [SerializeField]
    Button _downloadBtn;
    [SerializeField]
    Button _activeBtn;
    [SerializeField]
    Button _currentOnBtn;
    [SerializeField]
    Button _currentOffBtn;

    bool _isCurrentGameOn;
    public bool _IsCurrentGameOn { set { _isCurrentGameOn = value; } }

    private void Start()
    {
        _downloadBtn.onClick.AddListener(() => { DownloadGame(); });
        _activeBtn.onClick.AddListener(() => { ActiveGame(); });
        _currentOnBtn.onClick.AddListener(() => { OnOffGame(); });
        _currentOffBtn.onClick.AddListener(() => { OnOffGame(); });

        //temp
        InitGameActiveIcon(true);
    }

    public void InitGameActiveIcon(bool isCurrentGameOn)
    {
        _isCurrentGameOn = isCurrentGameOn;
        _currentOnBtn.gameObject.SetActive(isCurrentGameOn);
        _currentOffBtn.gameObject.SetActive(!isCurrentGameOn);
    }

    public void OnOffGame()
    {
        _currentOnBtn.gameObject.SetActive(!_isCurrentGameOn);
        _currentOffBtn.gameObject.SetActive(_isCurrentGameOn);

        _isCurrentGameOn = !_isCurrentGameOn;

        // Send Info OnOff
    }

    public void DownloadGame()
    {
        Debug.Log("Download");
    }

    public void ActiveGame()
    {
        Debug.Log("Active");
    }
}
