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

    private List<DailyMissionSO> list_MissionSO = new List<DailyMissionSO>();

    private List<DailyMissionData> list_DailyMission = new List<DailyMissionData>();
    public bool isOnedayCompelete;
    private int dailyMissionSOIndex;

    private void Update()
    {
        int timeInSeconds = (int)TimeManager.Instance.currentDailyMissionTime;

        float hours = timeInSeconds / 3600;
        float minutes = (timeInSeconds % 3600) / 60;
        float seconds = timeInSeconds % 60;


        txt_Timer.text = $"{(int)hours} : {(int)minutes} : {(int)seconds}";
    }

    public void increasemissinValue(int index, int value)
    {
        int currentValue = DataManager.Instance.GetAchivementCurrentValue(index) + value;
        DataManager.Instance.SetAchivementCurrentValue(index, currentValue);
        SetDailyMissionData(index);
    }


    public void SpawnDailyMissions()
    {
       // Debug.Log("Is One day complete : " + DataManager.Instance.GetDailyMissionOneDayComplete());
        isOnedayCompelete = DataManager.Instance.GetDailyMissionOneDayComplete();

        for (int i = 0; i < numberOfDailyMissions; i++)
        {
            GameObject dailyMission = Instantiate(pf_DailyMission, dailyMissionParent.position, Quaternion.identity, dailyMissionParent);
            DailyMissionData mission = dailyMission.GetComponent<DailyMissionData>();



            list_DailyMission.Add(mission);
            SetDailyMissionData(i);
        }

    }

    private void SetRandomSO(int index)
    {
        if (isOnedayCompelete)
        {
            int checkRandomIndex = 0;
            int randomIndex = Random.Range(0, all_DailyMissionSO.Length);

            if (randomIndex == checkRandomIndex)
            {
                randomIndex = Random.Range(0, all_DailyMissionSO.Length);
            }
            else if (randomIndex == DataManager.Instance.GetDailyMisisonSOIndex(index))
            {
                randomIndex = Random.Range(0, all_DailyMissionSO.Length);
            }



            list_MissionSO.Add(all_DailyMissionSO[randomIndex]);
            DataManager.Instance.SetDailyMisisonSOIndex(index, randomIndex);
            Debug.Log("in if index : " + index);
            if (index == numberOfDailyMissions - 1)
            {
               // Debug.Log("False daily misison ");
                isOnedayCompelete = false;
                DataManager.Instance.SetDailyMissionOnedayComplete(false);
            }
            checkRandomIndex = randomIndex;
        }
        else
        {
            int _index = DataManager.Instance.GetDailyMisisonSOIndex(index);
           // Debug.Log("in else index : " + _index);
            list_MissionSO.Add(all_DailyMissionSO[_index]);

        }
    }

    public void ResetAllListData()
    {
        for(int i = 0; i < list_DailyMission.Count; i++)
        {
            Destroy(list_DailyMission[i].gameObject);
        }
        list_DailyMission.Clear();
        list_MissionSO.Clear();
    }

    private void SetDailyMissionData(int _index)
    {

        SetRandomSO(_index);

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

        list_DailyMission[_index].img_RewardIcon.sprite = list_MissionSO[_index].img_RewardIcon;
        list_DailyMission[_index].txt_Description.text = list_MissionSO[_index].str_Description;
        list_DailyMission[_index].txt_RewardAmount.text = list_MissionSO[_index].rewardAmount.ToString();
        list_DailyMission[_index].txt_MissionCurrentValue.text = DataManager.Instance.GetAchivementCurrentValue(_index).ToString();
        list_DailyMission[_index].txt_MissionCompleteValue.text = list_MissionSO[_index].missionCompeleteAmount.ToString();

        list_DailyMission[_index].slider_RewardComplate.maxValue = list_MissionSO[_index].missionCompeleteAmount;
        list_DailyMission[_index].slider_RewardComplate.value = DataManager.Instance.GetAchivementCurrentValue(_index);
        list_DailyMission[_index].rewardType = list_MissionSO[_index].rewardType;
    }





    private bool CheckForDailyMissionComplete(int index)
    {
        int currentValue = DataManager.Instance.GetAchivementCurrentValue(index);
        int maxValue = all_DailyMissionSO[index].missionCompeleteAmount;

        if (currentValue >= maxValue)
        {
            UIManager.instance.ui_HomePanel.ui_HomeScreen.img_DailyMissionsRedDot.gameObject.SetActive(true);
            return true;
        }
        else
        {
            UIManager.instance.ui_HomePanel.ui_HomeScreen.img_DailyMissionsRedDot.gameObject.SetActive(false);
        }
        return false;
    }

    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }
}

