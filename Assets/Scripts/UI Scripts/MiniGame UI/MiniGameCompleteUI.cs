using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGameCompleteUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Reward;
    [SerializeField] private TextMeshProUGUI txt_HitCount;

    private int rewardAmount;
    private int hitCount;

    private void OnEnable()
    {
        hitCount = MiniGameManager.Instance.targetHitCount;
        rewardAmount = MiniGameManager.Instance.CalculateRewardAmount();
        txt_HitCount.text = hitCount.ToString();
        txt_Reward.text = rewardAmount.ToString();
    }


    public void OnClick_ClaimReward()
    {
        DataManager.Instance.IncreaseGems(rewardAmount);
        UIManager.instance.ui_Navigation.OnClick_MenuActivate(4);
        MiniGameManager.Instance.targetHitCount = 0;
        MiniGameManager.Instance.rewardAmount = 0;
        UIManager.instance.ui_MiniGame.ui_MiniGamePlay.gameObject.SetActive(false);
        UIManager.instance.ui_Navigation.gameObject.SetActive(true);
        UIManager.instance.ui_UseableResouce.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
