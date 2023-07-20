using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    public int timeForDailyRewards;
    public float activeGameTime;
    public float currentTime;
    public float[] currentSlotTime;

    private TimeSpan timeSpan;

    private void Awake()
    {
        Instance = this;
    }


    public void SetSlotTimeData(int _slotIndex, float _rewardSlotTime)
    {
        currentSlotTime[_slotIndex] = _rewardSlotTime;

        CalculateTimeForSLots(_slotIndex);
    }

    public void SetTimeDataForDailyReward()
    {
        CalculateTimeForDailyReward();
        currentTime = timeForDailyRewards;
    }


    // Update is called once per frame
    void Update()
    {
        activeGameTime += Time.deltaTime;

        CalculateDailyRewardTimeWhenGameIsRunning();


        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            CalculateTimeWhenGameIsRunning(i);
        }
    }

    private void OnDestroy()
    {
        // Send To DATAMANAGER

        //Set Player prefs Data
        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i, currentSlotTime[i]);
            //Debug.Log("Player prefs Slot TIme : " + i +" " +  PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i));
        }

        //time for remainng 
        int totalTime = timeForDailyRewards - (int)activeGameTime;
        DataManager.Instance.SetDayTime(totalTime);

        Debug.Log("Total Day time : " + DataManager.Instance.GetDayTime());

        DateTime gameQuitTime = DateTime.Now;
        PlayerPrefs.SetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME, gameQuitTime.ToString());
    }



    public void CalculateScreenOFTime()
    {
        string gameQuitTimeString = DataManager.Instance.GetGameQuitTime();

        if (!gameQuitTimeString.Equals(""))
        {
            DateTime quitTime = DateTime.Parse(gameQuitTimeString);
            DateTime currentTime = DateTime.Now;

            if (currentTime > quitTime)
            {
                timeSpan = currentTime - quitTime;
                Debug.Log("Total Quit Seconds : " + (float)timeSpan.TotalSeconds);
            }
        }
    }


    public void CalculateTimeForDailyReward()
    {
        CalculateScreenOFTime();

        int dayTime = DataManager.Instance.GetDayTime();

        Debug.Log("Total Day time is : " + (dayTime - (int)timeSpan.TotalSeconds));

        timeForDailyRewards = dayTime - (int)timeSpan.TotalSeconds;

        if(timeForDailyRewards <= 0)
        {
            Debug.Log("Set Reward Time");
            DataManager.Instance.SetDailyRewardActiveTime(0);
            GameManager.instance.CheckForDailyRewardTImeIsStart();
            timeForDailyRewards = DataManager.Instance.dailyRewardTime;
        }
    }


    public void CalculateTimeForSLots(int _index)
    {
        if (SlotsManager.Instance.allSlots[_index].slotState == SlotState.TimerStart)
        {
            //Calculate Time For Slots
            if (currentSlotTime[_index] > (float)timeSpan.TotalSeconds)
            {
                float totalTime = currentSlotTime[_index] - (float)timeSpan.TotalSeconds;
                Debug.Log("Current Time : " + currentSlotTime[_index]);
                Debug.Log("Total Time : " + totalTime);
                currentSlotTime[_index] = totalTime;
            }
            else
            {
                Debug.Log("Reward Unlocked");
                SlotsManager.Instance.isAnotherSlotHasActiveTime = false;
                DataManager.Instance.SetRewardSlotState(_index, SlotState.RewardGenrated);
                UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
                UIManager.instance.ui_SlotTimer.gameObject.SetActive(false);
                currentSlotTime[_index] = SlotsManager.Instance.allSlots[_index].timer;
            }
        }
    }

   

    private void CalculateDailyRewardTimeWhenGameIsRunning()
    {
        if (GameManager.instance.isDailyRewardCollected)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                DataManager.Instance.SetDailyRewardActiveTime(0);
                GameManager.instance.CheckForDailyRewardTImeIsStart();
                Debug.Log("Set Reward Time");
                currentTime = DataManager.Instance.dailyRewardTime;
            }
        }
    }


    private void CalculateTimeWhenGameIsRunning(int index)
    {
        if (SlotsManager.Instance.allSlots[index].slotState == SlotState.TimerStart)
        {
            //Debug.Log("Method is Running");
            //Calculate Time When Game is Running
            currentSlotTime[index] -= Time.deltaTime;
            if (currentSlotTime[index] <= 0)
            {
                //Give Reward
                Debug.Log("Slot Unlocked");
                SlotsManager.Instance.isAnotherSlotHasActiveTime = false;
                DataManager.Instance.SetRewardSlotState(index, SlotState.RewardGenrated);
                UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
                UIManager.instance.ui_SlotTimer.gameObject.SetActive(false);
                currentSlotTime[index] = SlotsManager.Instance.allSlots[index].timer;
            }
        }
    }
}
