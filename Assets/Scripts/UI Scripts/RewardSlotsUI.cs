using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSlotsUI : MonoBehaviour
{
    public SlotUI[] all_RewardSlots;



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

        //Slot Timer
        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            if (SlotsManager.Instance.allSlots[i].slotState == SlotState.TimerStart)
            {
                all_RewardSlots[i].SetSlotTime(TimeManager.Instance.currentSlotTime[i]);
                all_RewardSlots[i].img_BG.color = Color.green;
            }
        }


    }

    public void SetSlotsDataWhenChangeState()
    {
        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            Debug.Log("In Loop");
            if (SlotsManager.Instance.allSlots[i].slotState == SlotState.HasReward)
            {
                Debug.Log("In If");
                SlotsManager.Instance.isAnyotherSlotRunningTime = true;
                all_RewardSlots[i].ShowAllObjects();
                all_RewardSlots[i].img_BG.color = Color.red;
                all_RewardSlots[i].txt_SlotTimer.text = SlotsManager.Instance.allSlots[i].timer.ToString();
            }

            if (SlotsManager.Instance.allSlots[i].slotState == SlotState.TimerStart)
            {
                SlotsManager.Instance.isAnyotherSlotRunningTime = true;
                all_RewardSlots[i].SetSlotTime(TimeManager.Instance.currentSlotTime[i]);
                all_RewardSlots[i].img_BG.color = Color.green;
            }

            if (SlotsManager.Instance.allSlots[i].slotState == SlotState.RewardGenrated)
            {
                all_RewardSlots[i].img_BG.color = Color.yellow;
                all_RewardSlots[i].txt_SlotName.text = "Open";
                all_RewardSlots[i].txt_SlotTimer.text = "0";
            }
        }
    }


    public void OnClick_SlotStartTime(int index)
    {

        if(SlotsManager.Instance.allSlots[index].slotState == SlotState.HasReward)
        {
            DataManager.Instance.SetRewardSlotState(index, SlotState.TimerStart);
            SetSlotsDataWhenChangeState();
        }
        else if(SlotsManager.Instance.allSlots[index].slotState == SlotState.RewardGenrated)
        {
            all_RewardSlots[index].EmptySlot();
            DataManager.Instance.SetRewardSlotState(index, SlotState.Empty);
            SlotsManager.Instance.isAnyotherSlotRunningTime = false;
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
