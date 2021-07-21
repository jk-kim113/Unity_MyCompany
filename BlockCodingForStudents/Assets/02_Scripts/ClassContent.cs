using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassContent : MonoBehaviour
{
    [SerializeField]
    Text _classText;
    [SerializeField]
    Transform _scrollTarget;
    
    public void InitClassContent(int classNum, GameObject groupBoxPrefab, params int[] groupArr)
    {
        _classText.text = classNum.ToString() + "학년";

        for(int n = 0; n < groupArr.Length; n++)
            AddGroup(classNum,groupArr[n], groupBoxPrefab);
    }

    public void AddGroup(int classNum, int group, GameObject groupBoxPrefab)
    {
        ClassBox box = Instantiate(groupBoxPrefab, _scrollTarget).GetComponent<ClassBox>();
        box.InitBox(classNum, group);
    }
}
