using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionContent : MonoBehaviour
{
    [SerializeField]
    Text _missionTxt;
    [SerializeField]
    Text _isCorrectTxt;

    public void InitMissionContent(string mission, bool isCorrect)
    {
        _missionTxt.text = mission;
        _isCorrectTxt.text = isCorrect ? "O" : "X";
    }
}
