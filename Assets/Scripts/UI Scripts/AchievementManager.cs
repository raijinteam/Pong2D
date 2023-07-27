using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    public AchievementSO[] all_Archivements; // REFERANCE OF ALL ACTIVE DAILY MISSIONS

    [SerializeField] private GameObject pf_Achievement;
    [SerializeField] private Transform parentOfAchivement;

    [SerializeField] private int numberOfDailyMissions = 4;


    public bool canIncreaseValueOfCurrentAchivement;
    public bool isAchievementRewardCollected;


    public List<AchivementData> list_ActiveAchivementData = new List<AchivementData>();

    private GameObject dailyMission;
    private AchivementData achievementData;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {


        canIncreaseValueOfCurrentAchivement = true;


        for (int i = 0; i < all_Archivements.Length; i++)
        {
            dailyMission = Instantiate(pf_Achievement, transform.position, Quaternion.identity, parentOfAchivement);
            achievementData = dailyMission.GetComponent<AchivementData>();
            list_ActiveAchivementData.Add(achievementData);

            achievementData.txt_RewardAmount.text = "x" + all_Archivements[i].rewardAmount.ToString();
            achievementData.slider_RewardComplate.value = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + i);
            achievementData.slider_RewardComplate.maxValue = all_Archivements[i].missionCompeleteAmount[all_Archivements[i].currentLevel];
            SetAchievementData(i);
            achievementData.index = i;
            achievementData.btn_Claim.gameObject.SetActive(false);

            CheckIfAchievementRewardCollected(i);

            if (achievementData.isDailyMissionComplate)
            {
                //Debug.Log("In if COndidison");
                achievementData.btn_Claim.gameObject.SetActive(true);
                UIManager.instance.ui_HomePanel.ui_HomeScreen.img_RedDot.gameObject.SetActive(true);
            }
        }

    }

     
    public void SetAchievementData(int index)
    {
        // Debug.Log("Set Data Of : " + index);
        list_ActiveAchivementData[index].img_RewardIcon.sprite = all_Archivements[index].img_RewardIcon;
        list_ActiveAchivementData[index].txt_Description.text = all_Archivements[index].str_Description;
        list_ActiveAchivementData[index].txt_currentMissionAount.text = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index).ToString();
        list_ActiveAchivementData[index].txt_RequiredAmount.text = all_Archivements[index].missionCompeleteAmount[all_Archivements[index].currentLevel].ToString();
        list_ActiveAchivementData[index].slider_RewardComplate.maxValue = all_Archivements[index].missionCompeleteAmount[all_Archivements[index].currentLevel];
        list_ActiveAchivementData[index].txt_currentMissionAount.text = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index).ToString();
    }


    public void CheckIfAchievementRewardCollected(int index)
    {

        if (DataManager.Instance.GetArchivmentRewardState(index) == 0)
        {
           // Debug.Log("First False");

            list_ActiveAchivementData[index].isDailyMissionComplate = false;
            list_ActiveAchivementData[index].btn_Claim.gameObject.SetActive(false);
            //UIManager.instance.ui_HomePanel.ui_HomeScreen.img_RedDot.gameObject.SetActive(false);
        }
        else
        {
            list_ActiveAchivementData[index].isDailyMissionComplate = true;
            list_ActiveAchivementData[index].btn_Claim.gameObject.SetActive(true);
            UIManager.instance.ui_HomePanel.ui_HomeScreen.img_RedDot.gameObject.SetActive(true);

        }
    }


    public void IncreaseCurrentAchivementLevel(int index)
    {
        all_Archivements[index].currentLevel++;

        DataManager.Instance.SetArchivmentRewardState(index, 0);
        CheckIfAchievementRewardCollected(index);

        if (all_Archivements[index].currentLevel >= 5)
        {
            all_Archivements[index].currentLevel = 0;
            list_ActiveAchivementData.Remove(list_ActiveAchivementData[index]);
        }
    }

    public void IncreaseAchievementCurrentValue(int index, float _increaseAmount)
    {
        if (canIncreaseValueOfCurrentAchivement)
        {
            float currentValue = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index) + (int)_increaseAmount;
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index, (int)currentValue);
            list_ActiveAchivementData[index].txt_currentMissionAount.text = currentValue.ToString();
            list_ActiveAchivementData[index].slider_RewardComplate.value = currentValue;
            StartCoroutine(AchievementSlidlerAnimation(index));

            if (currentValue >= all_Archivements[index].missionCompeleteAmount[all_Archivements[index].currentLevel])
            {
                canIncreaseValueOfCurrentAchivement = false;
                list_ActiveAchivementData[index].isDailyMissionComplate = true;
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
            if (currentTime > list_ActiveAchivementData[index].slider_RewardComplate.value)
            {
                list_ActiveAchivementData[index].slider_RewardComplate.value++;
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
    }
}
