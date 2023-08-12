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

    public int maxPlayerLevel;

    public void SetAllPlayerLevel()
    {
        for (int i = 0; i < all_Players.Length; i++)
        {
            //Debug.Log("All Heros Level : " + DataManager.Instance.GetHeroLevel(i));
            all_Players[i].currentLevel = DataManager.Instance.GetHeroLevel(i);
            all_Players[i].currentCards = DataManager.Instance.GetHeroCards(i);
            SetPlayerState(i);
        }
    }


    public bool IsPlayerReachMaxLevel(int index)
    {
        if(all_Players[index].currentLevel >= maxPlayerLevel)
        {
            return true;
        }
        return false;
    }

    public bool IsEnoughCardsForUpgradePlayer(int index)
    {
        if (all_Players[index].currentCards >= all_Players[index].requireCardsToUnlock[all_Players[index + 1].currentLevel])
            return true;

        return false;
    }


    public void IncreasePlayerLevel(int index)
    {
        all_Players[index].currentLevel++;
        DataManager.Instance.SetHeroLevel(index , all_Players[index].currentLevel);
    }



    public void IncreasePlayerCards(int index , int amount)
    {
        all_Players[index].currentCards += amount;
        DataManager.Instance.SetHeroCard(index, all_Players[index].currentCards);
    }


    public void SetPlayerState(int index)
    {
        Debug.Log("Set Cards state");
        if (all_Players[index].currentCards > 0 && all_Players[index].currentCards < all_Players[index].requireCardsToUnlock[all_Players[index].currentLevel] || all_Players[index].currentLevel > 0)
        {
            Debug.Log("Cards increase");
            all_Players[index].playerState = PlayerState.HasCards;
            Debug.Log("Player " + index + " player State " + all_Players[index].playerState);
        }
        else if (all_Players[index].currentCards >= all_Players[index].requireCardsToUnlock[all_Players[index].currentLevel])
        {
            Debug.Log("Upgrade Player");
            all_Players[index].playerState = PlayerState.EnoughCardsForUpgrade;
        }
    }
}
