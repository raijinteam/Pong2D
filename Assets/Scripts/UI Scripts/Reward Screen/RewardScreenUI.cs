using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RewardScreenUI : MonoBehaviour
{
    [Header("Require Components")]
    [SerializeField] private GameObject pf_Card;
    [SerializeField] private Transform tf_CardSpawnPosition;
    [SerializeField] private TextMeshProUGUI txt_NumberOfRewards;
    public Transform tf_CardAnimationPosition;
    [SerializeField] private Image[] all_Cards;

    [Header("Coins Properites")]
    [SerializeField] private Image img_CoinDetailsContainer;
    [SerializeField] private TextMeshProUGUI txt_ConisAmount;

    [Header("Gems Properites")]
    [SerializeField] private Image img_GemsDetailsContainer;
    [SerializeField] private TextMeshProUGUI txt_GemsAmount;

    [Header("Skip it Properites")]
    [SerializeField] private Image img_SlipItDetailsContainer;
    [SerializeField] private TextMeshProUGUI txt_SkipItAmount;

    [Header("Cards Properites")]
    [SerializeField] private Image img_CardDetailsContainer;
    [SerializeField] private TextMeshProUGUI txt_PlayerType;
    [SerializeField] private TextMeshProUGUI txt_PlayerLevel;
    [SerializeField] private Slider slider_PlayerSlider;

    
    [Header("Properites")]
    [SerializeField] private float flt_AnimationDuration = 0.5f;


    [SerializeField] private int index = 0;
    private int numberOfRewards;
    private int currentNumberOfRewards;
    private int slotFinishedIndex;
    [SerializeField] private bool isCardAnimated;

    private void OnEnable()
    {
        index = 0;

        slotFinishedIndex = SlotsManager.Instance.slotFinishedIndex;
        numberOfRewards = SlotsManager.Instance.allSlots[slotFinishedIndex].numberOfRewards;

        ResetAllThings();

         currentNumberOfRewards = numberOfRewards;
        txt_NumberOfRewards.text = numberOfRewards.ToString();
       // Debug.Log("Number Of Rewards : " + numberOfRewards);


        for (int i = 0; i < numberOfRewards; i++)
        {
            all_Cards[i].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //close screen if player touch more then rewards amount
            if (index >= numberOfRewards)
            {
                this.gameObject.SetActive(false);
            }


            //Debug.Log("Before Called index : " + index);
            StartAnimation();
            currentNumberOfRewards--;
            txt_NumberOfRewards.text = currentNumberOfRewards.ToString();
            //Debug.Log("After Called index : " + index);
        }

    }


    private void ResetAllThings()
    {
        for (int i = 0; i < numberOfRewards; i++)
        {
            all_Cards[i].transform.position = tf_CardSpawnPosition.position;
            all_Cards[i].gameObject.SetActive(false);
            all_Cards[i].transform.rotation = Quaternion.identity;
        }

        ResetGemsDetails();

        ResetCoinDetails();

        ResetSkipItDetails();

        ResetCardDetails();

        index = 0;
        isCardAnimated = false;
    }


    private void StartAnimation()
    {
        if (isCardAnimated)
        {
            all_Cards[index - 1].gameObject.SetActive(false);
            StopCoroutine(ShowCoinDetailsAnimationCO());
            ResetCoinDetails();
            if(index == 2)
            {
                Debug.Log(" Stop Gems Corutine");
                StopCoroutine(ShowGemsDetailsCO());
                ResetGemsDetails();
            }
            if(index == 3)
            {
                Debug.Log("Stop Skip it up Corutine ");
                ResetGemsDetails();
                ResetSkipItDetails();
            }
            if(index == 4)
            {
                ResetSkipItDetails();
                ResetCardDetails();
            }
        }

        // CardAnimation();
        Sequence seq = DOTween.Sequence();
        if(index == 0)
        {
            seq.AppendCallback(CardAnimation).AppendInterval(flt_AnimationDuration).OnComplete(ShowCoinDetails);
        }
        else if(index == 1)
        {
            ResetCoinDetails();
            seq.AppendCallback(CardAnimation).AppendInterval(flt_AnimationDuration).OnComplete(ShowGemsDetails);
        }
        else if(index == 2)
        {
            ResetGemsDetails();
            seq.AppendCallback(CardAnimation).AppendInterval(flt_AnimationDuration).OnComplete(ShowSkipItDetails);
        }else if(index == 3)
        {
            ResetSkipItDetails();
            seq.AppendCallback(CardAnimation).AppendInterval(flt_AnimationDuration).OnComplete(ShowCardDetails);
        }else if(index == 4){
            ResetCardDetails();
            seq.AppendCallback(CardAnimation);
        }else if(index == 5)
        {
            seq.AppendCallback(CardAnimation);
        }else if(index == 6)
        {
            seq.AppendCallback(CardAnimation);
        }else if (index == 7)
        {
            seq.AppendCallback(CardAnimation);
        }
        isCardAnimated = true;
        
    }

    private void CardAnimation()
    {
        Sequence seq = DOTween.Sequence();
        //Debug.Log("Index" + index);
        all_Cards[index].transform.DOMove(tf_CardAnimationPosition.position, flt_AnimationDuration);

        seq.Append(all_Cards[index].transform.DOLocalRotate(new Vector3(0, 180, 0), flt_AnimationDuration / 2)).
            Append(all_Cards[index].transform.DOLocalRotate(new Vector3(0, 360, 0), flt_AnimationDuration / 2));
        index++;
    }


    private IEnumerator ShowCoinDetailsAnimationCO()
    {
         Sequence se = DOTween.Sequence();
        txt_ConisAmount.text = SlotsManager.Instance.allSlots[slotFinishedIndex].coinReward.ToString();
        se.Append(img_CoinDetailsContainer.transform.DOScale(1, flt_AnimationDuration)).
            Append(txt_ConisAmount.DOFade(1 , flt_AnimationDuration));
        yield return null;
    }

    private void ShowCoinDetails()
    {
        StartCoroutine(ShowCoinDetailsAnimationCO());

       /* Sequence se = DOTween.Sequence();
        txt_ConisAmount.text = SlotsManager.Instance.allSlots[slotFinishedIndex].coinReward.ToString();
        se.Append(img_CoinDetailsContainer.transform.DOScale(1, flt_AnimationDuration)).
            Append(txt_ConisAmount.DOFade(1 , flt_AnimationDuration));*/
    }
    private void ResetCoinDetails()
    {
        img_CoinDetailsContainer.transform.DOScale(0, 0.0001f);
        txt_ConisAmount.DOFade(0, 0.0001f);
    }


    private IEnumerator ShowGemsDetailsCO()
    {
        txt_GemsAmount.text = SlotsManager.Instance.allSlots[slotFinishedIndex].gemsReward.ToString();
        Sequence se = DOTween.Sequence();
        se.Append(img_GemsDetailsContainer.transform.DOScale(1, flt_AnimationDuration)).
            Append(txt_GemsAmount.DOFade(1, flt_AnimationDuration));
        yield return null;
    }
    private void ShowGemsDetails()
    {
        StartCoroutine(ShowGemsDetailsCO());

        /*txt_GemsAmount.text = SlotsManager.Instance.allSlots[slotFinishedIndex].gemsReward.ToString();
        Sequence se = DOTween.Sequence();
        se.Append(img_GemsDetailsContainer.transform.DOScale(1, flt_AnimationDuration)).
            Append(txt_GemsAmount.DOFade(1, flt_AnimationDuration));*/
    }
    private void ResetGemsDetails()
    {
        img_GemsDetailsContainer.transform.DOScale(0, 0.0001f);
        txt_GemsAmount.DOFade(0, 0.0001f);
    }


    private void ShowSkipItDetails()
    {
        Sequence se = DOTween.Sequence();
        txt_SkipItAmount.text = SlotsManager.Instance.allSlots[slotFinishedIndex].skipItUpReward.ToString();
        se.Append(img_SlipItDetailsContainer.transform.DOScale(1, flt_AnimationDuration)).
            Append(txt_SkipItAmount.DOFade(1, flt_AnimationDuration));
    }
    private void ResetSkipItDetails()
    {
        img_SlipItDetailsContainer.transform.DOScale(0, 0.0001f);
        txt_SkipItAmount.DOFade(0, 0.0001f);
    }


    private void ShowCardDetails()
    {
        img_CardDetailsContainer.transform.DOScale(1, flt_AnimationDuration);
        txt_PlayerType.DOFade(1, flt_AnimationDuration * 2);
        txt_PlayerLevel.DOFade(1, flt_AnimationDuration * 2);
        slider_PlayerSlider.transform.DOScaleX(1, flt_AnimationDuration * 2);
    }

    private void ResetCardDetails()
    {
        img_CardDetailsContainer.transform.DOScale(0, 0.001f);
        txt_PlayerType.DOFade(0, 0.001f);
        txt_PlayerLevel.DOFade(0, 0.001f);
        slider_PlayerSlider.transform.DOScaleX(0, 0.001f);
    }
}

[System.Serializable]
public struct CardData
{
    public Sprite cardSprite;
    public int rewardAmount;
}
