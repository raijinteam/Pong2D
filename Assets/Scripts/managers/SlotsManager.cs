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
              //  Debug.Log("Set Reward Amount");
                allSlots[i].numberOfRewards = all_SlotData[GameManager.instance.currentLevelIndex].numberOfRewards;
                DataManager.Instance.SetRewardSlotIndex(i, GameManager.instance.currentLevelIndex);
                allSlots[i].requireAmountForUnlock = all_SlotData[GameManager.instance.currentLevelIndex].requireAmountForUnlock;
                allSlots[i].name = all_SlotData[GameManager.instance.currentLevelIndex].name;
                DataManager.Instance.SetSlotUnlockTimer(i, all_SlotData[GameManager.instance.currentLevelIndex].timer);
              //  Debug.Log("Slot TImer : " + allSlots[GameManager.instance.currentLevelIndex].currentTimer);
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

    public void SetSlotData(int index)
    {
        allSlots[index].minGoldReward = all_SlotData[GameManager.instance.currentLevelIndex].minCoinAmount;
        allSlots[index].maxGoldReward = all_SlotData[GameManager.instance.currentLevelIndex].maxCoinAmount;
    }

    public void SetAllRewardsData(int _index)
    {
        SetDataInList(_index);
    }

    private void SetDataInList(int index)
    {
        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
            {
                allSlots[index].list_RewardAmount.Add(allSlots[index].coinReward.ToString());
                allSlots[index].list_RewardIcons.Add(all_SlotData[DataManager.Instance.GetRewardSlotIndex(index)].all_RewardsIcons[0]);
            }
            else if (i == 1)
            {
                allSlots[index].list_RewardAmount.Add(allSlots[index].gemsReward.ToString());
               allSlots[index].list_RewardIcons.Add(all_SlotData[DataManager.Instance.GetRewardSlotIndex(index)].all_RewardsIcons[1]);
            }
            else if (i == 2)
            {
                allSlots[index].list_RewardAmount.Add(allSlots[index].skipItUpReward.ToString());
                allSlots[index].list_RewardIcons.Add(all_SlotData[DataManager.Instance.GetRewardSlotIndex(index)].all_RewardsIcons[2]);
            }
            else if(i == 3)
            {
                int reward = Random.Range(0, 20);
                allSlots[index].list_RewardAmount.Add(reward.ToString());
                allSlots[index].list_RewardIcons.Add(PlayerManager.Instance.all_Players[reward].image);
            }
            else if (i == 4)
            {
                int reward = Random.Range(0, 20);
                allSlots[index].list_RewardAmount.Add(reward.ToString());
                allSlots[index].list_RewardIcons.Add(PlayerManager.Instance.all_Players[reward].image);
            }
            else if (i == 5)
            {
                int reward = Random.Range(0, 20);
                allSlots[index].list_RewardAmount.Add(reward.ToString());
                allSlots[index].list_RewardIcons.Add(PlayerManager.Instance.all_Players[reward].image);
            }
            else if (i == 6)
            {
                int reward = Random.Range(0, 20);
                allSlots[index].list_RewardAmount.Add(reward.ToString());
                allSlots[index].list_RewardIcons.Add(PlayerManager.Instance.all_Players[reward].image);
            }
            else if (i == 7)
            {
                int reward = Random.Range(0, 20);
                allSlots[index].list_RewardAmount.Add(reward.ToString());
                allSlots[index].list_RewardIcons.Add(PlayerManager.Instance.all_Players[reward].image);
            }
        }

        for (int i = 0; i < all_SlotData[GameManager.instance.currentLevelIndex].all_RewardsIcons.Length; i++)
        {
            allSlots[index].list_RewardIcons.Add(all_SlotData[GameManager.instance.currentLevelIndex].all_RewardsIcons[i]);
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
        //Debug.Log("Gems AMount : " + probality);
        return probality;
    }

    private int ProbalityOfSkipItReward()
    {
        int levelIndex = GameManager.instance.currentLevelIndex;
        int probality = Random.Range(all_SlotData[levelIndex].minSkipItAmount, all_SlotData[levelIndex].maxSkipItAmount);
      //  Debug.Log("Skiptiup AMount : " + probality);
        return probality;
    }

}
