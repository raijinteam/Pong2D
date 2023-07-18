using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("On Enable Error")]
    [SerializeField] private SlotsManager slot;

    public int gameCoins;

   

    private void Awake()
    {
        Instance = this;


        if (PlayerPrefs.HasKey(PlayerPrefsKeys.KEY_GAMECOINS))
        {
            GetPlayerData();
        }
        else
        {
            SetPlayerData();
        }
    }



    // Start is called before the first frame update
    void OnEnable()
    {
       
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

        //Set Slot State Empty
        for(int i =0; i < SlotsManager.Instance.allSlots.Length; i++)
        {
            SetRewardSlotState(i, SlotState.Empty);
            PlayerPrefs.SetFloat(PlayerPrefsKeys.KEY_REWARD_SLOT_TIME + i, SlotsManager.Instance.allSlots[i].timer);
        }

        //Set Achievement Current Value set 0
        for(int i =0; i < AchievementManager.Instance.all_Archivements.Length; i++)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_ACHIEVEMENT_CURRENT_VALUE + i, 0);
        }
    }

    private void GetPlayerData()
    {
        

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
}
