using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI txt_Winner;
    [SerializeField] private Image img_ProfilePlayer1;
    [SerializeField] private Image img_ProfilePlayer2;

    [SerializeField] private TextMeshProUGUI txt_NamePlayer1;
    [SerializeField] private TextMeshProUGUI txt_NamePlayer2;

    [SerializeField] private Image img_WinningStateContainer;
    [SerializeField] private Image img_BagContainer;

    [SerializeField] private TextMeshProUGUI txt_WinningCoinsAmount;
    [SerializeField] private TextMeshProUGUI txt_CurrentPlayerTropies;

    [SerializeField] private Image img_Bag;

    [SerializeField] private Button btn_Home;
    [SerializeField] private Button btn_Retry;

    private float winningCoins;
    private float currentCoins = 0;

    private float winningTrophies;
    private float currentTrophies;

    private void OnEnable()
    {
        Time.timeScale = 0;
        txt_Winner.text = GameManager.instance.winnerName + " Wins";

        img_WinningStateContainer.gameObject.SetActive(false);
        img_BagContainer.gameObject.SetActive(false);
        btn_Home.gameObject.SetActive(false);
        btn_Retry.gameObject.SetActive(false);

        if (GameManager.instance.winnerName == "Player")
        {

            int currentLevelIndex = GameManager.instance.currentLevelIndex;

            img_BagContainer.gameObject.SetActive(true);
            img_Bag.sprite = SlotsManager.Instance.allSlots[0].icon;
            img_WinningStateContainer.gameObject.SetActive(true);
            
            //Set Winning Coins
            winningCoins = GameManager.instance.all_LevelWinningPrice[currentLevelIndex];
            StartCoroutine(WinningCoinsAnimations());

            //Set Winning trophies
            currentTrophies = DataManager.Instance.totalTrophies;
            winningTrophies = currentTrophies + GameManager.instance.all_LevelWinningTrophies[currentLevelIndex];
            StartCoroutine(WinningTrophiesAnimation());

            btn_Home.gameObject.SetActive(true);
        }

        //IF USER NOT WIN WHEN TUTORIAL IS PLAYING SO AGAIN USER NEED TO PLAY TUTORIAL
        else if (GameManager.instance.winnerName == "Ai" && DataManager.Instance.isGameFirstTimeLoad)
        {
            btn_Retry.gameObject.SetActive(true);
        }else
        {
            btn_Home.gameObject.SetActive(true);
        }
    }



    private IEnumerator WinningCoinsAnimations()
    {
        float currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / 0.5f;
            Debug.Log("Time : " + currentTime);
            currentCoins = Mathf.Lerp(currentCoins, winningCoins, currentTime);
            txt_WinningCoinsAmount.text = currentCoins.ToString("F0");
            yield return null;
        }
    }

    private IEnumerator WinningTrophiesAnimation()
    {
        float currentTime = 0;
        while(currentTime < 1)
        {
            currentTime += Time.deltaTime / 0.5f;
           // Debug.Log("Time : " + currentTime);
            currentTrophies = Mathf.Lerp(currentTrophies, winningTrophies, currentTime);
            txt_CurrentPlayerTropies.text = currentTrophies.ToString("F0");
            yield return null;
        }
    }


    public void OnClick_Home()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        GameManager.instance.Gameover();
        UIManager.instance.allMenus.SetActive(true);
        UIManager.instance.ui_Navigation.gameObject.SetActive(true);
        UIManager.instance.ui_Navigation.OnClick_MenuActivate(2);
        UIManager.instance.ui_UseableResouce.gameObject.SetActive(true);


        if (DataManager.Instance.isGameFirstTimeLoad)
        {
            //if player win then go to homw page and need to open chest
            UIManager.instance.ui_Tutorial.toutorialState = TutorialState.ChestReward;
            UIManager.instance.ui_Tutorial.gameObject.SetActive(true);
        }
        
    }


    //RETRY BUTTON 
    //THIS RETRY BUTTON USE WHEN TUTORIAL ACTIVE AND PLAYER NOT WIN SO AGAIN DEMO MATCH PLAY
    public void OnClick_Retry()
    {
       // Debug.Log("Retry Button Called");
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        GameManager.instance.ResetDataForGameOver();
        GameManager.instance.StartGame();
    }
}
