using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGamePlayUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI txt_BallCount;
    [SerializeField] private TextMeshProUGUI txt_HitCount;


    private void OnEnable()
    {
        SetMiniGamePlayData();
    }

    public void SetMiniGamePlayData()
    {
        txt_BallCount.text = MiniGameManager.Instance.currentBallCount.ToString();
        txt_HitCount.text = MiniGameManager.Instance.targetHitCount.ToString();
    }
}
