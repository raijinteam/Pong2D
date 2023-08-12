using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardCardUI1 : MonoBehaviour
{

    [SerializeField] private Image img_Reward;
    [SerializeField] private TextMeshProUGUI txt_RewardAmount;

    public void DisableImage()
    {
        img_Reward.gameObject.SetActive(false);
    }

    public void SetRewardImage(Sprite sprite , string amount)
    {
        img_Reward.sprite = sprite;
        img_Reward.gameObject.SetActive(true);
        txt_RewardAmount.text = amount;
    }
}
