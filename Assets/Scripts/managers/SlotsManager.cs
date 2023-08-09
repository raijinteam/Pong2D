using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SlotsManager : MonoBehaviour
{
    public static SlotsManager Instance;

    public bool isAnotherSlotHasActiveTime;
    public int slotFinishedIndex;

    private void Awake()
    {
        Instance = this;
    }

    public SlotData[] all_SlotData;

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
                Debug.Log("Set Reward Amount");
                allSlots[i].numberOfRewards = all_SlotData[GameManager.instance.currentLevelIndex].numberOfRewards;
                allSlots[i].requireAmountForUnlock = all_SlotData[GameManager.instance.currentLevelIndex].requireAmountForUnlock;
                allSlots[i].name = all_SlotData[GameManager.instance.currentLevelIndex].name;
                DataManager.Instance.SetSlotUnlockTimer(i, all_SlotData[GameManager.instance.currentLevelIndex].timer);
                Debug.Log("Slot TImer : " + allSlots[GameManager.instance.currentLevelIndex].timer);
                TimeManager.Instance.SetSlotTimeData(i, DataManager.Instance.GetSlotUnlockTime(i));
                DataManager.Instance.SetRewardSlotState(i, SlotState.HasReward);
                UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
                allSlots[i].coinReward = ProbalityOfCoinsReward();
                allSlots[i].gemsReward = ProbalityOfGemsReward();
                allSlots[i].skipItUpReward = ProbalityOfSkipItReward();
                break;
            }
        }
    }


    private int ProbalityOfCoinsReward()
    {
        int levelIndex = GameManager.instance.currentLevelIndex;
        int probality = Random.Range(all_SlotData[levelIndex].minCoinAmount, all_SlotData[levelIndex].maxCoinAmount);
        return probality;
    }

    private int ProbalityOfGemsReward()
    {
        int levelIndex = GameManager.instance.currentLevelIndex;
        int probality = Random.Range(all_SlotData[levelIndex].minGemsAmount, all_SlotData[levelIndex].maxGemsAmount);
        return probality;
    }

    private int ProbalityOfSkipItReward()
    {
        int levelIndex = GameManager.instance.currentLevelIndex;
        int probality = Random.Range(all_SlotData[levelIndex].minSkipItAmount, all_SlotData[levelIndex].maxSkipItAmount);
        return probality;
    }

}
