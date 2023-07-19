using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DefaultExecutionOrder(-2)]
public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    public float[] currentSlotTime;


    private void Awake()
    {
        Instance = this;
    }


    public void SetSlotTimeData(int _slotIndex , float _rewardSlotTime)
    {
        currentSlotTime[_slotIndex] = _rewardSlotTime;
        if (SlotsManager.Instance.allSlots[_slotIndex].slotState == SlotState.TimerStart)
        {
            CalculateSlotTimeWhenGameNotRunning(_slotIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            CalculateTimeWhenGameIsRunning(i);
        }
    }

    private void OnDestroy()
    {
        // Send To DATAMANAGER

        //Set Player prefs Data
        for (int i =0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i, currentSlotTime[i]);
            //Debug.Log("Player prefs Slot TIme : " + i +" " +  PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i));
        }

        DateTime gameQuitTime = DateTime.Now;
        PlayerPrefs.SetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME, gameQuitTime.ToString()); 
    }


    private void CalculateSlotTimeWhenGameNotRunning(int index)
    {
        //string gameQuitTimeString = PlayerPrefs.GetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME);

        string gameQuitTimeString = DataManager.Instance.GetGameQuitTime();

        if (!gameQuitTimeString.Equals(""))
        {
            DateTime quitTime = DateTime.Parse(gameQuitTimeString);
            DateTime currentTime = DateTime.Now;

            if (currentTime > quitTime)
            {
                TimeSpan timeSpan = currentTime - quitTime;

                Debug.Log("Total Quit Seconds : " + (float)timeSpan.TotalSeconds);

                if (currentSlotTime[index] > (float)timeSpan.TotalSeconds)
                {
                    float totalTime = currentSlotTime[index] - (float)timeSpan.TotalSeconds;
                    Debug.Log("Current Time : " + currentSlotTime[index]);
                    Debug.Log("Total Time : " + totalTime);
                    currentSlotTime[index] = totalTime;
                }
                else
                {
                    Debug.Log("Reward Unlocked");
                   
                    DataManager.Instance.SetRewardSlotState(index, SlotState.RewardGenrated);
                    UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
                    currentSlotTime[index] = SlotsManager.Instance.allSlots[index].timer;
                }
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
               
                DataManager.Instance.SetRewardSlotState(index, SlotState.RewardGenrated);
                UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
                currentSlotTime[index] = SlotsManager.Instance.allSlots[index].timer;
            }
        }
    }
}
