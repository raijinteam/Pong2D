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

    private List<AchievementSO> listOfDailyMissionSO = new List<AchievementSO>();


    private List<AchivementData> list_ActiveAchivementData = new List<AchivementData>();

    private GameObject dailyMission;
    private AchivementData achievementData;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < all_Archivements.Length; i++)
        {
            listOfDailyMissionSO.Add(all_Archivements[i]);
        }
        ShuffleList(listOfDailyMissionSO);

        for (int i = 0; i < numberOfDailyMissions; i++)
        {
            dailyMission = Instantiate(pf_Achievement, transform.position, Quaternion.identity , parentOfAchivement);
            achievementData = dailyMission.GetComponent<AchivementData>();
            list_ActiveAchivementData.Add(achievementData);
            achievementData.slider_RewardComplate.value = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + i);
            achievementData.slider_RewardComplate.maxValue = all_Archivements[i].missionCompeleteAmount[all_Archivements[i].currentLevel];
            SetAchievementData(i);
        }
    }


    public void SetAchievementData(int index)
    {
        achievementData.img_RewardIcon.sprite = listOfDailyMissionSO[index].img_RewardIcon;
        achievementData.txt_Description.text = listOfDailyMissionSO[index].str_Description;
        achievementData.txt_RequiredAmount.text = listOfDailyMissionSO[index].missionCompeleteAmount[listOfDailyMissionSO[index].currentLevel].ToString();
        dailyMission.GetComponent<AchivementData>().txt_currentMissionAount.text = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index).ToString();
    }

    public void IncreaseAchievementCurrentValue(int index , float _increaseAmount)
    {
        float currentValue = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index) + (int)_increaseAmount;
        Debug.Log("Current Valye : " + currentValue);
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index, (int)currentValue);
        list_ActiveAchivementData[index].txt_currentMissionAount.text = currentValue.ToString();
        list_ActiveAchivementData[index].slider_RewardComplate.value = currentValue;
        StartCoroutine(AchievementSlidlerAnimation(index));
        if(currentValue >= all_Archivements[index].missionCompeleteAmount[all_Archivements[index].currentLevel])
        {
            all_Archivements[index].currentLevel++;
            SetAchievementData(index);
        }


    }


    private IEnumerator AchievementSlidlerAnimation(int index)
    {
        float currentTime = 0;
        float currentValue = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index);
        while (currentTime < 1)
        {
            currentTime = Time.deltaTime / 1;
            if(currentTime > list_ActiveAchivementData[index].slider_RewardComplate.value)
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
