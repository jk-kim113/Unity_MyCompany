using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalInfo : MonoBehaviour
{
    static PersonalInfo _uniqueInstance;
    public static PersonalInfo _instance { get { return _uniqueInstance; } }

    [SerializeField]
    Text _peronalIDText;
    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameObject _classContentPrefab;
    [SerializeField]
    GameObject _groupBoxPrefab;

    Dictionary<int, ClassContent> _classInfoDic = new Dictionary<int, ClassContent>();

    private void Awake()
    {
        _uniqueInstance = this;
    }

    public void InitClasGroup(string personalID, Dictionary<int, List<int>> classInfoDic)
    {
        _peronalIDText.text = personalID;
        foreach(int key in classInfoDic.Keys)
        {
            int[] temp = new int[classInfoDic[key].Count];
            for(int n = 0; n < temp.Length; n++)
                temp[n] = classInfoDic[key][n];

            CreateClassContent(key, temp);
        }   
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
