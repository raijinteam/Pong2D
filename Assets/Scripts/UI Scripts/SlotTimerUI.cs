using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotTimerUI : MonoBehaviour
{

    public int index;
    [SerializeField] private TextMeshProUGUI txt_SlotName;
    [SerializeField] private Image img_SlotIcon;
    [SerializeField] private Image img_RewardIcon;
    [SerializeField] private TextMeshProUGUI txt_UnlockTime;
    [SerializeField] private Slider slider_Timer;
    [SerializeField] private TextMeshProUGUI txt_NumberOfRewards;
    [SerializeField] private TextMeshProUGUI txt_GemsForUnlockReward;
    [SerializeField] private TextMeshProUGUI txt_CommonCards;
    [SerializeField] private TextMeshProUGUI txt_RareCards;
    [SerializeField] private TextMeshProUGUI txt_EpicCards;
    [SerializeField] private TextMeshProUGUI txt_GoldAmount;
    [SerializeField] private int unlockGemsmultiplication = 10;
    private float unlockTime;





    [SerializeField] private int requireGemsForUnlock;

    private void Update()
    {
        CalculateUnlockTime(TimeManager.Instance.currentSlotTime[index]);
    }

    public void SetSlotTimerData(string _name, float _unlockTime, int _numberOfRewards, int _requireAmountForUnlock)
    {
        CalculateUnlockTime(_unlockTime);

        int slotIndex = DataManager.Instance.GetRewardSlotIndex(index);
        img_SlotIcon.sprite = SlotsManager.Instance.all_SlotData[slotIndex].icon;

        txt_SlotName.text = SlotsManager.Instance.all_SlotData[slotIndex].name;
        int minGold = SlotsManager.Instance.all_SlotData[slotIndex].minCoinAmount;
        int maxGold = SlotsManager.Instance.all_SlotData[slotIndex].maxCoinAmount;

        int minCommonCard = SlotsManager.Instance.all_SlotData[slotIndex].minCommonCard;
        int maxCommonCard = SlotsManager.Instance.all_SlotData[slotIndex].maxCommonCard;
        int minRareCard = SlotsManager.Instance.all_SlotData[slotIndex].minRareCard;
        int maxRareCard = SlotsManager.Instance.all_SlotData[slotIndex].maxRareCard;
        int minEpicCard = SlotsManager.Instance.all_SlotData[slotIndex].minEpicCard;
        int maxEpicCard = SlotsManager.Instance.all_SlotData[slotIndex].maxEpicCard;

        txt_GoldAmount.text = $"{minGold} - {maxGold}";
        txt_CommonCards.text = $"{minCommonCard} - {maxCommonCard}";
        txt_RareCards.text = $"{minRareCard} - {maxRareCard}";
        txt_EpicCards.text = $"{minEpicCard} - {maxEpicCard}";

    }

    private void CalculateUnlockTime(float unlockTime)
    {
        float hours = unlockTime / 3600;
        float minutes = (unlockTime % 3600) / 60;
        float seconds = unlockTime % 60;

        txt_UnlockTime.text = $"{(int)hours} : {(int)minutes} : {(int)seconds}";

        slider_Timer.maxValue = SlotsManager.Instance.all_SlotData[DataManager.Instance.GetRewardSlotIndex(index)].timer;
        slider_Timer.value = seconds;

        CalculateRequireGemsAmount(hours);
    }

    private void CalculateRequireGemsAmount(float hours)
    {
        if (hours > 0)
        {
            requireGemsForUnlock = (int)hours * unlockGemsmultiplication;
        }
        else
        {
            requireGemsForUnlock = unlockGemsmultiplication;
        }
        txt_GemsForUnlockReward.text = requireGemsForUnlock.ToString();
    }

    public void OnClick_CompleteTimeWithGems()
    {

        if(DataManager.Instance.isGameFirstTimeLoad && index == 0)
        {
            SlotsManager.Instance.allSlots[0].slotState = SlotState.RewardGenrated;
            UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
            UIManager.instance.ui_Tutorial.toutorialState = TutorialState.CollecteChestRewards;
            UIManager.instance.ui_Tutorial.gameObject.SetActive(true);
            UIManager.instance.ui_SlotTimer.gameObject.SetActive(false);
        }
        else
        {
            if (!DataManager.Instance.IsEnoughGemssForPurchase(requireGemsForUnlock))
                return;

            DataManager.Instance.SetRewardSlotState(index, SlotState.RewardGenrated);
            UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
            TimeManager.Instance.currentSlotTime[index] = SlotsManager.Instance.allSlots[index].currentTimer;
            DataManager.Instance.DecreaseGems(requireGemsForUnlock);
            SlotsManager.Instance.isAnotherSlotHasActiveTime = false;
            this.gameObject.SetActive(false);
        }


       
    }

    public void OnClick_ReduceTimeWithAds()
    {
        if (!DataManager.Instance.isGameFirstTimeLoad)
        {
            TimeManager.Instance.currentSlotTime[index] -= (3 * 3600);

        }

    }


    public void OnClick_Close()
    {
        if (DataManager.Instance.isGameFirstTimeLoad)
        {
            this.gameObject.SetActive(false);

        }
    }
}
