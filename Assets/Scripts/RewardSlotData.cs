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
    public int requireAmountForUnlock;
    public int numberOfRewards;
}
