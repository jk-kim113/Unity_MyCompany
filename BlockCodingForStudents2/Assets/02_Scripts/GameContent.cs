using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameContent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    Text _titleTxt;
    [SerializeField]
    Image _icon;

    Vector3 _originSize;

    private void Awake()
    {
        _originSize = _icon.transform.localScale;
    }

    public void InitContent(Sprite icon, string txt)
    {
        _icon.sprite = icon;
        _titleTxt.text = txt;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _icon.transform.localScale = _originSize * 0.92f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _icon.transform.localScale = _originSize;
        StudentMainUI._instance.MoveExplainPanel();
    }
}
