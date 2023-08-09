using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private RectTransform scrollviewContext;
    [SerializeField] private float flt_ScrollAnimationDuration = 0.2f;


    [Header("Skip'its Data")]
    [SerializeField] private int[] all_SkipitsBuyAmount;
    [SerializeField] private int[] all_SkipitsRewardAmount;
    [SerializeField] private TextMeshProUGUI[] all_TextSkipitsRewardAmount;
    [SerializeField] private TextMeshProUGUI[] all_SkipitsBuy;
    [SerializeField] private Image[] all_SkipitsRewardIcons;

    [Header("Coin Data")]
    [SerializeField] private int[] all_CoinBuyAmount;
    [SerializeField] private int[] all_CoinRewardAmount;
    [SerializeField] private TextMeshProUGUI[] all_TextCoinRewardAmount;
    [SerializeField] private TextMeshProUGUI[] all_CoinBuy;
    [SerializeField] private Image[] all_CoinRewardIcons;

    [Header("gems Data")]
    [SerializeField] private int[] all_GemsBuyAmount;
    [SerializeField] private int[] all_GemsRewardAmount;
    [SerializeField] private TextMeshProUGUI[] all_TextGemsRewardAmount;
    [SerializeField] private TextMeshProUGUI[] all_GemsBuy;
    [SerializeField] private Image[] all_GemsIcons;


    private void OnEnable()
    {
        //Set All Data
        SetAllShopBuyData();
    }

    private void SetAllShopBuyData()
    {
        for (int i = 0; i < all_SkipitsBuy.Length; i++)
        {
            all_SkipitsBuy[i].text =  "$" +all_SkipitsBuyAmount[i].ToString();
            all_TextSkipitsRewardAmount[i].text = all_SkipitsRewardAmount[i].ToString();
        }



        for (int i = 0; i < all_CoinBuy.Length; i++)
        {
            all_CoinBuy[i].text = all_CoinBuyAmount[i].ToString();
            all_TextCoinRewardAmount[i].text =  all_CoinRewardAmount[i].ToString();
        }

        for (int i = 0; i < all_GemsBuy.Length; i++)
        {
            all_GemsBuy[i].text ="$"+ all_GemsBuyAmount[i].ToString();
            all_TextGemsRewardAmount[i].text =  all_GemsRewardAmount[i].ToString();
        }
    }

    public void ScrollDownAnimation(Vector2 position)
    {
        if (this.gameObject.activeInHierarchy)
        {
            scrollviewContext.DOAnchorPos(position, flt_ScrollAnimationDuration);
        }
    }


    public void OnClick_BuyCheast(int index)
    {
      


        UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
    }

    public void OnClick_BuyCoins(int index)
    {
        if (!DataManager.Instance.IsEnoughGemssForPurchase(all_CoinBuyAmount[index]))
        {
            Debug.Log("Not Enough Gems");
            return;
        }
        UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
        UIManager.instance.ui_RewardSummary.SetRewardSummaryData(all_CoinRewardIcons[index].sprite, all_CoinRewardAmount[index].ToString());
        DataManager.Instance.IncreaseCoins(all_CoinRewardAmount[index]);
        DataManager.Instance.DecreaseGems(all_CoinBuyAmount[index]);
    }

    public void OnClick_BuyGems(int index)
    {
        UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
        UIManager.instance.ui_RewardSummary.SetRewardSummaryData(all_GemsIcons[index].sprite, all_GemsRewardAmount[index].ToString());
        int gemsAmount = all_GemsRewardAmount[index];
        DataManager.Instance.IncreaseGems(gemsAmount) ;
    }

    public void OnClick_BuySkipits(int index)
    {
        UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
        UIManager.instance.ui_RewardSummary.SetRewardSummaryData(all_SkipitsRewardIcons[index].sprite, all_SkipitsRewardAmount[index].ToString());
        int skipits = all_SkipitsRewardAmount[index];
        DataManager.Instance.IncreaseSkipIt(skipits);
    }
}
