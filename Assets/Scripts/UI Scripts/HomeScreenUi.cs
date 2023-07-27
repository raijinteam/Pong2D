using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeScreenUi : MonoBehaviour
{


    public Image img_RedDot;



    public void OnClick_StartGame()
    {
        this.gameObject.SetActive(false);
        UIManager.instance.ui_HomePanel.ui_Level.gameObject.SetActive(true);
    }

    public void OnClick_OpenDailyRewards()
    {
        UIManager.instance.ui_DailyReward.gameObject.SetActive(true);
    }

    public void OnClick_OpenAchivements()
    {
        UIManager.instance.ui_Achievement.gameObject.SetActive(true);
    }

    public void OnClick_OpenDailySpin()
    {
        UIManager.instance.ui_DailySpin.gameObject.SetActive(true);
    }
}
