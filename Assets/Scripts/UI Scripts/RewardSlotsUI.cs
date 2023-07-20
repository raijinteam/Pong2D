using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSlotsUI : MonoBehaviour
{
    public SlotUI[] all_RewardSlots;

    [SerializeField] private Sprite sprite_Empty_Slot;
    [SerializeField] private Sprite sprite_HasReward;
    [SerializeField] private Sprite sprite_TimeStart;
    [SerializeField] private Sprite sprite_RewardGenrated;

    private void Start()
    {
        SetSlotsDataWhenChangeState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SlotsManager.Instance.GiveSlotIfEmpty();
            SetSlotsDataWhenChangeState();
        }

        //Time Calculation
        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            if (SlotsManager.Instance.allSlots[i].slotState == SlotState.TimerStart)
            {
                all_RewardSlots[i].SetSlotTime(TimeManager.Instance.currentSlotTime[i]);
                all_RewardSlots[i].img_BG.sprite = sprite_TimeStart;
            }
        }
    }

    public void SetSlotsDataWhenChangeState()
    {
        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {

            if(SlotsManager.Instance.allSlots[i].slotState == SlotState.Empty)
            {
                all_RewardSlots[i].img_BG.sprite = sprite_Empty_Slot;
                all_RewardSlots[i].EmptySlot();
                DataManager.Instance.SetRewardSlotState(i, SlotState.Empty);
            }

            if (SlotsManager.Instance.allSlots[i].slotState == SlotState.HasReward)
            {
                all_RewardSlots[i].ShowAllObjects();
                all_RewardSlots[i].img_BG.sprite = sprite_HasReward;
                all_RewardSlots[i].txt_SlotTimer.text = SlotsManager.Instance.allSlots[i].timer.ToString();
            }else if (SlotsManager.Instance.allSlots[i].slotState == SlotState.TimerStart)
            {
                SlotsManager.Instance.isAnotherSlotHasActiveTime = true;
                all_RewardSlots[i].SetSlotTime(TimeManager.Instance.currentSlotTime[i]);
                all_RewardSlots[i].img_BG.sprite = sprite_TimeStart;
            }else if (SlotsManager.Instance.allSlots[i].slotState == SlotState.RewardGenrated)
            {
                all_RewardSlots[i].img_BG.sprite = sprite_RewardGenrated;
                all_RewardSlots[i].txt_SlotName.text = "Open";
                all_RewardSlots[i].txt_SlotTimer.text = "0";
            }
        }
    }


    public void OnClick_SlotStartTime(int index)
    {

        if(SlotsManager.Instance.allSlots[index].slotState == SlotState.HasReward && !SlotsManager.Instance.isAnotherSlotHasActiveTime)
        {
            SlotsManager.Instance.isAnotherSlotHasActiveTime = true;
            DataManager.Instance.SetRewardSlotState(index, SlotState.TimerStart);
            SetSlotsDataWhenChangeState();
        }
        else if(SlotsManager.Instance.allSlots[index].slotState == SlotState.RewardGenrated)
        {
            UIManager.instance.ui_RewardSummary.SetRewardSummaryData(all_RewardSlots[index].img_SlotIcon.sprite , 10.ToString());
            UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
            all_RewardSlots[index].EmptySlot();
            DataManager.Instance.SetRewardSlotState(index, SlotState.Empty);
            SetSlotsDataWhenChangeState();
        }
        else if(SlotsManager.Instance.allSlots[index].slotState == SlotState.TimerStart)
        {
            Sprite rewardSprite = all_RewardSlots[index].img_SlotIcon.sprite;
            string name = all_RewardSlots[index].txt_SlotName.text;
            int numberOFRewards = SlotsManager.Instance.allSlots[index].numberOfRewards;
            int requireAmountForUnlock = SlotsManager.Instance.allSlots[index].requireAmountForUnlock;
            float unlockTime = TimeManager.Instance.currentSlotTime[index];

            UIManager.instance.ui_SlotTimer.index = index;
            UIManager.instance.ui_SlotTimer.SetSlotTimerData(name, rewardSprite, unlockTime, numberOFRewards, requireAmountForUnlock) ;
            UIManager.instance.ui_SlotTimer.gameObject.SetActive(true);
        }
    }
}
