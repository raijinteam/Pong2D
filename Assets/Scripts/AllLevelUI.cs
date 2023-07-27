using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllLevelUI : MonoBehaviour
{
    [SerializeField] private LevelUI[] all_Levels;




    private void OnEnable()
    {
        //Set All Levels Data
        for(int i = 0; i < all_Levels.Length; i++)
        {
            CheckIfLevelIsUnlocked(i);

            all_Levels[i].txt_WinningTorphies.text = "Win +" + GameManager.instance.all_LevelWinningTrophies[i].ToString();
            all_Levels[i].txt_RequireTrophiesForIncreaseLevel.text = GameManager.instance.all_RequireTrophiesForLevelUp[i].ToString();
            all_Levels[i].txt_LossingTrophies.text = "Loose -" + GameManager.instance.all_LevelLossingTrophies[i].ToString();
            all_Levels[i].txt_WinningPrice.text = "Price <size=60>" +  GameManager.instance.all_LevelWinningPrice[i].ToString()+ "</size>";
            all_Levels[i].txt_EntryFees.text = "Entry Fees  " + GameManager.instance.all_LevelsEntryFees[i].ToString();
            all_Levels[i].slider_Trophies.value = DataManager.Instance.totalTrophies;
            all_Levels[i].txt_CurrentTrophies.text = DataManager.Instance.totalTrophies.ToString();
            all_Levels[i].slider_Trophies.maxValue = GameManager.instance.all_RequireTrophiesForLevelUp[i];
        }
    }


    private void CheckIfLevelIsUnlocked(int index)
    {
        all_Levels[index].isLevelUnlocked = false;
        all_Levels[index].img_TrophiesContainer.gameObject.SetActive(false);
        all_Levels[index].img_LockContainer.gameObject.SetActive(true);
        all_Levels[index].img_LockBG.gameObject.SetActive(true);
        all_Levels[index].txt_UnlockTrophiesCount.text = GameManager.instance.all_RequireTrophiesForLevelUp[index].ToString();
        if (GameManager.instance.all_RequireTrophiesForLevelUp[index] < DataManager.Instance.totalTrophies)
        {
            all_Levels[index].isLevelUnlocked = true;
            all_Levels[index].img_LockBG.gameObject.SetActive(false);
            all_Levels[index].img_TrophiesContainer.gameObject.SetActive(true);
            all_Levels[index].img_LockContainer.gameObject.SetActive(false);

        }
    }


    public void OnClick_StartLevel(int _index)
    {


        if (GameManager.instance.all_LevelsEntryFees[_index] > DataManager.Instance.totalCoins)
        {
            Debug.Log("Not Enough Coins");
            return;
        }

        this.gameObject.SetActive(false);
        GameManager.instance.currentLevelIndex = _index;
        DataManager.Instance.DecreaseCoins(GameManager.instance.all_LevelsEntryFees[_index]);
        UIManager.instance.ui_MatchMaker.gameObject.SetActive(true);
    }
}
