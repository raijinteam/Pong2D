using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTarget : MonoBehaviour
{

    private string ballTag = "MiniGameBall";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ballTag))
        {
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            MiniGameManager.Instance.targetHitCount++;
            UIManager.instance.ui_MiniGame.ui_MiniGamePlay.SetMiniGamePlayData();
            MiniGameManager.Instance.SpawnNewTarget();
            //MiniGameManager.Instance.SpawnNewBall();
        }
    }
}
