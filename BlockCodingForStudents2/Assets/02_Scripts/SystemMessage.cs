using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessage : MonoBehaviour
{
    [SerializeField]
    Text _gameTitleTxt;

    public void ShowGameTitle(int game, int level)
    {
        _gameTitleTxt.text = ((GameListGroup.eGameName)game).ToString() + " Level " + level.ToString();
    }
}
