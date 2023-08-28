using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum TutorialState
{
    Start,
    BatStartMSG,
    BatPaddleMove,
    BatPaddleSwing,
    BatPowerUp,
    BatFinalTestMSG,
    BatFinalTest,
    BallLeftSwingPoint,
    BallRightSwingMSG,
    BallRightSwingPoint,
    BasicComplete,
    TutorialGame,
    ChestReward,
    UnlockChest,
    CollecteChestRewards,
    ClickPlayerMenu,
    PlayerDetails,
    UpgradePlayer,
    MiniGame,
    Powerup
}


public class TutorialUI : MonoBehaviour
{
    [SerializeField] private RectTransform panel_Start;
    [SerializeField] private RectTransform panel_MovePaddleMSG;
    [SerializeField] private RectTransform panel_RotatePaddleMSG;
    [SerializeField] private RectTransform panel_BatPowerupMSG;
    [SerializeField] private RectTransform panel_BatPowerup;
    [SerializeField] private RectTransform panel_BatCompleteMSG;
    
    [SerializeField] private RectTransform panel_BallLeftSwingPoint;
    [SerializeField] private RectTransform panel_BallRightSwingPoint;
    [SerializeField] private RectTransform panel_BasciTutComplete;
    [SerializeField] private RectTransform panel_RewardChest;
    [SerializeField] private RectTransform panel_OpenRewardChest;
    [SerializeField] private RectTransform panel_CollectChestReward;
    [SerializeField] private RectTransform panel_SelectPlayerUpgrade;
    [SerializeField] private RectTransform panel_PlayerDetails;
    [SerializeField] private RectTransform panel_UpgradePlayer;
    [SerializeField] private RectTransform panel_MiniGameActivated;
    [SerializeField] private RectTransform panel_Powerup;


    [Header("bat Tutoarial")]
    [SerializeField] private bool canTouch;
    [SerializeField] private float userPaddleMoveTime;
    [SerializeField] private float userPaddleSwingTime;

    public TutorialState toutorialState;

    private void OnEnable()
    {
        canTouch = true;
    }


    private void Update()
    {
        if (toutorialState == TutorialState.Start)
        {
            if (Input.GetMouseButtonDown(0))
            {
                panel_Start.gameObject.SetActive(false);
                panel_MovePaddleMSG.gameObject.SetActive(true);
                toutorialState = TutorialState.BatStartMSG;
                GameManager.instance.StartTutorialGame();
            }
        }
        else if (toutorialState == TutorialState.BatStartMSG)
        {
            if (Input.GetMouseButtonDown(0))
            {
                panel_MovePaddleMSG.gameObject.SetActive(false);
                toutorialState = TutorialState.BatPaddleMove;
            }
        }

        else if (toutorialState == TutorialState.BatPaddleMove)
        {

            if (GameManager.instance.player.playerMovement.isPlayerMoving)
            {
                userPaddleMoveTime -= Time.deltaTime;
                canTouch = false;
            }

            if (userPaddleMoveTime <= 0)
            {
                toutorialState = TutorialState.BatPaddleSwing;
                panel_RotatePaddleMSG.gameObject.SetActive(true);
                canTouch = true;
            }


            if (Input.GetMouseButtonDown(0) && canTouch)
            {
                panel_MovePaddleMSG.gameObject.SetActive(false);
                GameManager.instance.isTutorialRunning = true;
            }
        }
        else if (toutorialState == TutorialState.BatPaddleSwing)
        {
            UIManager.instance.ui_PlayScreen.gameObject.SetActive(true);
            UIManager.instance.ui_PlayScreen.btn_Rotation.gameObject.SetActive(true);
            if (GameManager.instance.player.playerMovement.isPlayerRotating)
            {
                canTouch = false;
                userPaddleSwingTime -= Time.deltaTime;
            }

            if (userPaddleSwingTime <= 0)
            {
                canTouch = true;
                GameManager.instance.isBatTutorialGameStart = true;
                toutorialState = TutorialState.BatPowerUp;
            }

            if (Input.GetMouseButtonDown(0) && canTouch)
            {
                panel_RotatePaddleMSG.gameObject.SetActive(false);
            }
        }
        else if (toutorialState == TutorialState.BatPowerUp)
        {
            panel_BatPowerupMSG.gameObject.SetActive(true);
            UIManager.instance.ui_PlayScreen.btn_Powerup.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                panel_BatPowerupMSG.gameObject.SetActive(false);
            }
        }else if(toutorialState == TutorialState.BatFinalTestMSG)
        {
            panel_BatCompleteMSG.gameObject.SetActive(true);
            panel_BatPowerupMSG.gameObject.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
            panel_BatCompleteMSG.gameObject.SetActive(false);
                toutorialState = TutorialState.BatFinalTest;
                UIManager.instance.ui_TimeScreen.gameObject.SetActive(true);
                canTouch = false;
            }
        }
        else if (toutorialState == TutorialState.BatFinalTest)
        {
            if (GameManager.instance.testTargetHitCount >= 5)
            {
                canTouch = true;
                GameManager.instance.isBatTutorialGameStart = false;
                panel_BallLeftSwingPoint.gameObject.SetActive(true);
                toutorialState = TutorialState.BallLeftSwingPoint;
                panel_BallLeftSwingPoint.gameObject.SetActive(true);
            }
        }
        else if (toutorialState == TutorialState.BallLeftSwingPoint)
        {
            if (Input.GetMouseButtonDown(0) && canTouch)
            {
                canTouch = false;
                panel_BallLeftSwingPoint.gameObject.SetActive(false);
                GameManager.instance.BowlingTutorialStart();
            }
        }
        else if (toutorialState == TutorialState.BallRightSwingMSG)
        {
            panel_BallRightSwingPoint.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                panel_BallRightSwingPoint.gameObject.SetActive(false);
                toutorialState = TutorialState.BallRightSwingPoint;
                Time.timeScale = 1;
            }
        }
        else if (toutorialState == TutorialState.BallRightSwingPoint)
        {

        }
        else if (toutorialState == TutorialState.BasicComplete)
        {
            panel_BasciTutComplete.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                panel_BasciTutComplete.gameObject.SetActive(false);
                toutorialState = TutorialState.TutorialGame;
                //Play one match
                UIManager.instance.ui_MatchMaker.gameObject.SetActive(true);
                toutorialState = TutorialState.ChestReward;
                this.gameObject.SetActive(false);
            }
        }
        else if (toutorialState == TutorialState.ChestReward)
        {
            //Open Chest 
            panel_RewardChest.gameObject.SetActive(true);
            panel_Start.gameObject.SetActive(false);
        }
        else if (toutorialState == TutorialState.UnlockChest)
        {
            panel_OpenRewardChest.gameObject.SetActive(true);
            panel_RewardChest.gameObject.SetActive(false);
        }
        else if (toutorialState == TutorialState.CollecteChestRewards)
        {
            panel_OpenRewardChest.gameObject.SetActive(false);
            panel_CollectChestReward.gameObject.SetActive(true);
        }

        else if (toutorialState == TutorialState.ClickPlayerMenu)
        {
            panel_Start.gameObject.SetActive(false);
            panel_CollectChestReward.gameObject.SetActive(false);
            panel_SelectPlayerUpgrade.gameObject.SetActive(true);
            UIManager.instance.ui_SlotTimer.gameObject.SetActive(false);
            UIManager.instance.ui_Navigation.gameObject.SetActive(true);
        }
        else if (toutorialState == TutorialState.PlayerDetails)
        {
            panel_PlayerDetails.gameObject.SetActive(true);
            panel_SelectPlayerUpgrade.gameObject.SetActive(false);
        }
        else if (toutorialState == TutorialState.UpgradePlayer)
        {
            panel_UpgradePlayer.gameObject.SetActive(true);
            panel_PlayerDetails.gameObject.SetActive(false);
        }
        else if (toutorialState == TutorialState.MiniGame)
        {
            panel_Start.gameObject.SetActive(false);
            panel_MiniGameActivated.gameObject.SetActive(true);
            UIManager.instance.ui_Navigation.OnClick_MenuActivate(4);

        }
        else if (toutorialState == TutorialState.Powerup)
        {
            panel_Start.gameObject.SetActive(false);
            panel_MiniGameActivated.gameObject.SetActive(false);
            panel_Powerup.gameObject.SetActive(true);
        }

    }
}
