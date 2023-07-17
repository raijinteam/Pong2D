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
    void Start()
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

        for(int i =0; i < 4; i++)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + i, 0);
        }
    }

    private void GetPlayerData()
    {
       // PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY, 1);
    }



    private void ClearPlayerPrefs()
    {
        Debug.Log("Player Prefs Clear");
        PlayerPrefs.DeleteAll();
    }
}
