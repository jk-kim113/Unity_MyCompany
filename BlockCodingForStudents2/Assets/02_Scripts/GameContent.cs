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
    [SerializeField]
    Image _downIcon;

    Vector3 _originSize;
    bool _isDownload = false;

    int _myGameIndex;
    int _myGameLevel;

    private void Awake()
    {
        _originSize = _icon.transform.localScale;
    }

    public void InitContent(Sprite icon, string txt, int gameIndex, int gameLevel)
    {
        _icon.sprite = icon;
        _titleTxt.text = txt;

        _myGameIndex = gameIndex;
        _myGameLevel = gameLevel;
    }

    public void DownLoadGame()
    {
        StartCoroutine(DownLoadProcess());
    }

    IEnumerator DownLoadProcess()
    {
        _isDownload = true;
        yield return null;

        _icon.transform.localScale = Vector3.zero;

        while(_icon.transform.localScale.x < _originSize.x)
        {
            _icon.transform.localScale += Vector3.one * 0.08f;
            yield return new WaitForFixedUpdate();
        }

        _icon.transform.localScale = _originSize;

        float downGauge = 1.0f;
        float targetGauge = Random.Range(0.1f, 0.4f);
        float checkGauge = 0;
        _downIcon.fillAmount = downGauge;

        while(downGauge > 0)
        {
            checkGauge += 0.01f;
            downGauge -= 0.01f;
            _downIcon.fillAmount = downGauge;
            yield return new WaitForFixedUpdate();

            if(checkGauge > targetGauge)
            {
                checkGauge = 0;
                targetGauge = Random.Range(0.1f, 0.4f);

                float randomTime = Random.Range(1.2f, 2.5f);

                yield return new WaitForSeconds(randomTime);
            }
        }

        _downIcon.fillAmount = 0;
        _isDownload = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_isDownload)
            _icon.transform.localScale = _originSize * 0.92f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!_isDownload)
        {
            _icon.transform.localScale = _originSize;
            StudentMainUI._instance.MoveExplainPanel(_myGameIndex);
        }
    }
}
