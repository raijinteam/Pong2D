using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTarget : MonoBehaviour
{

    private string ballTag = "MiniGameBall";
    private string tutorialBall = "Ball";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ballTag))
        {
            MiniGameManager.Instance.targetHitCount++;
            UIManager.instance.ui_MiniGame.ui_MiniGamePlay.SetMiniGamePlayData();
            this.gameObject.SetActive(false);
            MiniGameManager.Instance.SpawnNewTarget();
            //collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag(tutorialBall))
        {
            this.gameObject.SetActive(false);
            if (GameManager.instance.isBatTutorialGameStart || GameManager.instance.isBallTutorialStart)
            {
                GameManager.instance.IncreaseTargetHitCountTutorial();
                GameManager.instance.SpawnNewTargetTutorial();
            }
        }
    }
}
