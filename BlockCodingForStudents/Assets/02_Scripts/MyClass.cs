using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyClass : MonoBehaviour
{
    [SerializeField]
    Text _classTxt;
    [SerializeField]
    Text _groupTxt;
    [SerializeField]
    Text _connectTxt;

    public void InitMyClass(int classNum, int groupNum)
    {
        _classTxt.text = classNum.ToString() + "학년";
        _groupTxt.text = groupNum.ToString() + "반";
    }
}
