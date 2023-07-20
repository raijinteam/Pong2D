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
            all_Levels[i].txt_WinningTorphies.text = "Win +" + GameManager.instance.all_LevelWinningTrophies[i].ToString();
            all_Levels[i].txt_RequireTrophiesForIncreaseLevel.text = GameManager.instance.all_RequireTrophiesForLevelUp[i].ToString();
            all_Levels[i].txt_LossingTrophies.text = "Loose -" + GameManager.instance.all_LevelLossingTrophies[i].ToString();
            all_Levels[i].txt_WinningPrice.text = GameManager.instance.all_LevelWinningPrice[i].ToString();
            all_Levels[i].txt_EntryFees.text = GameManager.instance.all_LevelsEntryFees[i].ToString();
        }
    }



    public void OnClick_StartLevel(int _index)
    {
        this.gameObject.SetActive(false);
        GameManager.instance.currentLevelIndex = _index;
        DataManager.Instance.DecreaseCoins(GameManager.instance.all_LevelsEntryFees[_index]);
        UIManager.instance.ui_MatchMaker.gameObject.SetActive(true);
    }
}
