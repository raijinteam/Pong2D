using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SlotsManager : MonoBehaviour
{
    public static SlotsManager Instance;


    public RewardSlotData[] allSlots;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < allSlots.Length; i++)
        {
            string state = PlayerPrefs.GetString(PlayerPrefsKeys.KEY_REWARD_SLOT_STATE + i);
            allSlots[i].slotState = (SlotState)Enum.Parse(typeof(SlotState), state);
            //PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i, allSlots[i].timer);
        }
    }
}
