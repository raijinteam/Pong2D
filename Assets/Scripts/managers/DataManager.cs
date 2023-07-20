using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;


    public int gameCoins;
    public int trophies;


    public int dailyRewardTime;

    public int numberOfAchivements;


    private void Awake()
    {
        Instance = this;    
    }



    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.KEY_GAMECOINS))
        {
            GetPlayerData();
        }
        else
        {
            SetPlayerData();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            ClearPlayerPrefs();
    }


    private void SetPlayerData()
    {
        Debug.Log("Set Player Data");

        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GAMECOINS, gameCoins);
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_TROPHIES, trophies);
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_PLAYER_TOTAL_RUNS, 0);

        //Set Open Day For Daily Reward
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAYS_COUNT, 0);
        PlayerPrefs.SetInt(PlayerPrefsKeys.KET_DAYTIME, 50); // Day Time 24 Hours for Daily rewards
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAILYREWARD_ACTIVE_TIME, 0);
        TimeManager.Instance.timeForDailyRewards = GetDayTime();
        TimeManager.Instance.currentTime = GetDayTime();

        //Set Slot State Empty
        for(int i =0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
             SetRewardSlotState(i, SlotState.Empty);
            PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i, 0);
        }

        //Set Achievement Current Value set 0
        for (int i = 0; i < numberOfAchivements; i++)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + i, 0);
        }
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_REWARD_COLLECTED, 0);


        //Only For testing
        UIManager.instance.ui_UseableResouce.gameObject.SetActive(true);
    }

    private void GetPlayerData()
    {
        //Get Player Data
        gameCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_GAMECOINS);
        trophies = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_TROPHIES);

        //Only For testing
        UIManager.instance.ui_UseableResouce.gameObject.SetActive(true);


        TimeManager.Instance.SetTimeDataForDailyReward();
        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            string state = PlayerPrefs.GetString(PlayerPrefsKeys.KEY_REWARD_SLOT_STATE + i);
            SlotsManager.Instance.SetSlotState(i, state);

            float playerPrefsTime = PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i);

            TimeManager.Instance.SetSlotTimeData(i, playerPrefsTime);
        }
    }



    public float RewardSlotTimePlayerPrefs(int index)
    {
        return PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + index);
    }

    public void SetRewardSlotState(int index, SlotState state)
    {
        SlotsManager.Instance.allSlots[index].slotState = state;
        PlayerPrefs.SetString(PlayerPrefsKeys.KEY_REWARD_SLOT_STATE + index, SlotsManager.Instance.allSlots[index].slotState.ToString());
    }

    private void ClearPlayerPrefs()
    {
        Debug.Log("Player Prefs Clear");
        PlayerPrefs.DeleteAll();
    }


    #region PLAYER USEABLE RESOURCES


    public void IncreaseCoins(int _amount)
    {
        gameCoins += _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GAMECOINS, gameCoins);
        UIManager.instance.ui_UseableResouce.SetCoinsUI();
    }
    public void DecreaseCoins(int _amount)
    {
        gameCoins -= _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GAMECOINS, gameCoins);
        UIManager.instance.ui_UseableResouce.SetCoinsUI();
    }

    public void IncreaseTrophies(int _amount)
    {
        trophies += _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_TROPHIES, trophies);
        UIManager.instance.ui_UseableResouce.SetTrophiesUI();
    }

    public void DecreaseTrophies(int _amount)
    {
        trophies -= _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_TROPHIES, trophies);
        UIManager.instance.ui_UseableResouce.SetTrophiesUI();
    }

    #endregion







    #region PLAYERPREFS SET DATA


    public void SetPlayerTotalRuns(int runs)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_PLAYER_TOTAL_RUNS, runs);
    }

    public void SetArchivmentRewardState(int index , int i)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_REWARD_COLLECTED + index, i);
    }


    public void SetSlotUnlockTimer(int _index , float _time)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + _index , _time);
    }

    public void SetDayTime(int _time)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KET_DAYTIME, _time);
    }

    public void SetDailyRewardActiveTime(int i)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAILYREWARD_ACTIVE_TIME , i);
    }

    #endregion

    #region PLAYERPREFS GET DATA


    public int GetPlayerTotalRuns()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_PLAYER_TOTAL_RUNS);
    }

    public int GetArchivmentRewardState(int i)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_REWARD_COLLECTED + i);
    }


    public string GetGameQuitTime()
    {
        return PlayerPrefs.GetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME);
    }

    public int GetDayTime()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KET_DAYTIME);
    }

    public int GetDailyRewardActiveTimeForBool()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_DAILYREWARD_ACTIVE_TIME);
    }

    public float GetSlotUnlockTime(int _index)
    {
        return PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + _index);
    }
    #endregion
}
