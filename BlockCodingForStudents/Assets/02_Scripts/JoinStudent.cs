using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinStudent : MonoBehaviour
{

    [SerializeField]
    Transform _scrollTarget;
    [SerializeField]
    GameObject _joinContentPrefab;

    private void Start()
    {
        //AddStudent(1, "일반 학생");
        //AddStudent(2, "전 학생");
        //AddStudent(3, "일반 학생");
        //AddStudent(4, "전 학생");
    }

    public void AddStudent(int iconIdx, string studentType, string schoolName, int grade, int group, int num, string studentName)
    {
        JoinContent joinContent = Instantiate(_joinContentPrefab, _scrollTarget).GetComponent<JoinContent>();
        joinContent.InitJoinContent(null, studentType, schoolName, grade, group, num, studentName);
    }
}
