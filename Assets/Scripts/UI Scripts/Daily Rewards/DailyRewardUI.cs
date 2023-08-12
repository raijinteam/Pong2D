using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DailyRewardUI : MonoBehaviour
{
    [Header("Daily Reward Data")]
    [SerializeField] private int slotIndex;
    [SerializeField] private DayRewardData[] all_DayRewardData;

    [SerializeField] private DailyRewardButtonUI[] allDaysRewardButtons;
    [SerializeField] private TextMeshProUGUI txt_Timer;

    [SerializeField] private int index = 0;

    private List<DailyRewardButtonUI> dailyRewardButtons = new List<DailyRewardButtonUI>();


    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private List<string> amount = new List<string>();

    private void OnEnable()
    {


        for (int i = 0; i < all_DayRewardData.Length; i++)
        {
            allDaysRewardButtons[i].img_Icon.sprite = all_DayRewardData[i].icon;
            allDaysRewardButtons[i].rewardAmount = all_DayRewardData[i].amount;
            allDaysRewardButtons[i].txt_RewardAmount.text = allDaysRewardButtons[i].rewardAmount.ToString();
        }
        for (int i = 0; i < SlotsManager.Instance.all_SlotData[slotIndex].numberOfRewards; i++)
        {
            sprites.Add(SlotsManager.Instance.all_SlotData[slotIndex].all_RewardsIcons[i]);
            amount.Add("0");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        index = DataManager.Instance.GetDayCount();


        for (int i = 0; i < allDaysRewardButtons.Length; i++)
        {
            allDaysRewardButtons[i].GetComponent<Button>().interactable = false;
            allDaysRewardButtons[i].img_ClimedReward.gameObject.SetActive(false);
            allDaysRewardButtons[i].activeReward.gameObject.SetActive(false);
        }

        //Set Clamied Reward
        for (int i = 0; i < index; i++)
        {
            allDaysRewardButtons[i].img_ClimedReward.gameObject.SetActive(true);
        }

        SetActiveDailyReward();

    }


    private void Update()
    {
        if (DataManager.Instance.GetDailyRewardActiveTimeForBool() == 1)
        {
            int timeInSeconds = (int)TimeManager.Instance.currentDailyRewardTime;

            float hours = timeInSeconds / 3600;
            float minutes = (timeInSeconds % 3600) / 60;
            float seconds = timeInSeconds % 60;

            txt_Timer.text = $"{(int)hours} : {(int)minutes} : {(int)seconds}";
        }
        else
        {
            if (DataManager.Instance.GetDailyRewardActiveTimeForBool() == 0)
            {
                if (index >= allDaysRewardButtons.Length)
                {
                    Debug.Log("Day 7 reward Claimed");
                    ResetToRewardDayOne();
                }
                Debug.Log("Day index : " + index);
                SetActiveDailyReward();
                allDaysRewardButtons[index].GetComponent<Button>().interactable = true;
            }
        }
    }

    private void ResetToRewardDayOne()
    {
        DataManager.Instance.SetDayCount(0);
        index = DataManager.Instance.GetDayCount();
        for (int i = 0; i < allDaysRewardButtons.Length; i++)
        {
            allDaysRewardButtons[i].GetComponent<Button>().interactable = false;
            allDaysRewardButtons[i].img_ClimedReward.gameObject.SetActive(false);
            allDaysRewardButtons[i].activeReward.gameObject.SetActive(false);
        }

        //Set Clamied Reward
        for (int i = 0; i < index; i++)
        {
            allDaysRewardButtons[i].img_ClimedReward.gameObject.SetActive(true);
        }

        SetActiveDailyReward();
    }

    public void SetActiveDailyReward()
    {

        if (DataManager.Instance.GetDailyRewardActiveTimeForBool() == 0)
        {
            allDaysRewardButtons[index].activeReward.gameObject.SetActive(true);
            allDaysRewardButtons[index].activeRewardHeader.DOAnchorPos(new Vector2(0, 20), 0.5f).SetLoops(-1, LoopType.Yoyo);
            allDaysRewardButtons[index].GetComponent<Button>().interactable = true;
        }
        else
        {
            allDaysRewardButtons[index].activeReward.gameObject.SetActive(false);
            allDaysRewardButtons[index].GetComponent<Button>().interactable = false;
        }
    }

    public void OnClick_ActiveNextRewardButton(int _buttonIndex)
    {

        if (_buttonIndex == index && index <= allDaysRewardButtons.Length - 1)
        {
            if (all_DayRewardData[index].rewardType == RewardType.Creats)
            {
                UIManager.instance.ui_RewardSummary.SetMultiplRewardSummaryData(sprites, amount);
                UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
                Debug.Log("Clicked on create");
            }
            else if (all_DayRewardData[index].rewardType == RewardType.Coins)
            {
                int amount = all_DayRewardData[index].amount;
                DataManager.Instance.IncreaseCoins(amount);
            }
            else if (all_DayRewardData[index].rewardType == RewardType.Gems)
            {
                int amount = all_DayRewardData[index].amount;
                DataManager.Instance.IncreaseGems(amount);
            }
            else if (all_DayRewardData[index].rewardType == RewardType.Skipitups)
            {
                int amount = all_DayRewardData[index].amount;
                DataManager.Instance.IncreaseSkipIt(amount);
            }



            DataManager.Instance.SetDailyRewardActiveTime(1);
            GameManager.instance.CheckForDailyRewardTImeIsStart();
            allDaysRewardButtons[index].GetComponent<Button>().interactable = false;
            allDaysRewardButtons[index].img_ClimedReward.gameObject.SetActive(true);
            allDaysRewardButtons[index].activeReward.gameObject.SetActive(false);

            //Set Multiple reward in reward summary
            if (index >= allDaysRewardButtons.Length)
            {
                /*int childCount = allDaysRewardButtons[index].transform.GetChild(0).childCount;

                for (int i = 0; i < childCount; i++)
                {
                    sprites.Add(allDaysRewardButtons[index].transform.GetChild(0).transform.GetChild(i).GetComponent<Image>().sprite);

                }*/


                ResetToRewardDayOne();
            }
            else
            {

                UIManager.instance.ui_RewardSummary.SetRewardSummaryData(allDaysRewardButtons[index].img_Icon.sprite, allDaysRewardButtons[index].txt_RewardAmount.text);
                UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);

                if (index <= allDaysRewardButtons.Length)
                {
                    index++;
                    DataManager.Instance.SetDayCount(index);
                    allDaysRewardButtons[index].activeReward.gameObject.SetActive(false);

                }
            }


            //print(index);
        }
    }


    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }

}

public enum RewardType
{
    Coins,
    Gems,
    Skipitups,
    Creats
}

[System.Serializable]
public struct DayRewardData
{
    public RewardType rewardType;
    public Sprite icon;
    public int amount;
}
