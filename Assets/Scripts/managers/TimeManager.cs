using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    public float activeGameTime;
    [Header("Daily Reward")]
    public int timeForDailyRewards;
    public float currentDailyRewardTime;
    [Header("Slot timer")]
    public float[] currentSlotTime;
    [Header("Daily Mission")]
    public float dailyMissionTime;
    public float currentDailyMissionTime;
    [Header("Daily Challange")]
    public float dailyChallangeTime;
    public float currentDailyChallangeTime;


    private TimeSpan timeSpan;

    private void Awake()
    {
        Instance = this;
    }


    public void SetDailyMissionTimeData()
    {
        //Debug.Log("Daily misison first called");
        CalculateTimeForDailyMission();
        currentDailyMissionTime = dailyMissionTime;
    }

    public void SetSlotTimeData(int _slotIndex, float _rewardSlotTime)
    {
        currentSlotTime[_slotIndex] = _rewardSlotTime;

        CalculateTimeForSLots(_slotIndex);
    }

    public void SetTimeDataForDailyReward()
    {
        CalculateTimeForDailyReward();
       // Debug.Log("Get Time : " + DataManager.Instance.GetDayTime());
        currentDailyRewardTime = DataManager.Instance.GetDayTime();
    }

    public void SetTimeDataForDailyChallange()
    {
        CalculateTimeForDailyChallange();
        currentDailyChallangeTime = DataManager.Instance.GetDailyChallangeTime();
    }

    // Update is called once per frame
    void Update()
    {
        activeGameTime += Time.deltaTime;

        CalculateDailyRewardTimeWhenGameIsRunning();

        CalculateDailyMissionTimeWhenGameIsRunning();

        CalculateDailyChallangeTimeWhenGameIsRunning();

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
            DataManager.Instance.SetRewardSlotTime(i, currentSlotTime[i]);
        }

        //time for remainng 
        int totalTime = (int)currentDailyRewardTime - (int)activeGameTime;
        DataManager.Instance.SetDayTime(totalTime);
        float totalDailyMissionTime = dailyMissionTime - activeGameTime;
        DataManager.Instance.SetDailyMissionTime(totalDailyMissionTime);

        if (DataManager.Instance.GetDailyChallangeRewardClaimedState())
        {
            float totalDailyChallangeTime = dailyChallangeTime - activeGameTime;
            DataManager.Instance.SetDailyChallangeTime(totalDailyChallangeTime);
        }

        

        DateTime gameQuitTime = DateTime.Now;
        DataManager.Instance.SetGameQuitTime(gameQuitTime.ToString());
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


    #region Daili Reward

    public void CalculateTimeForDailyReward()
    {
        CalculateScreenOFTime();

        int dayTime = DataManager.Instance.GetDayTime();

        //  Debug.Log("Total Day time is : " + (dayTime - (int)timeSpan.TotalSeconds));

        if (GameManager.instance.isDailyRewardCollected)
        {
            timeForDailyRewards = dayTime - (int)timeSpan.TotalSeconds;
        }


        if (timeForDailyRewards <= 0)
        {
            // Debug.Log("Set Reward Time");
            DataManager.Instance.SetDailyRewardActiveTime(0);
            GameManager.instance.CheckForDailyRewardTImeIsStart();
            timeForDailyRewards = DataManager.Instance.dailyRewardTime;
        }
    }

    private void CalculateDailyRewardTimeWhenGameIsRunning()
    {
        if (GameManager.instance.isDailyRewardCollected)
        {
            currentDailyRewardTime -= Time.deltaTime;
            if (currentDailyRewardTime <= 0)
            {
                DataManager.Instance.SetDailyRewardActiveTime(0);
                GameManager.instance.CheckForDailyRewardTImeIsStart();
                currentDailyRewardTime = timeForDailyRewards;
            }
        }
    }

    #endregion



    #region Daily Missions

    public void CalculateTimeForDailyMission()
    {
        CalculateScreenOFTime();
        // Debug.Log("Daily mission time : " + (int)DataManager.Instance.GetDailyMissionTime());
        int dayTime = (int)DataManager.Instance.GetDailyMissionTime();

        //  Debug.Log("Total Day time is : " + (dayTime - timeSpan.TotalSeconds));

        dailyMissionTime = dayTime - (int)timeSpan.TotalSeconds;

        if (dailyMissionTime <= 0)
        {
            // Debug.Log("Set Reward Time");
            DataManager.Instance.SetDailyMissionTime(0);
            DataManager.Instance.SetDailyMissionOnedayComplete(true);
            UIManager.instance.ui_Achievement.ResetAllListData();
            UIManager.instance.ui_Achievement.SpawnDailyMissions();
            Debug.Log("In Time manager 1");
            timeForDailyRewards = (int)DataManager.Instance.dailyMissionTime;
        }
    }


    private void CalculateDailyMissionTimeWhenGameIsRunning()
    {

        currentDailyMissionTime -= Time.deltaTime;
        if (currentDailyMissionTime <= 0)
        {
            DataManager.Instance.SetDailyMissionTime(0);
            //    Debug.Log("Set Reward Time");
            DataManager.Instance.SetDailyMissionOnedayComplete(true);
            //Debug.Log("In Time manager 2");
            UIManager.instance.ui_Achievement.ResetAllListData();
            UIManager.instance.ui_Achievement.SpawnDailyMissions();
            currentDailyMissionTime = DataManager.Instance.dailyMissionTime;
        }

    }

    #endregion


    #region Daily Challange

    public void CalculateTimeForDailyChallange()
    {
        CalculateScreenOFTime();
        // Debug.Log("Daily mission time : " + (int)DataManager.Instance.GetDailyMissionTime());
        int dayTime = (int)DataManager.Instance.GetDailyChallangeTime();

        //  Debug.Log("Total Day time is : " + (dayTime - timeSpan.TotalSeconds));

        if (DataManager.Instance.GetDailyChallangeRewardClaimedState())
        {
            dailyChallangeTime = dayTime - (int)timeSpan.TotalSeconds;

            Debug.Log("Daily Challange Time : " + dailyChallangeTime);
            if (dailyChallangeTime <= 0)
            {
                // Debug.Log("Set Reward Time");
                DataManager.Instance.SetDailyChallangeTime(0);
                DataManager.Instance.SetDailyChallangeFinishedState(false);
                dailyChallangeTime = (int)DataManager.Instance.dailyChallangeTime;
            }
        }
        


    }


    private void CalculateDailyChallangeTimeWhenGameIsRunning()
    {
        if (DataManager.Instance.GetDailyChallangeRewardClaimedState())
        {
            currentDailyChallangeTime -= Time.deltaTime;
            if (currentDailyChallangeTime <= 0)
            {
                DataManager.Instance.SetDailyChallangeTime(0);
                currentDailyMissionTime = DataManager.Instance.dailyChallangeTime;
            }
        }
        

    }

    #endregion


    #region Slot Timer

    public void CalculateTimeForSLots(int _index)
    {
        if (SlotsManager.Instance.allSlots[_index].slotState == SlotState.TimerStart)
        {
            //Calculate Time For Slots
            if (currentSlotTime[_index] > (float)timeSpan.TotalSeconds)
            {
                float totalTime = currentSlotTime[_index] - (float)timeSpan.TotalSeconds;
                //  Debug.Log("Current Time : " + currentSlotTime[_index]);
                // Debug.Log("Total Time : " + totalTime);
                currentSlotTime[_index] = totalTime;
            }
            else
            {
                //Debug.Log("Reward Unlocked");
                SlotsManager.Instance.isAnotherSlotHasActiveTime = false;
                DataManager.Instance.SetRewardSlotState(_index, SlotState.RewardGenrated);
                UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
                UIManager.instance.ui_SlotTimer.gameObject.SetActive(false);
                currentSlotTime[_index] = SlotsManager.Instance.allSlots[_index].currentTimer;
            }
        }
    }


    private void CalculateTimeWhenGameIsRunning(int index)
    {
        if (SlotsManager.Instance.allSlots[index].slotState == SlotState.TimerStart)
        {
            currentSlotTime[index] -= Time.deltaTime;
            if (currentSlotTime[index] <= 0)
            {
                SlotsManager.Instance.isAnotherSlotHasActiveTime = false;
                DataManager.Instance.SetRewardSlotState(index, SlotState.RewardGenrated);
                UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
                UIManager.instance.ui_SlotTimer.gameObject.SetActive(false);
                currentSlotTime[index] = SlotsManager.Instance.allSlots[index].currentTimer;
            }
        }
    }

    #endregion















}
