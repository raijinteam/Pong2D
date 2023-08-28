using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelectUI : MonoBehaviour
{

    [Header("Only For Dubbing")]
    [SerializeField] private int upgradeIndex;

    [Header("All Player")]
    public PlayerDetailsUI[] all_PlayerDetails;
    [SerializeField] private Sprite sprite_greenFill;
    [SerializeField] private Sprite sprite_blueFill;


    [Header("Player BG Sprite")]
    [SerializeField] private Color commom_Glow;
    [SerializeField] private Color common_BottomGlow;
    [SerializeField] private Color rare_Glow;
    [SerializeField] private Color rare_BottomGlow;
    [SerializeField] private Color epic_Glow;
    [SerializeField] private Color epic_BottomGlow;
    [SerializeField] private Sprite sprite_CommonBG;

    [SerializeField] private Sprite sprite_RareBG;

    [SerializeField] private Sprite sprite_EpicBG;

    [Header("Active Player UI")]
    [SerializeField] private Image img_SelectedPlayer;
    [SerializeField] private TextMeshProUGUI txt_SelectedPlayerLevel;
    [SerializeField] private TextMeshProUGUI txt_UltimateCount;
    [SerializeField] private TextMeshProUGUI txt_ActivePlayerBatForce;
    [SerializeField] private TextMeshProUGUI txt_ActivePlayerBallForce;
    [SerializeField] private TextMeshProUGUI txt_ActivePlayerSwingForce;

    [SerializeField] private TextMeshProUGUI txt_SelectedPlayerCurrentCards;
    [SerializeField] private TextMeshProUGUI txt_SelectedPlayerRequireCards;
    [SerializeField] private Slider slider_Card;
    [SerializeField] private Image img_SliderFill;




    private void OnEnable()
    {

        //Set Current Active Player Data
        SetCurrentActivePlayerData();

        //Set All Player Data
        for (int i = 0; i < all_PlayerDetails.Length; i++)
        {
            SetAllPlayerData(i);
        }

    }


    private void Update()
    {
        //this is only for testing
        if (Input.GetKeyDown(KeyCode.U))
        {

            Debug.Log("Upgrade Player");
            if (PlayerManager.Instance.all_Players[upgradeIndex].playerState == PlayerState.HasCards)
            {
                all_PlayerDetails[upgradeIndex].img_SliderFill.sprite = sprite_greenFill;
                
            }else if (PlayerManager.Instance.all_Players[upgradeIndex].playerState == PlayerState.EnoughCardsForUpgrade)
            {
                all_PlayerDetails[upgradeIndex].img_SliderFill.sprite = sprite_blueFill;
                all_PlayerDetails[upgradeIndex].img_LevelUpIndigator.gameObject.SetActive(true);
            }
                IncreasePlayerCards(1);
        }
    }


    private void IncreasePlayerCards(int amount)
    {
        Sprite sprite = PlayerManager.Instance.all_Players[upgradeIndex].image;
        PlayerManager.Instance.all_Players[upgradeIndex].currentCards += amount;
        int currentPlayerLevel = PlayerManager.Instance.all_Players[upgradeIndex].currentLevel;
        int currentCards = PlayerManager.Instance.all_Players[upgradeIndex].currentCards;

        int requireCards = PlayerManager.Instance.all_Players[upgradeIndex].requireCardsToUnlock[currentPlayerLevel];

        all_PlayerDetails[upgradeIndex].SetPlayerData(upgradeIndex, sprite, currentPlayerLevel, currentCards, requireCards);
        PlayerManager.Instance.SetPlayerState(upgradeIndex);
    }


    public void SetCurrentActivePlayerData()
    {
        int currentActivePlayerIndex = DataManager.Instance.activePlayerIndex;

        Sprite playerSprite = PlayerManager.Instance.all_Players[currentActivePlayerIndex].image;

        int currentPlayerLevel = PlayerManager.Instance.all_Players[currentActivePlayerIndex].currentLevel;

        int currentCards = PlayerManager.Instance.all_Players[currentActivePlayerIndex].currentCards;

        int requireCards = PlayerManager.Instance.all_Players[currentActivePlayerIndex].requireCardsToUnlock[currentPlayerLevel];

        img_SliderFill.sprite = sprite_blueFill;

        if (PlayerManager.Instance.IsEnoughCardsForUpgradePlayer(currentActivePlayerIndex))
            img_SliderFill.sprite = sprite_greenFill;

        txt_UltimateCount.text = PlayerManager.Instance.all_Players[currentActivePlayerIndex].ultimateCount[currentPlayerLevel].ToString();
        txt_ActivePlayerBatForce.text = PlayerManager.Instance.all_Players[currentActivePlayerIndex].maxBatForce[currentPlayerLevel].ToString();
        txt_ActivePlayerBallForce.text = PlayerManager.Instance.all_Players[currentActivePlayerIndex].maxBallForce[currentPlayerLevel].ToString();
        txt_ActivePlayerSwingForce.text = PlayerManager.Instance.all_Players[currentActivePlayerIndex].maxSwingForce[currentPlayerLevel].ToString();

        img_SelectedPlayer.sprite = playerSprite;
        txt_SelectedPlayerCurrentCards.text = currentCards.ToString();
        txt_SelectedPlayerRequireCards.text = requireCards.ToString();
        slider_Card.maxValue = requireCards;
        slider_Card.value = currentCards;
    }

    public void SetAllPlayerData(int index)
    {
        all_PlayerDetails[index].img_Lock.gameObject.SetActive(false);

        //Set Player State
        PlayerManager.Instance.SetPlayerState(index);

        all_PlayerDetails[index].slider_Cards.gameObject.SetActive(true);

        if (PlayerManager.Instance.all_Players[index].playerState == PlayerState.NoCards)
        {
            all_PlayerDetails[index].slider_Cards.gameObject.SetActive(false);
            all_PlayerDetails[index].level_Conrainer.gameObject.SetActive(false);
            all_PlayerDetails[index].img_Lock.gameObject.SetActive(true);
        }


        all_PlayerDetails[index].img_SliderFill.sprite = sprite_greenFill;
        all_PlayerDetails[index].img_LevelUpIndigator.gameObject.SetActive(false);


        if (PlayerManager.Instance.IsEnoughCardsForUpgradePlayer(index))
        {
            //Debug.Log(index + " This player has enough cards for upgrade");
           // Debug.Log("Current Cards : " + PlayerManager.Instance.all_Players[index].currentCards + " Require Cards : " + PlayerManager.Instance.all_Players[index].requireCardsToUnlock[PlayerManager.Instance.all_Players[index].currentLevel]);
            all_PlayerDetails[index].img_SliderFill.sprite = sprite_blueFill;
            all_PlayerDetails[index].img_LevelUpIndigator.gameObject.SetActive(true);
        }

        SetPlayerBG(index);

        Sprite playerSprite = PlayerManager.Instance.all_Players[index].image;

        int currentPlayerLevel = PlayerManager.Instance.all_Players[index].currentLevel;

        int currentCards = PlayerManager.Instance.all_Players[index].currentCards;

        int requireCards = PlayerManager.Instance.all_Players[index].requireCardsToUnlock[currentPlayerLevel];

        all_PlayerDetails[index].SetPlayerData(index, playerSprite, currentPlayerLevel, currentCards, requireCards);
    }

    private void SetPlayerBG(int index) //Set player bg accroding to player type
    {
        if (PlayerManager.Instance.all_Players[index].playerType == PlayerType.Common)
        {
            all_PlayerDetails[index].SetPlayerBG(sprite_CommonBG);
            all_PlayerDetails[index].img_BGGlow.color = commom_Glow;
            all_PlayerDetails[index].img_ForegroundGlow.color = common_BottomGlow;

        }
        else if (PlayerManager.Instance.all_Players[index].playerType == PlayerType.Rare)
        {
            all_PlayerDetails[index].SetPlayerBG(sprite_RareBG);
            all_PlayerDetails[index].img_BGGlow.color = rare_Glow;
            all_PlayerDetails[index].img_ForegroundGlow.color = rare_BottomGlow;
        }
        else if (PlayerManager.Instance.all_Players[index].playerType == PlayerType.Epic)
        {
            all_PlayerDetails[index].SetPlayerBG(sprite_EpicBG);
            all_PlayerDetails[index].img_BGGlow.color = epic_Glow;
            all_PlayerDetails[index].img_ForegroundGlow.color = epic_BottomGlow;
        }
    }

    public void OnClick_PlayerDetails(int _index)
    {

        if (PlayerManager.Instance.all_Players[_index].playerState == PlayerState.NoCards)
            return;

        if (!DataManager.Instance.isGameFirstTimeLoad || (UIManager.instance.ui_Tutorial.toutorialState == TutorialState.PlayerDetails && _index == 0))
        {
            UIManager.instance.ui_SelectedPlayerInfo.index = _index;
            UIManager.instance.ui_SelectedPlayerInfo.gameObject.SetActive(true);
            UIManager.instance.ui_Tutorial.toutorialState = TutorialState.UpgradePlayer;
        }else if (!DataManager.Instance.isGameFirstTimeLoad)
        {
            UIManager.instance.ui_SelectedPlayerInfo.index = _index;
            UIManager.instance.ui_SelectedPlayerInfo.gameObject.SetActive(true);
        }
    }

}
