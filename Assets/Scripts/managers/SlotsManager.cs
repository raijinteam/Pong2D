using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlotsManager : MonoBehaviour
{
    public static SlotsManager Instance;

    public bool isAnotherSlotHasActiveTime;

    private void Awake()
    {
        Instance = this;
    }


    public RewardSlotData[] allSlots;
    public void SetSlotState(int _slotIndex, string _slotState)
    {
        allSlots[_slotIndex].slotState = (SlotState)Enum.Parse(typeof(SlotState), _slotState);
    }



    public void GiveSlotIfEmpty()
    {
        for (int i = 0; i < allSlots.Length; i++)
        {
            if (allSlots[i].slotState == SlotState.Empty)
            {
                DataManager.Instance.SetSlotUnlockTimer(i, allSlots[GameManager.instance.currentLevelIndex].timer);
                Debug.Log("Slot TImer : " + allSlots[GameManager.instance.currentLevelIndex].timer);
                TimeManager.Instance.SetSlotTimeData(i, DataManager.Instance.GetSlotUnlockTime(i));
                DataManager.Instance.SetRewardSlotState(i, SlotState.HasReward);
                UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
                break;
            }
        }
    }


}
