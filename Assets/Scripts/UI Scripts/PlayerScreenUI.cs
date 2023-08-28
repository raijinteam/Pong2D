using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScreenUI : MonoBehaviour
{

    public GameObject allBoundries;
    public GameObject activeStates;
    public TextMeshProUGUI txt_GameTime;
    public TextMeshProUGUI txt_Score;
    public TextMeshProUGUI txt_Wickets;
    public Button btn_Rotation;
    public Button btn_Powerup;

    public bool showPowerupTutorial;

    private void OnEnable()
    {
        btn_Rotation.gameObject.SetActive(false);
        allBoundries.gameObject.SetActive(false);
        activeStates.SetActive(false);
        btn_Powerup.gameObject.SetActive(false);

        SetRotationButtonActiveWhenPlayerBatting();

        if (GameManager.instance.isGamePlaying)
        {
            allBoundries.gameObject.SetActive(true);
            activeStates.SetActive(true);

            txt_GameTime.text = GameManager.instance.currentActiveGameTime.ToString("00:00");
            txt_Score.text = GameManager.instance.roundRunsCount.ToString();
            txt_Wickets.text = GameManager.instance.roundWicketCount.ToString();
        }


        //is Player level is 2 then powerup button is enable



        /*if (DataManager.Instance.playerLevel >= 2)
        {
            btn_Powerup.gameObject.SetActive(true);
        }

        //is first time powerup button enable then show powerup button tutorial
        if (DataManager.Instance.playerLevel == 2 && showPowerupTutorial)
        {
            Time.timeScale = 0;
            Debug.Log("Show powerup BUtton");
            StartPowerupTutorial();
        }*/
    }


    public void SetTragetCount(int currentTargetCount , int finishTargetCount)
    {
        txt_Score.text = currentTargetCount.ToString();
        txt_Wickets.text = finishTargetCount.ToString();
    }


    private void SetRotationButtonActiveWhenPlayerBatting()
    {
        //Check if player batting then also rotation button need to be true
        if (GameManager.instance.isPlayerBatting)
        {
            btn_Rotation.gameObject.SetActive(true);
        }


        //Check if is batting tutorial and is player rotation is enable so show rotation button
        var isPlayerBattingTutorial = GameManager.instance.isBatTutorialGameStart == true;
        var isTutorialStatePlayerRotation = UIManager.instance.ui_Tutorial.toutorialState == TutorialState.BatPaddleSwing;
        if (isPlayerBattingTutorial && isTutorialStatePlayerRotation)
        {
            btn_Rotation.gameObject.SetActive(true);
        }
    }


    private void StartPowerupTutorial()
    {
        if (DataManager.Instance.playerLevel == 2)
        {
            //Show powerup tutorial
            UIManager.instance.ui_Tutorial.toutorialState = TutorialState.Powerup;
            UIManager.instance.ui_Tutorial.gameObject.SetActive(true);
        }
    }




    public void OnPointerDown_PlayerRotation()
    {
        GameManager.instance.player.playerMovement.isPlayerRotating = true;
    }

    public void OnPointerUp_PlayerRotation()
    {
        GameManager.instance.player.playerMovement.isPlayerRotating = false;
    }


    public void OnClick_PowerUp()
    {
        if (DataManager.Instance.isGameFirstTimeLoad)
        {
            //Time.timeScale = 1;
            UIManager.instance.ui_Tutorial.toutorialState = TutorialState.BatFinalTestMSG;
            //UIManager.instance.ui_Tutorial.gameObject.SetActive(false);
        }

        //Apply powerup logic
        Debug.Log("Poweruo is used");
    }
}
