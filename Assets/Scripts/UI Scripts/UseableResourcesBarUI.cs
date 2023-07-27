using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseableResourcesBarUI : MonoBehaviour
{
    [SerializeField] private Image img_Profile;

    [SerializeField] private float coins;
    [SerializeField] private float tropies;
    [SerializeField] private float gems;

    [SerializeField] private TextMeshProUGUI txt_TropiesCount;
    [SerializeField] private TextMeshProUGUI txt_CoinsCount;
    [SerializeField] private TextMeshProUGUI txt_GemsCount;

    [SerializeField] private float lerpTimer;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        tropies = DataManager.Instance.totalTrophies;
        coins = DataManager.Instance.totalCoins;
        gems = DataManager.Instance.totalGems;


        txt_TropiesCount.text = tropies.ToString("F0");
        txt_CoinsCount.text = coins.ToString("F0");
        txt_GemsCount.text = gems.ToString("F0");

    }


    public IEnumerator CoinAnimation()
    {
        float currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / lerpTimer;
            coins = Mathf.Lerp(coins, DataManager.Instance.totalCoins, currentTime);
            txt_CoinsCount.text = coins.ToString("F0");
            yield return null;
        }
    }

    public IEnumerator TrophiesAnimation()
    {
        float currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / lerpTimer;
            tropies = Mathf.Lerp(tropies, DataManager.Instance.totalTrophies, currentTime);
            txt_TropiesCount.text = tropies.ToString("F0");
            yield return null;
        }
    }

    public IEnumerator GemsAnimation()
    {
        float currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / lerpTimer;
            gems = Mathf.Lerp(gems, DataManager.Instance.totalGems, currentTime);
            txt_GemsCount.text = gems.ToString("F0");
            yield return null;
        }
    }

    public void OnClick_BuyCoin()
    {

        UIManager.instance.ui_Shop.gameObject.SetActive(true);

        UIManager.instance.ui_Navigation.OnClick_MenuActivate(0);

        Vector2 position = new Vector2(0, 250);
        UIManager.instance.ui_Shop.ScrollDownAnimation(position);
    }

    public void OnClick_BuyGems()
    {

        UIManager.instance.ui_Shop.gameObject.SetActive(true);

        UIManager.instance.ui_Navigation.OnClick_MenuActivate(0);

        Vector2 position = new Vector2(0, 700);
        UIManager.instance.ui_Shop.ScrollDownAnimation(position);
    }

    public void OnClick_OpenProfile()
    {
        UIManager.instance.ui_PlayerProfile.gameObject.SetActive(true);
    }
}
