using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }



    public PlayerData[] all_Players;



    public bool IsEnoughCardsForUpgradePlayer(int index)
    {
        if (all_Players[index].currentCards >= all_Players[index].requireCardsToUnlock[all_Players[index].currentLevel])
            return true;

        return false;
    }

    public void CheckIfPlayerHasEnoughCardsForUpgrade(int index)
    {
        if (all_Players[index].currentCards >= all_Players[index].requireCardsToUnlock[all_Players[index].currentLevel])
        {
            Debug.Log("Upgrade Player");
            all_Players[index].playerState = PlayerState.EnoughCardsForUpgrade;
        }
    }
}
