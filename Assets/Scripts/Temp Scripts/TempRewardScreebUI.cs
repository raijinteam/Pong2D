using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;


public class TempRewardScreebUI : MonoBehaviour
{
    [Header("Require Components")]
    [SerializeField] private RewardCardUI1[] all_Cards;
    [SerializeField] private Transform tf_CardEndPosition;
    [SerializeField] private Transform tf_CardSpawnPosition;
    [SerializeField] private TextMeshProUGUI txt_RewardAmount;

    [Header("Coins")]
    [SerializeField] private Transform coinCard;
    [SerializeField] private Transform coinPanel;
    [SerializeField] private TextMeshProUGUI txt_CoinAmount;


    [Header("Gems")]
    [SerializeField] private Transform gemsCard;
    [SerializeField] private Transform gemsPanel;
    [SerializeField] private TextMeshProUGUI txt_GemsAmount;

    [Header("Skipits up")]
    [SerializeField] private Transform skipitCard;
    [SerializeField] private Transform skipitPanel;
    [SerializeField] private TextMeshProUGUI txt_SkipitsAmount;

    [Header("Player ")]
    [SerializeField] private Transform playerCard;
    [SerializeField] private Transform playerDetails;
    [SerializeField] private TextMeshProUGUI txt_PlayerName;
    [SerializeField] private TextMeshProUGUI txt_PlayerType;
    [SerializeField] private TextMeshProUGUI txt_PlayerLevel;
    [SerializeField] private Slider slider_Player;
    [SerializeField] private TextMeshProUGUI txt_CardValue;


    public List<Sprite> list_Rewardicon = new List<Sprite>();
    public List<string> list_RewardAmount = new List<string>();

    [Header("Properies")]
    [SerializeField] private float flt_AnimationDuration;
    [SerializeField] private int cardIndex = 0;
    [SerializeField] private int slotFinishedIndex;
    [SerializeField] private int numberOfRewards;
    private int currentNumberOfRewards;
    private bool isFirstcard;


    private void OnEnable()
    {
        isFirstcard = true;

        ResetAllObjects();
        numberOfRewards = 0;
        cardIndex = 0;

        slotFinishedIndex = SlotsManager.Instance.slotFinishedIndex;
        numberOfRewards = SlotsManager.Instance.allSlots[slotFinishedIndex].numberOfRewards;
        currentNumberOfRewards = numberOfRewards;
        txt_RewardAmount.text = numberOfRewards.ToString();

        for (int i = 0; i < numberOfRewards; i++)
        {
            all_Cards[i].gameObject.SetActive(true);
            list_Rewardicon.Add(SlotsManager.Instance.allSlots[slotFinishedIndex].list_RewardIcons[i]);
            list_RewardAmount.Add(SlotsManager.Instance.allSlots[slotFinishedIndex].list_RewardAmount[i]);
        }
    }


    private void Update()
    {

        if (isFirstcard)
        {
            AnimateCardAndDetails(cardIndex);
            cardIndex++;
            currentNumberOfRewards--;
            txt_RewardAmount.text = currentNumberOfRewards.ToString();

        }
        if (Input.GetMouseButtonDown(0))
        {
            if (currentNumberOfRewards <= 0)
            {
                UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
                SetDataInRewardSummaryScreen();

                this.gameObject.SetActive(false);
            }

            if (cardIndex > 0)
            {
                StopPreviousAnimations(cardIndex - 1);
            }

            if (cardIndex < all_Cards.Length)
            {
                AnimateCardAndDetails(cardIndex);
                cardIndex++;
                currentNumberOfRewards--;
                txt_RewardAmount.text = currentNumberOfRewards.ToString();
            }
        }
    }


    private void StopPreviousAnimations(int cardIndex)
    {
        switch (cardIndex)
        {
            case 0:
                all_Cards[cardIndex].DOKill();
                coinCard.DOKill();
                coinPanel.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                coinPanel.localScale = Vector3.zero;
                break;
            case 1:
                gemsCard.DOKill();
                gemsPanel.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                gemsPanel.localScale = Vector3.zero;
                break;
            case 2:
                skipitCard.DOKill();
                skipitPanel.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                skipitPanel.localScale = Vector3.zero;
                break;
            case 3:
                playerCard.DOKill();
                playerDetails.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                playerDetails.localScale = Vector3.zero;
                break;
            case 4:
                playerCard.DOKill();
                playerDetails.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                playerDetails.localScale = Vector3.zero;
                break;
            case 5:
                playerCard.DOKill();
                playerDetails.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                playerDetails.localScale = Vector3.zero;
                break;
            case 6:
                playerCard.DOKill();
                playerDetails.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                playerDetails.localScale = Vector3.zero;
                break;
            case 7:
                playerCard.DOKill();
                playerDetails.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                playerDetails.localScale = Vector3.zero;
                break;
            case 8:
                playerCard.DOKill();
                playerDetails.DOKill();
                all_Cards[cardIndex].gameObject.SetActive(false);
                playerDetails.localScale = Vector3.zero;
                break;
        }
    }

    private void AnimateCardAndDetails(int cardIndex)
    {
        if (cardIndex < numberOfRewards)
        {
            Debug.Log("INdex : " + cardIndex);
            switch (cardIndex)
            {
                case 0:
                    AnimateCard(all_Cards[0].transform);
                    coinCard.DOMove(tf_CardEndPosition.position, 1f).OnComplete(() => AnimateDetails(coinPanel, txt_CoinAmount));
                    txt_CoinAmount.text = list_RewardAmount[0];
                    isFirstcard = false;
                    break;
                case 1:
                    AnimateCard(all_Cards[1].transform);
                    gemsCard.DOMove(tf_CardEndPosition.position, 1f).OnComplete(() => AnimateDetails(gemsPanel, txt_GemsAmount));
                    txt_GemsAmount.text = list_RewardAmount[1];
                    break;
                case 2:
                    AnimateCard(all_Cards[2].transform);
                    skipitCard.DOMove(tf_CardEndPosition.position, 1f).OnComplete(() => AnimateDetails(skipitPanel, txt_SkipitsAmount));
                    txt_SkipitsAmount.text = list_RewardAmount[2];
                    break;
                case 3:
                    AnimateCard(all_Cards[3].transform);
                    playerCard.DOMove(tf_CardEndPosition.position, 1f).OnComplete(() => AnimateDetails(playerDetails, txt_CoinAmount));
                    SetPlayerDetails(3);
                    break;
                case 4:
                    AnimateCard(all_Cards[4].transform);
                    playerCard.DOMove(tf_CardEndPosition.position, 1f).OnComplete(() => AnimateDetails(playerDetails, txt_CoinAmount));
                    SetPlayerDetails(4);
                    break;
                case 5:
                    AnimateCard(all_Cards[5].transform);
                    playerCard.DOMove(tf_CardEndPosition.position, 1f).OnComplete(() => AnimateDetails(playerDetails, txt_CoinAmount));
                    SetPlayerDetails(5);
                    break;
                case 6:
                    AnimateCard(playerCard);
                    playerCard.DOMove(tf_CardEndPosition.position, 1f).OnComplete(() => AnimateDetails(playerDetails, txt_CoinAmount));
                    break;
                case 7:
                    AnimateCard(playerCard);
                    playerCard.DOMove(tf_CardEndPosition.position, 1f).OnComplete(() => AnimateDetails(playerDetails, txt_CoinAmount));
                    break;
                case 8:
                    break;
            }
        }
    }

    private void AnimateCard(Transform card)
    {
        Sequence seq = DOTween.Sequence();
        card.DOMove(tf_CardEndPosition.position, flt_AnimationDuration);
        seq.Append(card.DORotate(new Vector3(0, 180, 0), flt_AnimationDuration / 2)).Append(card.DORotate(new Vector3(0, 360, 0), flt_AnimationDuration / 2).OnComplete(SetRewardItems));
    }

    private void SetRewardItems()
    {
        all_Cards[cardIndex - 1].SetRewardImage(list_Rewardicon[cardIndex - 1], list_RewardAmount[cardIndex - 1]);
    }


    private void AnimateDetails(Transform details, TextMeshProUGUI txt)
    {
        details.DOScale(Vector3.one, flt_AnimationDuration);
        txt.DOFade(1, flt_AnimationDuration);
    }

    private void ResetAnimationDetails(Transform details, TextMeshProUGUI txt)
    {
        details.DOScale(Vector3.zero, 0.001f);
        txt.DOFade(0, 0.001f);
    }


    private void SetPlayerDetails(int index)
    {
        int playerIndex = int.Parse(list_RewardAmount[index]);

        txt_PlayerName.text = PlayerManager.Instance.all_Players[playerIndex].name;
        txt_PlayerLevel.text = (PlayerManager.Instance.all_Players[playerIndex].currentLevel + 1).ToString();
        txt_PlayerType.text = PlayerManager.Instance.all_Players[playerIndex].playerType.ToString();
        StartCoroutine(PlayerSliderAnimation(playerIndex , 5));
    }

    private IEnumerator PlayerSliderAnimation(int _playerIndex , int updateCards)
    {
        ResetSlider();

        int currentPlayerLevel = PlayerManager.Instance.all_Players[_playerIndex].currentLevel;
        slider_Player.maxValue = PlayerManager.Instance.all_Players[_playerIndex].requireCardsToUnlock[currentPlayerLevel];
        int currentCards =  PlayerManager.Instance.all_Players[_playerIndex].currentCards;
        slider_Player.value = currentCards;
            currentCards += updateCards;
        PlayerManager.Instance.all_Players[_playerIndex].currentCards = currentCards;
        txt_CardValue.text = $"{currentCards} / {PlayerManager.Instance.all_Players[_playerIndex].requireCardsToUnlock[currentPlayerLevel]}";
        DataManager.Instance.SetHeroCard(_playerIndex, currentCards);

        float timer = 0;
        yield return new WaitForSeconds(flt_AnimationDuration);
        while(timer < 1)
        {
            timer += Time.deltaTime / 2f;
            Debug.Log("Current Player Cards : " + currentCards);
            slider_Player.value = Mathf.Lerp(0, currentCards, timer);
            yield return null;
        }
        PlayerManager.Instance.SetPlayerState(_playerIndex);
    }

    private void ResetSlider()
    {
        slider_Player.value = 0;
    }

    private void ResetGemsPanel()
    {
        Debug.Log("Reset Gems Panel");
        ResetAnimationDetails(gemsPanel, txt_GemsAmount);
    }

    private void ResetAllObjects()
    {

        for (int i = 0; i < all_Cards.Length; i++)
        {
            all_Cards[i].transform.position = tf_CardSpawnPosition.position;
            all_Cards[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < numberOfRewards; i++)
        {
            all_Cards[i].DisableImage();
        }
        ResetAnimationDetails(coinPanel, txt_CoinAmount);
        ResetGemsPanel();
        ResetAnimationDetails(skipitPanel, txt_SkipitsAmount);
        ResetAnimationDetails(playerDetails, txt_CoinAmount);
    }

    private void SetDataInRewardSummaryScreen()
    {
        UIManager.instance.ui_RewardSummary.SetMultiplRewardSummaryData(list_Rewardicon, list_RewardAmount);
        list_RewardAmount.Clear();
        list_Rewardicon.Clear();
        SlotsManager.Instance.allSlots[slotFinishedIndex].list_RewardAmount.Clear();
        SlotsManager.Instance.allSlots[slotFinishedIndex].list_RewardIcons.Clear();
    }
}

