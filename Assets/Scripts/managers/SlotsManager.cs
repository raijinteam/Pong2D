using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Slot1State
{
    Empty,
    HasReward,
    TimerStart,
    RewardCollected
}

public enum Slot2State
{
    Empty,
    HasReward,
    TimerStart,
    RewardCollected
}

public class SlotsManager : MonoBehaviour
{
    public static SlotsManager Instance;


    [HideInInspector]
    public bool isFirstSlotEmpty;
    
    public bool isSecondSlotEmpty;
    [HideInInspector]
    public bool isThiredSlotEmpty;
    [HideInInspector]
    public bool isForthSlotEmpty;

    [HideInInspector]
    public bool isStartTimerSlot1;
    
    public bool isStartTimerSlot2;
    [HideInInspector]
    public bool isStartTimerSlot3;
    [HideInInspector]
    public bool isStartTimerSlot4;

    
    public bool isRewardGiveSlot1;
    
    public bool isRewardGiveSlot2;
    
    public bool isRewardGiveSlot3;
    
    public bool isRewardGiveSlot4;

    public float unlockTimeSlot1;
    public float unlockTimeSlot2;
    public float unlockTimeSlot3;
    public float unlockTimeSlot4;


    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        CheckForRewardGiveSlot1();
        CheckForRewardGiveSlot2();
        CheckForRewardGiveSlot3();
        CheckForRewardGiveSlot4();

        CheckForFirstSlotIsEmpty();
        CheckForSecondSlotIsEmpty();
        CheckForThiredSlotIsEmpty();
        CheckForForthSlotIsEmpty();


        CheckForStartTimeSlot1();
        CheckForStartTimeSlot2();
        CheckForStartTimeSlot3();
        CheckForStartTimeSlot4();
    }


    public void CheckForStartTimeSlot1()
    {
        isStartTimerSlot1 = false;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME) == 1)
            isStartTimerSlot1 = true;
    }
    public void CheckForStartTimeSlot2()
    {
        isStartTimerSlot2 = false;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 1) == 1)
            isStartTimerSlot2 = true;
    }
    public void CheckForStartTimeSlot3()
    {
        isStartTimerSlot3 = false;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 2) == 1)
            isStartTimerSlot3 = true;
    }
    public void CheckForStartTimeSlot4()
    {
        isStartTimerSlot4 = false;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 3) == 1)
            isStartTimerSlot4 = true;
    }



    public void CheckForFirstSlotIsEmpty()
    {
        isFirstSlotEmpty = true;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY) == 1)
            isFirstSlotEmpty = false;
    }

    public void CheckForSecondSlotIsEmpty()
    {
        isSecondSlotEmpty = true;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 1) == 1)
            isSecondSlotEmpty = false;
    }

    public void CheckForThiredSlotIsEmpty()
    {
        isThiredSlotEmpty = true;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 2) == 1)
            isThiredSlotEmpty = false;
    }

    public void CheckForForthSlotIsEmpty()
    {
        isForthSlotEmpty = true;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 3) == 1)
            isForthSlotEmpty = false;
    }



    public void CheckForRewardGiveSlot1()
    {
        isRewardGiveSlot1 = false;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE) == 1)
            isRewardGiveSlot1 = true;
    }

    public void CheckForRewardGiveSlot2()
    {
        isRewardGiveSlot2 = false;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 1) == 1)
            isRewardGiveSlot2 = true;
    }

    public void CheckForRewardGiveSlot3()
    {
        isRewardGiveSlot3 = false;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 2) == 1)
            isRewardGiveSlot3 = true;
    }

    public void CheckForRewardGiveSlot4()
    {
        isRewardGiveSlot4 = false;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 3) == 1)
            isRewardGiveSlot4 = true;
    }
}
