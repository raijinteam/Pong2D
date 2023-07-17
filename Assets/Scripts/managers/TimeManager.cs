using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    public float slotTimer1;
    public float slotTimer2;
    public float slotTimer3;
    public float slotTimer4;

    [Space]
    public float gameQuitTime;

    public bool isFirstSlotTimeOver;
    public bool isSecondSlotTimeOver;
    public bool isThiredSlotTimeOver;
    public bool isForthSlotTimeOver;

    public float currentUnlockTimeSlot1;
    public float currentUnlockTimeSlot2;
    public float currentUnlockTimeSlot3;
    public float currentUnlockTimeSlot4;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Check For time WHen Game is NOt Running

        if (!SlotsManager.Instance.isFirstSlotEmpty && SlotsManager.Instance.isStartTimerSlot1)
        {
            currentUnlockTimeSlot1 = PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME);
            ScreenOffTimeForSlot1();
        }
        else
        {
            currentUnlockTimeSlot1 = SlotsManager.Instance.unlockTimeSlot1;
        }

        if (!SlotsManager.Instance.isSecondSlotEmpty && SlotsManager.Instance.isStartTimerSlot2)
        {
            currentUnlockTimeSlot2 = PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + 1);
            ScreenOffTimeForSlot2();
        }
        else
        {
            currentUnlockTimeSlot2 = SlotsManager.Instance.unlockTimeSlot2;
        }

        if (!SlotsManager.Instance.isThiredSlotEmpty && SlotsManager.Instance.isStartTimerSlot3)
        {
            currentUnlockTimeSlot3 = PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + 2);
            ScreenOffTimeForSlot3();
        }
        else
        {
            currentUnlockTimeSlot3 = SlotsManager.Instance.unlockTimeSlot3;
        }

        if (!SlotsManager.Instance.isForthSlotEmpty && SlotsManager.Instance.isStartTimerSlot4)
        {
            currentUnlockTimeSlot4 = PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + 3);
            ScreenOffTimeForSlot4();
        }
        else
        {
            currentUnlockTimeSlot4 = SlotsManager.Instance.unlockTimeSlot4;
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateFirstSlotTime();
        CalculateSecondSlotTime();
        CalculateThiredSlotTime();
        CalculateForthSlotTime();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME , currentUnlockTimeSlot1);
        PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + 1 , currentUnlockTimeSlot2);
        PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + 2, currentUnlockTimeSlot3);
        PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + 3, currentUnlockTimeSlot4);
        DateTime gameQuitTime = DateTime.Now;
        PlayerPrefs.SetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME, gameQuitTime.ToString()); 
    }

    #region Calculate Time When Game is Not Running
    //Calculate Time When Game is Not Running

    private void ScreenOffTimeForSlot1()
    {
        string gameQuitTimeString = PlayerPrefs.GetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME);

        if (!gameQuitTimeString.Equals(""))
        {
            DateTime quitTime = DateTime.Parse(gameQuitTimeString);
            DateTime currentTime = DateTime.Now;

            if (currentTime > quitTime)
            {
                TimeSpan timeSpan = currentTime - quitTime;

                Debug.Log("Total Quit Seconds : " + (float)timeSpan.TotalSeconds);

                if (currentUnlockTimeSlot1 > (float)timeSpan.TotalSeconds)
                {
                    float totalTime = currentUnlockTimeSlot1 - (float)timeSpan.TotalSeconds;
                    Debug.Log("Total Time : " + totalTime);
                    currentUnlockTimeSlot1 = totalTime;
                }
                else
                {
                    Debug.Log("Reward Unlocked");
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME, 0);
                    SlotsManager.Instance.CheckForStartTimeSlot1();
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY, 0);
                    SlotsManager.Instance.CheckForFirstSlotIsEmpty();
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE, 1);
                    SlotsManager.Instance.CheckForRewardGiveSlot1();
                    currentUnlockTimeSlot1 = SlotsManager.Instance.unlockTimeSlot1;
                }
            }
        }
    }

    private void ScreenOffTimeForSlot2()
    {
        string gameQuitTimeString = PlayerPrefs.GetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME);

        if (!gameQuitTimeString.Equals(""))
        {
            DateTime quitTime = DateTime.Parse(gameQuitTimeString);
            DateTime currentTime = DateTime.Now;

            if (currentTime > quitTime)
            {
                TimeSpan timeSpan = currentTime - quitTime;

                Debug.Log("Total Quit Seconds : " + (float)timeSpan.TotalSeconds);

                if (currentUnlockTimeSlot2 > (float)timeSpan.TotalSeconds)
                {
                    float totalTime = currentUnlockTimeSlot2 - (float)timeSpan.TotalSeconds;
                    Debug.Log("Total Time : " + totalTime);
                    currentUnlockTimeSlot2 = totalTime;
                }
                else
                {
                    Debug.Log("Reward Unlocked");
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 1, 0);
                    SlotsManager.Instance.CheckForStartTimeSlot2();
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 1, 0);
                    SlotsManager.Instance.CheckForSecondSlotIsEmpty();
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 1, 1);
                    SlotsManager.Instance.CheckForRewardGiveSlot2();
                    currentUnlockTimeSlot2 = SlotsManager.Instance.unlockTimeSlot2;
                }
            }
        }
    }

    private void ScreenOffTimeForSlot3()
    {
        string gameQuitTimeString = PlayerPrefs.GetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME);

        if (!gameQuitTimeString.Equals(""))
        {
            DateTime quitTime = DateTime.Parse(gameQuitTimeString);
            DateTime currentTime = DateTime.Now;

            if (currentTime > quitTime)
            {
                TimeSpan timeSpan = currentTime - quitTime;

                Debug.Log("Total Quit Seconds : " + (float)timeSpan.TotalSeconds);

                if (currentUnlockTimeSlot2 > (float)timeSpan.TotalSeconds)
                {
                    float totalTime = currentUnlockTimeSlot3 - (float)timeSpan.TotalSeconds;
                    Debug.Log("Total Time : " + totalTime);
                    currentUnlockTimeSlot3 = totalTime;
                }
                else
                {
                    Debug.Log("Reward Unlocked");
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 2, 0);
                    SlotsManager.Instance.CheckForStartTimeSlot3();
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 2, 0);
                    SlotsManager.Instance.CheckForThiredSlotIsEmpty();
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 2, 1);
                    SlotsManager.Instance.CheckForRewardGiveSlot3();
                    currentUnlockTimeSlot3 = SlotsManager.Instance.unlockTimeSlot3;
                }
            }
        }
    }

    private void ScreenOffTimeForSlot4()
    {
        string gameQuitTimeString = PlayerPrefs.GetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME);

        if (!gameQuitTimeString.Equals(""))
        {
            DateTime quitTime = DateTime.Parse(gameQuitTimeString);
            DateTime currentTime = DateTime.Now;

            if (currentTime > quitTime)
            {
                TimeSpan timeSpan = currentTime - quitTime;

                Debug.Log("Total Quit Seconds : " + (float)timeSpan.TotalSeconds);

                if (currentUnlockTimeSlot4 > (float)timeSpan.TotalSeconds)
                {
                    float totalTime = currentUnlockTimeSlot4 - (float)timeSpan.TotalSeconds;
                    Debug.Log("Total Time : " + totalTime);
                    currentUnlockTimeSlot4 = totalTime;
                }
                else
                {
                    Debug.Log("Reward Unlocked");
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 3, 0);
                    SlotsManager.Instance.CheckForStartTimeSlot3();
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 3, 0);
                    SlotsManager.Instance.CheckForForthSlotIsEmpty();
                    PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 3, 1);
                    SlotsManager.Instance.CheckForRewardGiveSlot4();
                    currentUnlockTimeSlot4 = SlotsManager.Instance.unlockTimeSlot4;
                }
            }
        }
    }

    #endregion

    //Calculate time when game is running
    private void CalculateFirstSlotTime()
    {
        bool isTimerStart = SlotsManager.Instance.isStartTimerSlot1;
        
        if(!isFirstSlotTimeOver && isTimerStart)
        {
            //Calculate Time When Game is Running
            currentUnlockTimeSlot1 -= Time.deltaTime;
            if(currentUnlockTimeSlot1 <= 0)
            {
                //Give Reward
                Debug.Log("Slot Unlocked");
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME, 0);
                SlotsManager.Instance.CheckForStartTimeSlot1();
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY, 0);
                SlotsManager.Instance.CheckForFirstSlotIsEmpty();
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE, 1);
                SlotsManager.Instance.CheckForRewardGiveSlot1();
                currentUnlockTimeSlot1 = SlotsManager.Instance.unlockTimeSlot1;
            }
        }
    }

    private void CalculateSecondSlotTime()
    {
        bool isTimerStart = SlotsManager.Instance.isStartTimerSlot2;
        if (!isSecondSlotTimeOver && isTimerStart)
        {
            currentUnlockTimeSlot2 -= Time.deltaTime;
            if (currentUnlockTimeSlot2 <= 0)
            {
                //Give Reward
                Debug.Log("Slot Unlocked");
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 1, 0);
                SlotsManager.Instance.CheckForStartTimeSlot2();
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 1, 0);
                SlotsManager.Instance.CheckForSecondSlotIsEmpty();
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 1, 1);
                SlotsManager.Instance.CheckForRewardGiveSlot2();
                currentUnlockTimeSlot2 = SlotsManager.Instance.unlockTimeSlot2;
            }
        }
    }

    private void CalculateThiredSlotTime()
    {
        bool isStartTime = SlotsManager.Instance.isStartTimerSlot3;
        if (!isThiredSlotTimeOver && isStartTime)
        {
            currentUnlockTimeSlot3 -= Time.deltaTime;
            if (currentUnlockTimeSlot3 <= 0)
            {
                Debug.Log("Slot Unlocked");
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 2, 0);
                SlotsManager.Instance.CheckForStartTimeSlot3();
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 2, 0);
                SlotsManager.Instance.CheckForThiredSlotIsEmpty();
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 2, 1);
                SlotsManager.Instance.CheckForRewardGiveSlot3();
                currentUnlockTimeSlot3 = SlotsManager.Instance.unlockTimeSlot3;
            }
        }
    }

    private void CalculateForthSlotTime()
    {
        bool isStartTime = SlotsManager.Instance.isStartTimerSlot4;
        if (!isForthSlotTimeOver && isStartTime)
        {
            currentUnlockTimeSlot4 -= Time.deltaTime;
            if (currentUnlockTimeSlot4 <= 0)
            {
                //Give Reward
                Debug.Log("Slot Unlocked");
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 3, 0);
                SlotsManager.Instance.CheckForStartTimeSlot3();
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 3, 0);
                SlotsManager.Instance.CheckForForthSlotIsEmpty();
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 3, 1);
                SlotsManager.Instance.CheckForRewardGiveSlot4();
                currentUnlockTimeSlot4 = SlotsManager.Instance.unlockTimeSlot4;
            }
        }
    }
}
