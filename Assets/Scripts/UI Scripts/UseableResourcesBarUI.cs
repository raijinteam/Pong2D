using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseableResourcesBarUI : MonoBehaviour
{
    [SerializeField] private Image img_Profile;
    [SerializeField] private TextMeshProUGUI txt_TropiesCount;
    [SerializeField] private TextMeshProUGUI txt_CoinsCount;
    [SerializeField] private TextMeshProUGUI txt_GemsCount;

    private void OnEnable()
    {
        txt_TropiesCount.text = DataManager.Instance.trophies.ToString();
        txt_CoinsCount.text = DataManager.Instance.gameCoins.ToString();
    }


    public void SetCoinsUI()
    {
        txt_CoinsCount.text = DataManager.Instance.gameCoins.ToString();
    }

    public void SetTrophiesUI()
    {
        txt_TropiesCount.text = DataManager.Instance.trophies.ToString();
    }
}
