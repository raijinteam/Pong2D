using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI txt_Winner;
    [SerializeField] private TextMeshProUGUI txt_Player1;
    [SerializeField] private TextMeshProUGUI txt_Player2;


    private void OnEnable()
    {
        Time.timeScale = 0;
        txt_Winner.text = GameManager.instance.winnerName + " Wins";
        string player1Runs = GameManager.instance.playerTotalRunsInOneRound.ToString();
        string player1Wickets = GameManager.instance.playerTotalWicketsInOneRound.ToString();
        string player2Runs = GameManager.instance.aiTotalRuns.ToString();
        string player2Wickets = GameManager.instance.aiTotalWickets.ToString();

        txt_Player1.text = player1Runs + " / " + player1Wickets;
        txt_Player2.text = player2Runs + " / " + player2Wickets;
    }

    public void OnClick_Restart()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        UIManager.instance.ui_PlayScreen.gameObject.SetActive(true);
        GameManager.instance.ResetScoreWHenGameover();
    }


    public void OnClick_Home()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        UIManager.instance.allMenus.SetActive(true);
    }
}
