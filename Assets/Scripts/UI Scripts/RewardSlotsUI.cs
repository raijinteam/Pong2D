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
       
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
            {
                if (SlotsManager.Instance.allSlots[i].slotState == SlotState.Empty)
                {
                    DataManager.Instance.SetRewardSlotState(i, SlotState.HasReward);
                    break;
                }
            }
        }

        for(int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {

            if(SlotsManager.Instance.allSlots[i].slotState == SlotState.HasReward)
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

    }


    public void OnClick_SlotStartTime(int index)
    {

        if(SlotsManager.Instance.allSlots[index].slotState == SlotState.HasReward)
        {
            DataManager.Instance.SetRewardSlotState(index, SlotState.TimerStart);
        }else if(SlotsManager.Instance.allSlots[index].slotState == SlotState.RewardGenrated)
        {
            all_RewardSlots[index].EmptySlot();
            DataManager.Instance.SetRewardSlotState(index, SlotState.Empty);
        }

    }
}
