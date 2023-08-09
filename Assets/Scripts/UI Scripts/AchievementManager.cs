using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
   /* public static AchievementManager Instance;

    public DailyMissionSO[] all_Missions; // REFERANCE OF ALL ACTIVE DAILY MISSIONS

    [SerializeField] private GameObject pf_Achievement;
    [SerializeField] private Transform parentOfAchivement;

    [SerializeField] private int numberOfDailyMissions = 4;


    public bool canIncreaseValueOfCurrentAchivement;
    public bool isAchievementRewardCollected;


    public List<DailyMissionData> list_DailyActiveMissions = new List<DailyMissionData>();

    private GameObject dailyMission;
    private DailyMissionData achievementData;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {


        canIncreaseValueOfCurrentAchivement = true;


        for (int i = 0; i < all_Missions.Length; i++)
        {
            dailyMission = Instantiate(pf_Achievement, transform.position, Quaternion.identity, parentOfAchivement);
            achievementData = dailyMission.GetComponent<DailyMissionData>();
            list_DailyActiveMissions.Add(achievementData);

            achievementData.txt_RewardAmount.text = "x" + all_Missions[i].rewardAmount.ToString();
            achievementData.slider_RewardComplate.value = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + i);
            achievementData.slider_RewardComplate.maxValue = all_Missions[i].missionCompeleteAmount;
            SetAchievementData(i);
            achievementData.index = i;
            achievementData.missionRewardClaimed.gameObject.SetActive(false);

            CheckIfAchievementRewardCollected(i);

            if (achievementData.isDailyMissionComplate)
            {
                //Debug.Log("In if COndidison");
                achievementData.btn_Claim.gameObject.SetActive(true);
                UIManager.instance.ui_HomePanel.ui_HomeScreen.img_DailyMissionsRedDot.gameObject.SetActive(true);
            }
        }

    }

     
    public void SetAchievementData(int index)
    {
        // Debug.Log("Set Data Of : " + index);
        list_DailyActiveMissions[index].img_RewardIcon.sprite = all_Missions[index].img_RewardIcon;
        list_DailyActiveMissions[index].txt_Description.text = all_Missions[index].str_Description;

        int currentMissionValue = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index);
        int requiredMissinValue = all_Missions[index].missionCompeleteAmount;
        list_DailyActiveMissions[index].txt_MissionProgress.text = $"{currentMissionValue} / {requiredMissinValue}";



        list_DailyActiveMissions[index].slider_RewardComplate.maxValue = all_Missions[index].rewardAmount;
    }


    public void CheckIfAchievementRewardCollected(int index)
    {

        if (DataManager.Instance.GetArchivmentRewardState(index) == 0)
        {
           // Debug.Log("First False");

            list_DailyActiveMissions[index].isDailyMissionComplate = false;
            list_DailyActiveMissions[index].missionCFinished.gameObject.SetActive(false);
            //UIManager.instance.ui_HomePanel.ui_HomeScreen.img_RedDot.gameObject.SetActive(false);
        }
        else
        {
            list_DailyActiveMissions[index].isDailyMissionComplate = true;
            list_DailyActiveMissions[index].missionCFinished.gameObject.SetActive(true);
            UIManager.instance.ui_HomePanel.ui_HomeScreen.img_DailyMissionsRedDot.gameObject.SetActive(true);

        }
    }


    public void IncreaseCurrentAchivementLevel(int index)
    {
        all_Missions[index].currentLevel++;

        DataManager.Instance.SetArchivmentRewardState(index, 0);
        CheckIfAchievementRewardCollected(index);

        if (all_Missions[index].currentLevel >= 5)
        {
            all_Missions[index].currentLevel = 0;
            list_DailyActiveMissions.Remove(list_DailyActiveMissions[index]);
        }
    }

    public void IncreaseAchievementCurrentValue(int index, float _increaseAmount)
    {
        if (canIncreaseValueOfCurrentAchivement)
        {
            float currentValue = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index) + (int)_increaseAmount;
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index, (int)currentValue);
            *//*list_ActiveAchivementData[index].txt_currentMissionAount.text = currentValue.ToString();*//*

            int requiredMissinValue = all_Missions[index].missionCompeleteAmount;
            list_DailyActiveMissions[index].txt_MissionProgress.text = $"{currentValue} / {requiredMissinValue}";


            list_DailyActiveMissions[index].slider_RewardComplate.value = currentValue;
            StartCoroutine(AchievementSlidlerAnimation(index));

            if (currentValue >= all_Missions[index].missionCompeleteAmount)
            {
                canIncreaseValueOfCurrentAchivement = false;
                list_DailyActiveMissions[index].isDailyMissionComplate = true;
                DataManager.Instance.SetArchivmentRewardState(index, 1);
                CheckIfAchievementRewardCollected(index);
            }
        }

    }


    private IEnumerator AchievementSlidlerAnimation(int index)
    {
        float currentTime = 0;
        float currentValue = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index);
        while (currentTime < 1)
        {
            currentTime = Time.deltaTime / 1;
            if (currentTime > list_DailyActiveMissions[index].slider_RewardComplate.value)
            {
                list_DailyActiveMissions[index].slider_RewardComplate.value++;
            }
            yield return null;
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }*/
}
