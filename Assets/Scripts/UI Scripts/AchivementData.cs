using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchivementData : MonoBehaviour
{

    [Header("Daily Mission SO")]
    [SerializeField] public AchievementSO achivementSO;

    [Header("Daily Mission Data")]

    public int index;
    public Image img_RewardIcon;
    public TextMeshProUGUI txt_Description;
    public TextMeshProUGUI txt_currentMissionAount; // how many points in current mission
    public TextMeshProUGUI txt_RequiredAmount; // how many points required to complate mission
    public TextMeshProUGUI txt_RewardAmount;
    [SerializeField] private Sprite img_DailyMissionComplate;
    public Slider slider_RewardComplate;
    public Button btn_Claim;

    public bool isDailyMissionComplate;



    public void OnClick_DailyMissionClaimButton()
    {
        AchievementManager.Instance.canIncreaseValueOfCurrentAchivement = true;
        UIManager.instance.ui_HomePanel.ui_HomeScreen.img_RedDot.gameObject.SetActive(false);
        UIManager.instance.ui_RewardSummary.SetRewardSummaryData(img_RewardIcon.sprite, txt_RewardAmount.text);
        UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index, 0);
        slider_RewardComplate.value = 0f;
        btn_Claim.gameObject.SetActive(false);
        AchievementManager.Instance.IncreaseCurrentAchivementLevel(index);
        AchievementManager.Instance.SetAchievementData(index);
    }
}
