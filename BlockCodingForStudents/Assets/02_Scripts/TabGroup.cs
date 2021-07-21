using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField]
    Sprite _originTabIcon;
    [SerializeField]
    Sprite _clickedTabIcon;

    Button[] _tabBtnArr;
    Image[] _tabImgArr;
    public delegate void ActiveFunction(int tabState);

    int _currentClickedIndex = -1;

    private void Awake()
    {
        _tabBtnArr = GetComponentsInChildren<Button>();
        _tabImgArr = GetComponentsInChildren<Image>();
    }

    public void InitBtn(ActiveFunction activeFunction)
    {
        for(int n = 0; n < _tabBtnArr.Length; n++)
        {
            int state = n;
            _tabBtnArr[n].onClick.AddListener(() => { activeFunction(state); });
        }
    }

    public void ShowClickedImg(int index)
    {
        _tabImgArr[index].sprite = _clickedTabIcon;

        if(_currentClickedIndex >= 0)
            _tabImgArr[_currentClickedIndex].sprite = _originTabIcon;

        _currentClickedIndex = index;
    }
}
