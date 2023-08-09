using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyMissionUI : MonoBehaviour
{
    [SerializeField] private DailyMissionSO[] all_DailyMissionSO;
        [SerializeField] private int numberOfDailyMissions;
    [SerializeField] private GameObject pf_DailyMission;
    [SerializeField] private RectTransform dailyMissionParent;
    public TextMeshProUGUI txt_Timer;

    private List<DailyMissionData> list_DailyMission = new List<DailyMissionData>();

    private void Start()
    {
        //SpawnDailyMissions();
    }

    private void Update()
    {
        int timeInSeconds = (int)TimeManager.Instance.currentDailyMissionTime;

        float hours = timeInSeconds / 3600;
        float minutes = (timeInSeconds % 3600) / 60;
        float seconds = timeInSeconds % 60;


        txt_Timer.text = $"{(int)hours} : {(int)minutes} : {(int)seconds}";
    }

    public void increasemissinValue(int index , int value)
    {
        int currentValue = DataManager.Instance.GetAchivementCurrentValue(index) + value;
        DataManager.Instance.SetAchivementCurrentValue(index, currentValue);
        SetDailyMissionData(index);
    }
    

    public void SpawnDailyMissions()
    {
        for (int i = 0; i < numberOfDailyMissions; i++)
        {
            GameObject dailyMission = Instantiate(pf_DailyMission, dailyMissionParent.position, Quaternion.identity, dailyMissionParent);
            DailyMissionData mission = dailyMission.GetComponent<DailyMissionData>();

            list_DailyMission.Add(mission);
            SetDailyMissionData(i);
        }

    }

    private void SetDailyMissionData(int _index)
    {
        list_DailyMission[_index].index = _index;

        if (CheckForDailyMissionComplete(_index))
        {
            list_DailyMission[_index].missionFinished.gameObject.SetActive(true);
        }

        list_DailyMission[_index].isDailyMissionClaimed = DataManager.Instance.GetDailyMissionClaimedState(_index);

        if (list_DailyMission[_index].isDailyMissionClaimed)
        {
            list_DailyMission[_index].CheckForRewardIsClaimed();
        }

        list_DailyMission[_index].img_RewardIcon.sprite = all_DailyMissionSO[_index].img_RewardIcon;
        list_DailyMission[_index].txt_Description.text = all_DailyMissionSO[_index].str_Description;
        list_DailyMission[_index].txt_RewardAmount.text = all_DailyMissionSO[_index].rewardAmount.ToString();
        list_DailyMission[_index].txt_MissionCurrentValue.text = DataManager.Instance.GetAchivementCurrentValue(_index).ToString();
        list_DailyMission[_index].txt_MissionCompleteValue.text = all_DailyMissionSO[_index].missionCompeleteAmount.ToString();

        list_DailyMission[_index].slider_RewardComplate.maxValue = all_DailyMissionSO[_index].missionCompeleteAmount;
        list_DailyMission[_index].slider_RewardComplate.value = DataManager.Instance.GetAchivementCurrentValue(_index);
        list_DailyMission[_index].isGemsReward = all_DailyMissionSO[_index].isGemsReward;
    }


    private bool CheckForDailyMissionComplete(int index)
    {
        int currentValue = DataManager.Instance.GetAchivementCurrentValue(index);
        int maxValue = all_DailyMissionSO[index].missionCompeleteAmount;
        
        if (currentValue >= maxValue)
        {
            Debug.Log("mission is compleated");
            UIManager.instance.ui_HomePanel.ui_HomeScreen.img_DailyMissionsRedDot.gameObject.SetActive(true);
            return true;
        }
        else
        {
            Debug.Log("In else condition");
            UIManager.instance.ui_HomePanel.ui_HomeScreen.img_DailyMissionsRedDot.gameObject.SetActive(false);
        }
        return false;
    }

    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }
}

