using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_WaitTimer;
    [SerializeField] private TextMeshProUGUI txt_Target;
    [SerializeField] private float flt_WaitTime = 3;
    private float currentTime = 0;

    private void OnEnable()
    {
        if(GameManager.instance.GetBall != null)
            GameManager.instance.GetBall.gameObject.SetActive(false);

        currentTime = flt_WaitTime;
        if (GameManager.instance.isPlayerBatting)
        {
            txt_Target.text = "Target : " +  (GameManager.instance.playerTotalRunsInOneRound + 1).ToString();
        }
        else
        {
            txt_Target.text = "Target : " + (GameManager.instance.aiTotalRuns + 1).ToString();
        }
    }

    private void Update()
    {
        StartWaitTime();
    }

    private void StartWaitTime()
    {
        currentTime -= Time.deltaTime;
        txt_WaitTimer.text = currentTime.ToString("F0");
        if (currentTime <= 0)
        {
            //GameManager.instance.isGamePlaying = true;
            this.gameObject.SetActive(false);
            GameManager.instance.StartGame();
            //UIManager.instance.ui_PlayScreen.gameObject.SetActive(true); 
            //GameManager.instance.SpawnNewBall();
            //GameManager.instance.player.gameObject.SetActive(true);
            //GameManager.instance.aiPaddle.gameObject.SetActive(true);

        }
    }

  
}
