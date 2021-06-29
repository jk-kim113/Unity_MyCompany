using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    Slider _loadingBar;

    public void OpenLoadingWnd()
    {
        _loadingBar.value = 0;
    }

    public void SettingLoadRate(float rate)
    {
        _loadingBar.value = rate;
    }
}
