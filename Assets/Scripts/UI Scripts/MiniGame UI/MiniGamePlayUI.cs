using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGamePlayUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI txt_MiniGameTimer;
    [SerializeField] private TextMeshProUGUI txt_HitCount;


    private void OnEnable()
    {
        SetMiniGamePlayData();
    }

    private void Update()
    {
        txt_MiniGameTimer.text = MiniGameManager.Instance.currentMiniGameTimer.ToString("F0");
    }

    public void SetMiniGamePlayData()
    {
        txt_MiniGameTimer.text = MiniGameManager.Instance.currentMiniGameTimer.ToString();
        txt_HitCount.text = MiniGameManager.Instance.targetHitCount.ToString();
    }
}
