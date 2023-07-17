using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image[] all_Levels;



   
    public void OnClick_StartLevel()
    {
        this.gameObject.SetActive(false);
        UIManager.instance.ui_MatchMaker.gameObject.SetActive(true);
    }
}
