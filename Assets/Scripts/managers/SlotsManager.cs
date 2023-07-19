using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DefaultExecutionOrder(-1)]
public class SlotsManager : MonoBehaviour
{
    public static SlotsManager Instance;


    public bool isAnyotherSlotRunningTime;

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
        if (!isAnyotherSlotRunningTime)
        {
            for (int i = 0; i < allSlots.Length; i++)
            {
                if (allSlots[i].slotState == SlotState.Empty)
                {
                    Debug.Log("Give SLot");
                    DataManager.Instance.SetRewardSlotState(i, SlotState.HasReward);
                    UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
                    break;
                }
            }
        }
       
    }

  /*  public void SetSlotsDataWhenChangeState()
    {
        for (int i = 0; i < allSlots.Length; i++)
        {

            if (allSlots[i].slotState == SlotState.HasReward)
            {
                all_RewardSlots[i].ShowAllObjects();
                all_RewardSlots[i].img_BG.color = Color.red;
                all_RewardSlots[i].txt_SlotTimer.text = SlotsManager.Instance.allSlots[i].timer.ToString();
            }

            if (SlotsManager.Instance.allSlots[i].slotState == SlotState.TimerStart)
            {
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
    }*/
   
}
