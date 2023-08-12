using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RewardSlotsUI : MonoBehaviour
{
    public SlotUI[] all_RewardSlots;


    [SerializeField] private bool IsBagTimerRunning = false;
    [SerializeField] private int bagIndex = 0;

    private void Start()
    {
        SetSlotsDataWhenChangeState();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha2))
            GameManager.instance.currentLevelIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            GameManager.instance.currentLevelIndex = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            GameManager.instance.currentLevelIndex = 3;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SlotsManager.Instance.GiveSlotIfEmpty();
            SetSlotsDataWhenChangeState();
        }


        if (IsBagTimerRunning)
        {
            all_RewardSlots[bagIndex].SetSlotTime(TimeManager.Instance.currentSlotTime[bagIndex]);
        }
    }

    public void SetSlotsDataWhenChangeState()
    {
        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {

            if(SlotsManager.Instance.allSlots[i].slotState == SlotState.Empty)
            {
                //Debug.Log("Slot Empty");
                //all_RewardSlots[i].img_BG.sprite = sprite_Empty_Slot;
                all_RewardSlots[i].EmptySlot();
                DataManager.Instance.SetRewardSlotState(i, SlotState.Empty);
            }

            if (SlotsManager.Instance.allSlots[i].slotState == SlotState.HasReward)
            {
                // Debug.Log("Slot Filled");
                int slotIndex = DataManager.Instance.GetRewardSlotIndex(i);
                all_RewardSlots[i].ShowAllObjects();
                all_RewardSlots[i].SetSlotFilled();
                all_RewardSlots[i].img_SlotClick.DOAnchorPos(new Vector2(0, 30), 0.5f).SetLoops(-1 , LoopType.Yoyo); 
                //all_RewardSlots[i].img_BG.sprite = sprite_HasReward;
                all_RewardSlots[i].txt_FilledSlotTimer.text = SlotsManager.Instance.allSlots[slotIndex].currentTimer.ToString();
            }else if (SlotsManager.Instance.allSlots[i].slotState == SlotState.TimerStart)
            {
                //Debug.Log("Slot Running");
                IsBagTimerRunning = true;
                bagIndex = i;
                SlotsManager.Instance.isAnotherSlotHasActiveTime = true;
                all_RewardSlots[i].SetSlotRunning();
                all_RewardSlots[i].SetSlotTime(TimeManager.Instance.currentSlotTime[i]);
                //all_RewardSlots[i].img_BG.sprite = sprite_TimeStart;
            }else if (SlotsManager.Instance.allSlots[i].slotState == SlotState.RewardGenrated)
            {
               // Debug.Log("Slot Finished");
                SlotsManager.Instance.slotFinishedIndex = bagIndex;
                IsBagTimerRunning = false;
                //all_RewardSlots[i].img_BG.sprite = sprite_RewardGenrated;
                all_RewardSlots[i].SetSlotFinished();
            }
        }
    }



    public void OnClick_SlotStartTime(int index)
    {
       // Debug.Log("Sloyt State : " + SlotsManager.Instance.allSlots[index].slotState);
        Debug.Log("Clicked");
        if(SlotsManager.Instance.allSlots[index].slotState == SlotState.HasReward && !SlotsManager.Instance.isAnotherSlotHasActiveTime)
        {
            SlotsManager.Instance.isAnotherSlotHasActiveTime = true;
            DataManager.Instance.SetRewardSlotState(index, SlotState.TimerStart);
            SetSlotsDataWhenChangeState();
        }
        else if(SlotsManager.Instance.allSlots[index].slotState == SlotState.RewardGenrated)
        {
            //   Debug.Log("Reward Genrated");
            SlotsManager.Instance.SetAllRewardsData(index);
            UIManager.instance.ui_Reward.gameObject.SetActive(true);
            all_RewardSlots[index].EmptySlot();
            DataManager.Instance.SetRewardSlotState(index, SlotState.Empty);
            SetSlotsDataWhenChangeState();
        }
        else if(SlotsManager.Instance.allSlots[index].slotState == SlotState.TimerStart)
        {
            int numberOFRewards = SlotsManager.Instance.allSlots[GameManager.instance.currentLevelIndex].numberOfRewards;
            int requireAmountForUnlock = SlotsManager.Instance.allSlots[index].requireAmountForUnlock;
            float unlockTime = TimeManager.Instance.currentSlotTime[index];

            UIManager.instance.ui_SlotTimer.index = index;
            string name = SlotsManager.Instance.allSlots[index].name;
            UIManager.instance.ui_SlotTimer.SetSlotTimerData(name, unlockTime, numberOFRewards, requireAmountForUnlock) ;
            UIManager.instance.ui_SlotTimer.gameObject.SetActive(true);
        }
    }
}
