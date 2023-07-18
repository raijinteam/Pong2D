using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    public float[] currentSlotTime;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Check For time WHen Game is NOt Running

        for(int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            //Debug.Log("Player prefs Data : " + i + " " + PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i));
            currentSlotTime[i] = PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i);
            if(SlotsManager.Instance.allSlots[i].slotState == SlotState.TimerStart)
            {
                CalculateSlotTimeWhenGameNotRunning(i);
            }
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
        string gameQuitTimeString = PlayerPrefs.GetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME);

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
                currentSlotTime[index] = SlotsManager.Instance.allSlots[index].timer;
            }
        }
    }
}
