using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalInfo : MonoBehaviour
{
    [SerializeField]
    Text _peronalIDText;
    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameObject _classContentPrefab;
    [SerializeField]
    GameObject _groupBoxPrefab;

    Dictionary<int, ClassContent> _classInfoDic = new Dictionary<int, ClassContent>();

    public void InitClasGroup(string personalID, Dictionary<int, int[]> classInfoDic)
    {
        _peronalIDText.text = personalID;
        foreach(int key in classInfoDic.Keys)
            CreateClassContent(key, classInfoDic[key]);
    }

    public void AddClassGroup(int classNum, int groupNum)
    {
        if(_classInfoDic.ContainsKey(classNum))
            _classInfoDic[classNum].AddGroup(classNum,groupNum, _groupBoxPrefab);
        else
            CreateClassContent(classNum, groupNum);
    }

    void CreateClassContent(int classNum, params int[] groupNum)
    {
        ClassContent classContent = Instantiate(_classContentPrefab, _scrollTarget).GetComponent<ClassContent>();
        classContent.InitClassContent(classNum, _groupBoxPrefab, groupNum);
        _classInfoDic.Add(classNum, classContent);
    }
}
