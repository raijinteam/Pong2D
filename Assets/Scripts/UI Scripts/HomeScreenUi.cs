using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenUi : MonoBehaviour
{




    private void OnEnable()
    {
    

    }

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
}
