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
        else
        {
            img_SpinWheelRadDot.gameObject.SetActive(false);
        }
    }

    public void OnClick_StartGame()
    {
        if (!DataManager.Instance.isGameFirstTimeLoad)
        {
            Debug.Log("Button clicked");
            this.gameObject.SetActive(false);
            UIManager.instance.ui_HomePanel.ui_Level.gameObject.SetActive(true);

        }

    }

    public void OnClick_OpenDailyRewards()
    {
        if (!DataManager.Instance.isGameFirstTimeLoad)
        {
            UIManager.instance.ui_DailyReward.gameObject.SetActive(true);

        }

    }

    public void OnClick_OpenAchivements()
    {
        if (!DataManager.Instance.isGameFirstTimeLoad)
        {
            UIManager.instance.ui_Achievement.gameObject.SetActive(true);

        }

    }

    public void OnClick_OpenDailySpin()
    {
        if (!DataManager.Instance.isGameFirstTimeLoad)
        {
            Debug.Log("Open Dailyt SPin");
            UIManager.instance.ui_DailySpin.gameObject.SetActive(true);

        }
    }
}
