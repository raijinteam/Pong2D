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

    private int winningCoins;

    private void OnEnable()
    {
        Time.timeScale = 0;
        txt_Winner.text = GameManager.instance.winnerName + " Wins";

        img_WinningStateContainer.gameObject.SetActive(false);
        img_BagContainer.gameObject.SetActive(false);


        if (GameManager.instance.winnerName == "Player")
        {
            img_BagContainer.gameObject.SetActive(true);
            img_Bag.sprite = SlotsManager.Instance.allSlots[0].icon;
            img_WinningStateContainer.gameObject.SetActive(true);
            winningCoins = GameManager.instance.all_LevelWinningPrice[GameManager.instance.currentLevelIndex];
            txt_WinningCoinsAmount.text = winningCoins.ToString();
            txt_CurrentPlayerTropies.text = DataManager.Instance.totalTrophies.ToString();
        }
    }


    public void OnClick_Home()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        GameManager.instance.Gameover();
        UIManager.instance.allMenus.SetActive(true);
        UIManager.instance.ui_UseableResouce.gameObject.SetActive(true);
    }
}
