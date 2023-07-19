using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;


    public int gameCoins;

   

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
        if (Input.GetKeyDown(KeyCode.Space))
            ClearPlayerPrefs();
    }


   


    private void SetPlayerData()
    {
        Debug.Log("Set Player Data");

        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_GAMECOINS, gameCoins);

        //Set Open Day For Daily Reward
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAYS_COUNT, 0);


        //Set Slot State Empty
        for(int i =0; i < 4; i++)
        {
             SetRewardSlotState(i, SlotState.Empty);
            //SlotsManager.Instance.allSlots[i].slotState = SlotState.Empty;
            PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i, 0);
            float playerPrefsTime = PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i);
            TimeManager.Instance.SetSlotTimeData(i, playerPrefsTime);
            Debug.Log("Reward Slot TIme : " + PlayerPrefs.GetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i));
        }

        //Set Achievement Current Value set 0
        for (int i = 0; i < AchievementManager.Instance.all_Archivements.Length; i++)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + i, 0);
        }
    }

    private void GetPlayerData()
    {
        for(int i = 0; i < SlotsManager.Instance.allSlots.Length; i++)
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

    #region PLAYERPREFS SET DATA

   

    #endregion

    #region PLAYERPREFS GET DATA

    public string GetGameQuitTime()
    {
        return PlayerPrefs.GetString(PlayerPrefsKeys.KEY_GAME_QUIT_TIME);
    }

    #endregion
}
