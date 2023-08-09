using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum SlotType
{
    basic,
    grand,
    rare,
    elite
}


public class SlotData : MonoBehaviour
{
    public SlotType slotType;
    public string name;
    public Sprite icon;
    public float timer;
    public int requireAmountForUnlock;
    public int numberOfRewards;
    public int minCoinAmount;
    public int maxCoinAmount;
    public int minGemsAmount;
    public int maxGemsAmount;
    public int minSkipItAmount;
    public int maxSkipItAmount;
}
