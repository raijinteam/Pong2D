using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeScreenUi : MonoBehaviour
{


    public Image img_DailyMissionsRedDot;
    public Image img_SpinWheelRadDot;

    private void OnEnable()
    {
        SetWheelRedDot();
    }


    public void SetWheelRedDot()
    {
        if (DataManager.Instance.totalSkipIt > 0)
        {
            img_SpinWheelRadDot.gameObject.SetActive(true);
        }
        else{
            img_SpinWheelRadDot.gameObject.SetActive(false);
        }
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

    public void OnClick_OpenDailySpin()
    {
        Debug.Log("Open Dailyt SPin");
        UIManager.instance.ui_DailySpin.gameObject.SetActive(true);
    }
}
