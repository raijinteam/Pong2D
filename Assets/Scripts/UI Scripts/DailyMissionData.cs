using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyMissionData : MonoBehaviour
{

    [Header("Daily Mission Data")]

    public int index;
    public Image img_RewardIcon;
    public TextMeshProUGUI txt_Description;
    public TextMeshProUGUI txt_MissionCurrentValue;
    public TextMeshProUGUI txt_MissionCompleteValue;
    public TextMeshProUGUI txt_RewardAmount;
    public RectTransform missionFinished;
    public RectTransform missionRewardClaimed;
    public Slider slider_RewardComplate;
    public Button btn_Claim;

    public bool isDailyMissionClaimed;
    public bool isDailyMissionFinished;
    public bool isGemsReward;


    public void CheckForRewardIsClaimed()
    {
        if (isDailyMissionClaimed)
        {
            missionFinished.gameObject.SetActive(true);
            missionRewardClaimed.gameObject.SetActive(true);
        }
    }

    public void OnClick_DailyMissionClaimButton()
    {
        if (isGemsReward)
        {
            //Increase Gems
            DataManager.Instance.IncreaseGems(int.Parse(txt_RewardAmount.text));
        }
        else
        {
            DataManager.Instance.IncreaseCoins(int.Parse(txt_RewardAmount.text));
        }

        DataManager.Instance.SetDailyMissionRewardState(index, true);
        Debug.Log("Daily mission climed state : " + DataManager.Instance.GetDailyMissionClaimedState(index));
        missionRewardClaimed.gameObject.SetActive(true);
        missionFinished.gameObject.SetActive(false);
        UIManager.instance.ui_HomePanel.ui_HomeScreen.img_DailyMissionsRedDot .gameObject.SetActive(false);
        UIManager.instance.ui_RewardSummary.SetRewardSummaryData(img_RewardIcon.sprite, txt_RewardAmount.text);
        UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
    }
}
