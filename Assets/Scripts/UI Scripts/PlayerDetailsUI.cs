using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDetailsUI : MonoBehaviour
{
    [SerializeField] private Image img_Icon;
    [SerializeField] private TextMeshProUGUI txt_CurrentCards;
    [SerializeField] private TextMeshProUGUI txt_RequireCards;
    public Image img_UpgradeIcon;
    public Slider slider_Cards;
    public Image img_SliderFill;





    public void SetPlayerData(Sprite _icon , int _currentCards , int _requireCards)
    {
        img_Icon.sprite = _icon;
        txt_CurrentCards.text = _currentCards.ToString();
        txt_RequireCards.text = _requireCards.ToString();
        slider_Cards.maxValue = _requireCards;
        slider_Cards.value = _currentCards;

    }
}
