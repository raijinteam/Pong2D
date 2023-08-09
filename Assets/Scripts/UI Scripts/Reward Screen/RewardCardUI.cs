using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RewardCardUI : MonoBehaviour
{

    [SerializeField] private float flt_AnimationDuration = 0.5f;
    [SerializeField] private Image img_Reward;
    [SerializeField] private TextMeshProUGUI txt_RewardAmount;

    private void OnEnable()
    {
        //CardAnimation();
    }


    public void SetRewardCardData(Color _icon , int _amount)
    {
        img_Reward.color = _icon;
        txt_RewardAmount.text = "x" + _amount.ToString();
    }

    private void CardAnimation()
    {
        Vector3 position = UIManager.instance.cardAnimationPosition.position;
        Debug.Log("Position : " + position);
        transform.DOMove(position, flt_AnimationDuration);
        transform.DOLocalRotate(new Vector3(0, 180, 0), flt_AnimationDuration);
    }
}
