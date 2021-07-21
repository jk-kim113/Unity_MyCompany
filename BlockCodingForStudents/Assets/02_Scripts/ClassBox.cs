using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClassBox : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Text _groupText;

    int _myClass = 0;
    int _myGroup = 0;

    public void InitBox(int classNum, int group)
    {
        _groupText.text = group.ToString();
        _myClass = classNum;
        _myGroup = group;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TeacherMainUI._instance.GetClassInfo(_myClass, _myGroup);
    }
}
