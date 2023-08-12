using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDetailsUI : MonoBehaviour
{
    [SerializeField] private Image img_Icon;
    [SerializeField] private Image img_PlayerBG;
    public RectTransform level_Conrainer;
    [SerializeField] private TextMeshProUGUI txt_PlayerLevel;
    public Image img_BGGlow;
    public Image img_ForegroundGlow;
    [SerializeField] private TextMeshProUGUI txt_CurrentCards;
    [SerializeField] private TextMeshProUGUI txt_RequireCards;
    public Image img_LevelUpIndigator;
    public Slider slider_Cards;
    public Image img_SliderFill;

    public Image img_Lock;

    public void SetPlayerBG(Sprite _bg)
    {
        img_PlayerBG.sprite = _bg;

    }

    public void SetPlayerData(int _index, Sprite _icon, int _playerLevel, int _currentCards, int _requireCards)
    {

        if (PlayerManager.Instance.IsPlayerReachMaxLevel(_index))
        {
            txt_PlayerLevel.text = "Max";
        }
        else
        {
            txt_PlayerLevel.text = (_playerLevel + 1).ToString();
        }

        img_Icon.sprite = _icon;
        txt_CurrentCards.text = _currentCards.ToString();
        txt_RequireCards.text = _requireCards.ToString();
        slider_Cards.maxValue = _requireCards;
        slider_Cards.value = _currentCards;

    }
}
