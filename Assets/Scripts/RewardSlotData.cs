using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum SlotState
{
    Empty,
    HasReward,
    TimerStart,
    RewardGenrated
}


public class RewardSlotData : MonoBehaviour
{

    public SlotState slotState;
    public string name;
    public Sprite icon;
    public float timer;
    public float currentTimer;
    public int requireAmountForUnlock;
    public int numberOfRewards;
    public int coinReward;
    public int gemsReward;
    public int skipItUpReward;

    public int minGoldReward;
    public int maxGoldReward;


    public int minCommonCard;
    public int maxCommonCard;
    public int minRareCard;
    public int maxRareCard;
    public int minEpicCard;
    public int maxEpicCard;


    public List<string> list_RewardAmount = new List<string>();
    public List<Sprite> list_RewardIcons = new List<Sprite>();
}
