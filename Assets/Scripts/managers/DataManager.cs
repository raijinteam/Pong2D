using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;


    public string playerName;
    public int profileIconIndex;
    public int totalCoins;
    public int totalTrophies;
    public int totalGems;
    public int totalSkipIt;
    public int playerLevel;
    public int playerXP;
    public float requireXPForLevelup;

    public float dailyMissionTime;

    public float xpIncreasePR;

    public int activePlayerIndex;

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

        //this is temp code
        if (Input.GetKeyDown(KeyCode.P))
            IncreaseXPCount(10);


        if (Input.GetKeyDown(KeyCode.Tab))
            ClearPlayerPrefs();

        //Onlyu FOr testing
        if (Input.GetKeyDown(KeyCode.L))
            IncreaseCoins(1000);

        //Only For Testing
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_PLAYER_TOTAL_RUNS, 50);
            GameManager.instance.playerTotalRuns = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_PLAYER_TOTAL_RUNS);
        }
    }


    private void SetPlayerData()
    {
        Debug.Log("Set Player Data");

        SetPlayerLevel(1); // Set Player level;
        playerLevel = GetPlayerLevel();

        SetPlayerCoins(totalCoins); //Set PLyaer coins
        totalCoins = GetPlayerCoins();

        //  PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GAMECOINS, totalCoins);
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_TROPHIES, totalTrophies);
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GEMS, totalGems);
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_PLAYER_TOTAL_RUNS, 0);
        SetPlayerXP(0);
        playerXP = GetPlayerXP();

        SetRequireXP(requireXPForLevelup);
        requireXPForLevelup = GetRequireXP();

        SetActivePlayerIndex(0); // Set Active Player Index
        activePlayerIndex = GetActivePlayerIndex();

        SetDailyMissionTime(dailyMissionTime); //Set Daily Mission time

        //Set Open Day For Daily Reward
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAYS_COUNT, 0);
        PlayerPrefs.SetInt(PlayerPrefsKeys.KET_DAYTIME, 24 * 60 * 60); // Day Time 24 Hours for Daily rewards
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAILYREWARD_ACTIVE_TIME, 0);
        TimeManager.Instance.timeForDailyRewards = GetDayTime();
        TimeManager.Instance.currentDailyRewardTime = GetDayTime();

        //Set Slot State Empty
        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            SetRewardSlotState(i, SlotState.Empty);
            PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i, 0);
        }


        UIManager.instance.ui_Achievement.SpawnDailyMissions(); // Spawn all Daily missions

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
        // totalCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_GAMECOINS);
        totalTrophies = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_TROPHIES);
        totalGems = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_GEMS);

        totalCoins = GetPlayerCoins();

        playerLevel = GetPlayerLevel();

        requireXPForLevelup = GetRequireXP();

        playerXP = GetPlayerXP();

        activePlayerIndex = GetActivePlayerIndex();

        dailyMissionTime = GetDailyMissionTime();

        //Only For testing
        UIManager.instance.ui_UseableResouce.gameObject.SetActive(true);

        UIManager.instance.ui_Achievement.SpawnDailyMissions(); // Spawn all Daily missions


        TimeManager.Instance.SetTimeDataForDailyReward(); // Set Daily reward time
        TimeManager.Instance.SetDailyMissionTimeData(); // Set Daily Mission time
        for (int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            string state = PlayerPrefs.GetString(PlayerPrefsKeys.KEY_REWARD_SLOT_STATE + i);
            SlotsManager.Instance.SetSlotState(i, state);

            float playerPrefsTime = PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i);

            TimeManager.Instance.SetSlotTimeData(i, playerPrefsTime);
        }
    }



    public void IncreaseXPCount(int value)
    {
        playerXP += value;
        SetPlayerXP(playerXP);
        CheckForPlayerLevelUP();
    }

    public void CheckForPlayerLevelUP()
    {
        if (playerXP >= requireXPForLevelup)
        {
            UIManager.instance.LevelUpPopUpCalled();
            playerLevel++;
            SetPlayerLevel(playerLevel);
            playerXP = 0;
            requireXPForLevelup = requireXPForLevelup + (requireXPForLevelup * (xpIncreasePR / 100));
            SetRequireXP(requireXPForLevelup);
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


    public bool IsEnoughCoinsForPurchase(int coinAmount)
    {
        if (totalCoins < coinAmount)
            return false;
        return true;
    }

    public bool IsEnoughGemssForPurchase(int gemsAmount)
    {
        if (totalGems < gemsAmount)
            return false;
        return true;
    }


    public bool IsEnoughSkipItForUse()
    {
        if (totalSkipIt > 0)
        {
            return true;
        }
        return false;
    }


    #region PLAYER USEABLE RESOURCES


    public void IncreaseCoins(int _amount)
    {
        totalCoins += _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GAMECOINS, totalCoins);
        StartCoroutine(UIManager.instance.ui_UseableResouce.CoinAnimation());
    }


    public void DecreaseCoins(int _amount)
    {
        totalCoins -= _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GAMECOINS, totalCoins);
        StartCoroutine(UIManager.instance.ui_UseableResouce.CoinAnimation());
    }

    public void IncreaseGems(int _amount)
    {
        totalGems += _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GEMS, totalGems);
        StartCoroutine(UIManager.instance.ui_UseableResouce.GemsAnimation());
    }

    public void DecreaseGems(int _amount)
    {
        totalGems -= _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GEMS, totalGems);
        StartCoroutine(UIManager.instance.ui_UseableResouce.GemsAnimation());
    }

    public void IncreaseTrophies(int _amount)
    {
        totalTrophies += _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_TROPHIES, totalTrophies);
        StartCoroutine(UIManager.instance.ui_UseableResouce.TrophiesAnimation());
    }

    public void DecreaseTrophies(int _amount)
    {
        totalTrophies -= _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_TROPHIES, totalTrophies);
        StartCoroutine(UIManager.instance.ui_UseableResouce.TrophiesAnimation());
    }

    public void IncreaseSkipIt(int _amount)
    {
        SetPlayerSkipIt(_amount);
        StartCoroutine(UIManager.instance.ui_UseableResouce.SkipItAnimation());
        UIManager.instance.ui_HomePanel.ui_HomeScreen.SetWheelRedDot();
    }
    public void DecreaseSkipIt(int _amount)
    {
        totalSkipIt -= _amount;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_SKIPIT, totalSkipIt);
        StartCoroutine(UIManager.instance.ui_UseableResouce.SkipItAnimation());
        UIManager.instance.ui_HomePanel.ui_HomeScreen.SetWheelRedDot();
    }




    #endregion







    #region PLAYERPREFS SET DATA


    public void SetDailyMissionRewardState(int index, bool _state)
    {
        if (_state == false)
        { PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAILY_MISSION_REWARD_CLAIMED + index, 0); }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAILY_MISSION_REWARD_CLAIMED + index, 1);
        }
    }

    public void SetDailyMissionTime(float time)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_DAILY_MISSION_TIME, time);
    }

    public void SetPlayerName(string name)
    {
        PlayerPrefs.SetString(PlayerPrefsKeys.KEY_PLAYER_NAME, name);
    }

    public void SetPlayerProfileImageIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_PLAYER_PROFILE_INDEX, index);
    }

    public void SetPlayerSkipIt(int value)
    {
        totalSkipIt += value;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_SKIPIT, totalSkipIt);
    }
    public void SetRequireXP(float value)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REQUIREXP, value);
    }

    public void SetPlayerLevel(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_PLAYER_LEVEL, value);
    }

    public void SetPlayerCoins(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GAMECOINS, value);
    }

    public void SetPlayerXP(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_PLAYER_XP, value);
    }

    public void SetActivePlayerIndex(int index)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACTIVE_PLAYER_INDEX, index);
        activePlayerIndex = GetActivePlayerIndex();
    }

    public void SetPlayerTotalRuns(int runs)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_PLAYER_TOTAL_RUNS, runs);
    }

    public void SetArchivmentRewardState(int index, int i)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_REWARD_COLLECTED + index, i);
    }


    public void SetSlotUnlockTimer(int _index, float _time)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + _index, _time);
    }

    public void SetDayTime(int _time)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KET_DAYTIME, _time);
    }

    public void SetDailyRewardActiveTime(int i)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAILYREWARD_ACTIVE_TIME, i);
    }



    public void SetAchivementCurrentValue(int index, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index, value);
    }

    public void SetDayCount(int value)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAYS_COUNT, value);
    }

    public void SetGameQuitTime(string time)
    {
        PlayerPrefs.SetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME, time);
    }

    public void SetRewardSlotTime(int index, float time)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + index, time);
    }

    #endregion

    #region PLAYERPREFS GET DATA

    public bool GetDailyMissionClaimedState(int index)
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_DAILY_MISSION_REWARD_CLAIMED + index) == 0)
            return false;
        else
            return true;
    }

    public float GetDailyMissionTime()
    {
        return PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_DAILY_MISSION_TIME);
    }

    public string GetPlayerName()
    {
        return PlayerPrefs.GetString(PlayerPrefsKeys.KEY_PLAYER_NAME);
    }

    public int GetPlayerProfileIconIndex()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_PLAYER_PROFILE_INDEX);
    }

    public int GetTotalSkipIt()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_SKIPIT);
    }

    public float GetRequireXP()
    {
        return PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REQUIREXP);
    }

    public int GetPlayerLevel()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_PLAYER_LEVEL);
    }

    public int GetPlayerCoins()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_GAMECOINS);
    }

    public int GetPlayerXP()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_PLAYER_XP);
    }


    public int GetActivePlayerIndex()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACTIVE_PLAYER_INDEX);
    }


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

    public int GetAchivementCurrentValue(int index)
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + index);
    }

    public int GetDayCount()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_DAYS_COUNT);
    }

    public string GetQuitTime()
    {
        return PlayerPrefs.GetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME);
    }

    public float GetRewardSlotTime(int index)
    {
        return PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + index);
    }

    #endregion
}
