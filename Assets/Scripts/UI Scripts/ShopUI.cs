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


    [Header("Cheast Data")]
    [SerializeField] private int[] all_CheastBuyAmount;
    [SerializeField] private TextMeshProUGUI[] all_CheastBuy;
    [SerializeField] private Image[] all_CheastRewardIcons;

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
        for(int i =0; i < all_CheastBuy.Length; i++)
        {
            all_CheastBuy[i].text = all_CheastBuyAmount[i].ToString();
        }

        for (int i = 0; i < all_CoinBuy.Length; i++)
        {
            all_CoinBuy[i].text = all_CoinBuyAmount[i].ToString();
            all_TextCoinRewardAmount[i].text = "x" + all_CoinRewardAmount[i].ToString();
        }

        for (int i = 0; i < all_GemsBuy.Length; i++)
        {
            all_GemsBuy[i].text ="$"+ all_GemsBuyAmount[i].ToString();
            all_TextGemsRewardAmount[i].text = "x" + all_GemsRewardAmount[i].ToString();
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
        if (!DataManager.Instance.IsEnoughGemssForPurchase(all_CheastBuyAmount[index]))
        {
            Debug.Log("Not Enough Gems");
            return;
        }


        UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
        //UIManager.instance.ui_RewardSummary.SetRewardSummaryData(all_CheastRewardIcons[index].sprite, all_CheastBuy[index].ToString());
        DataManager.Instance.DecreaseGems(all_CheastBuyAmount[index]);
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



}
