using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedPlayerInfoUI : MonoBehaviour
{
    public int index;

    private float ultimateCount;
    private float batForce;
    private float ballForce;
    private float swingForce;
    private int currentSelectedPlayerLevel;

    [SerializeField] private TextMeshProUGUI txt_PlayerName;
    [SerializeField] private Image img_PlayerIcon;
    [SerializeField] private TextMeshProUGUI txt_PlayerLevel;
    [SerializeField] private Image img_LevelUpIndicator;

    [Header("Bat Force")]
    [SerializeField] private TextMeshProUGUI txt_CurrentBatForce;
    [SerializeField] private TextMeshProUGUI txt_NewBatForce;
    [Header("ball Force")]
    [SerializeField] private TextMeshProUGUI txt_CurrentBallForce;
    [SerializeField] private TextMeshProUGUI txt_NewBowlingForce;
    [Header("Bowing Swing")]
    [SerializeField] private TextMeshProUGUI txt_CurrentSwing;
    [SerializeField] private TextMeshProUGUI txt_NewBowingSwing;
    [Header("Ultimatem Count")]
    [SerializeField] private TextMeshProUGUI txt_CurrentUltimatime;
    [SerializeField] private TextMeshProUGUI txt_NewUltimatime;


    [SerializeField] private Slider slider_Cards;
    [SerializeField] private Image img_FillSlider;
    [SerializeField] private Sprite sprite_BlueFill;
    [SerializeField] private Sprite sprite_GreenFill;


    [SerializeField] private TextMeshProUGUI txt_CurrentCards;
    [SerializeField] private TextMeshProUGUI txt_RequireCards;
    [SerializeField] private TextMeshProUGUI txt_Description;
    [SerializeField] private TextMeshProUGUI txt_PlayerBuyAmount;

    [SerializeField] private Button btn_Select;
    [SerializeField] private Button btn_Upgrade;

    private int playerBuyAmount;

    private bool canPlayerClick;

    public void OnEnable()
    {

        canPlayerClick = true;

        btn_Select.gameObject.SetActive(true);
        btn_Upgrade.gameObject.SetActive(true);
        btn_Upgrade.interactable = false;
        img_LevelUpIndicator.gameObject.SetActive(false);


        SetPlayerUpdateData(index);

        img_FillSlider.sprite = sprite_GreenFill;

        if (PlayerManager.Instance.all_Players[index].playerState == PlayerState.NoCards)
        {
            btn_Select.gameObject.SetActive(false);
            btn_Upgrade.gameObject.SetActive(false);
        }
        else if (PlayerManager.Instance.all_Players[index].playerState == PlayerState.EnoughCardsForUpgrade)
        {
            img_FillSlider.sprite = sprite_BlueFill;
            btn_Upgrade.interactable = true;
            img_LevelUpIndicator.gameObject.SetActive(true);
            SetPlayerUpdateData(index);
        }

        currentSelectedPlayerLevel = PlayerManager.Instance.all_Players[index].currentLevel;
        if (PlayerManager.Instance.IsPlayerReachMaxLevel(index))
        {
            txt_PlayerLevel.text = "Max";
            btn_Upgrade.interactable = false;
        }
        else
        {
            txt_PlayerLevel.text = (currentSelectedPlayerLevel + 1).ToString();
            playerBuyAmount = PlayerManager.Instance.all_Players[index].levelIncreaseAmount[currentSelectedPlayerLevel];
            txt_PlayerBuyAmount.text = playerBuyAmount.ToString(); ;
        }

        int currentCards = PlayerManager.Instance.all_Players[index].currentCards;
        int requireCards = PlayerManager.Instance.all_Players[index].requireCardsToUnlock[currentSelectedPlayerLevel];

        img_PlayerIcon.sprite = PlayerManager.Instance.all_Players[index].image;
        txt_PlayerName.text = PlayerManager.Instance.all_Players[index].name;


        txt_CurrentCards.text = currentCards.ToString();
        txt_RequireCards.text = requireCards.ToString();


        slider_Cards.maxValue = requireCards;
        slider_Cards.value = currentCards;
        Debug.Log("Current Cards :" + currentCards);
        Debug.Log("Slider value : " + slider_Cards.value);


        SetPlayerPropertiesData();

    }


    private void SetPlayerUpdateData(int index)
    {
        currentSelectedPlayerLevel = PlayerManager.Instance.all_Players[index].currentLevel;
        txt_NewUltimatime.gameObject.SetActive(false);
        txt_NewBatForce.gameObject.SetActive(false);
        txt_NewBowlingForce.gameObject.SetActive(false);
        txt_NewBowingSwing.gameObject.SetActive(false);

        float newBatForce = PlayerManager.Instance.all_Players[index].maxBatForce[currentSelectedPlayerLevel + 1] - PlayerManager.Instance.all_Players[index].maxBatForce[currentSelectedPlayerLevel];

        float newBowlingForce = PlayerManager.Instance.all_Players[index].maxBallForce[currentSelectedPlayerLevel + 1] - PlayerManager.Instance.all_Players[index].maxBallForce[currentSelectedPlayerLevel];

        float newBowlingSwing = PlayerManager.Instance.all_Players[index].maxSwingForce[currentSelectedPlayerLevel + 1] - PlayerManager.Instance.all_Players[index].maxSwingForce[currentSelectedPlayerLevel];

        float newultimateCount = PlayerManager.Instance.all_Players[index].ultimateCount[currentSelectedPlayerLevel + 1] - PlayerManager.Instance.all_Players[index].ultimateCount[currentSelectedPlayerLevel];


        if (PlayerManager.Instance.all_Players[index].playerState == PlayerState.EnoughCardsForUpgrade)
        {
            if (PlayerManager.Instance.all_Players[index].maxBatForce[currentSelectedPlayerLevel] != PlayerManager.Instance.all_Players[index].maxBatForce[currentSelectedPlayerLevel + 1])
            {
                txt_NewBatForce.gameObject.SetActive(true);
                txt_NewBatForce.text = "+" + Mathf.Abs( newBatForce);
            }
            if (PlayerManager.Instance.all_Players[index].maxBallForce[currentSelectedPlayerLevel] != PlayerManager.Instance.all_Players[index].maxBallForce[currentSelectedPlayerLevel + 1])
            {
                txt_NewBowlingForce.gameObject.SetActive(true);
                txt_NewBowlingForce.text = "+" + Mathf.Abs( newBowlingForce);
            }
            if (PlayerManager.Instance.all_Players[index].maxSwingForce[currentSelectedPlayerLevel] != PlayerManager.Instance.all_Players[index].maxSwingForce[currentSelectedPlayerLevel + 1])
            {
                txt_NewBowingSwing.gameObject.SetActive(true);
                txt_NewBowingSwing.text = "+" + Mathf.Abs( newBowlingSwing);
            }
            if (PlayerManager.Instance.all_Players[index].ultimateCount[currentSelectedPlayerLevel] != PlayerManager.Instance.all_Players[index].ultimateCount[currentSelectedPlayerLevel + 1])
            {
                txt_NewUltimatime.gameObject.SetActive(true);
                txt_NewUltimatime.text = "+" +Mathf.Abs( newultimateCount);
            }
        }


    }


    private void SetPlayerPropertiesData()
    {
        int currentLevel = PlayerManager.Instance.all_Players[index].currentLevel;
        ultimateCount = PlayerManager.Instance.all_Players[index].ultimateCount[currentLevel];
        batForce = PlayerManager.Instance.all_Players[index].maxBatForce[currentLevel];
        ballForce = PlayerManager.Instance.all_Players[index].maxBallForce[currentLevel];
        swingForce = PlayerManager.Instance.all_Players[index].maxSwingForce[currentLevel];

        txt_CurrentUltimatime.text = ultimateCount.ToString();
        txt_CurrentBatForce.text = batForce.ToString();
        txt_CurrentBallForce.text = ballForce.ToString();
        txt_CurrentSwing.text = swingForce.ToString();
    }


    private IEnumerator AnimationPlayerState()
    {
        canPlayerClick = false;
        btn_Upgrade.interactable = false;
        float currentTime = 0;
        while (currentTime < 1)
        {

            int currentPlayerLevel = PlayerManager.Instance.all_Players[index].currentLevel;
            currentTime += Time.deltaTime / 1f;
            ultimateCount = Mathf.Lerp(ultimateCount, PlayerManager.Instance.all_Players[index].ultimateCount[currentPlayerLevel], currentTime);

            batForce = Mathf.Lerp(batForce, PlayerManager.Instance.all_Players[index].maxBatForce[currentPlayerLevel], currentTime);

            ballForce = Mathf.Lerp(ballForce, PlayerManager.Instance.all_Players[index].maxBallForce[currentPlayerLevel], currentTime);

            swingForce = Mathf.Lerp(swingForce, PlayerManager.Instance.all_Players[index].maxSwingForce[currentPlayerLevel], currentTime);

            txt_CurrentUltimatime.text = ultimateCount.ToString("F0");
            txt_CurrentBatForce.text = batForce.ToString("F0");
            txt_CurrentBallForce.text = ballForce.ToString("F0");
            txt_CurrentSwing.text = swingForce.ToString("F0");

            yield return null;
        }
        SetPlayerUpdateData(index);

    }

    private IEnumerator SliderAnimation()
    {
        float currentTime = 0;
        slider_Cards.value = 0;
        img_LevelUpIndicator.gameObject.SetActive(false);
        img_FillSlider.sprite = sprite_GreenFill;

        currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / 1f;
            int currentCard = PlayerManager.Instance.all_Players[index].currentCards;
            slider_Cards.value = Mathf.Lerp(0, currentCard, currentTime);
            yield return null;
        }


        //if its tutorial then upgrade complete then go to home screen
        if (DataManager.Instance.isGameFirstTimeLoad)
        {
            UIManager.instance.ui_Tutorial.gameObject.SetActive(false);
            DataManager.Instance.isGameFirstTimeLoad = false;
            DataManager.Instance.SetBasicTutorialState(DataManager.Instance.isGameFirstTimeLoad);
            UIManager.instance.ui_Navigation.OnClick_MenuActivate(2);
            this.gameObject.SetActive(false);
            UIManager.instance.ui_Tutorial.toutorialState = TutorialState.MiniGame;
        }

        if (PlayerManager.Instance.IsEnoughCardsForUpgradePlayer(index))
        {
            Debug.Log("Has Cards for Upgrade");
            btn_Upgrade.interactable = true;
            img_LevelUpIndicator.gameObject.SetActive(true);
            img_FillSlider.sprite = sprite_BlueFill;
            PlayerManager.Instance.all_Players[index].playerState = PlayerState.EnoughCardsForUpgrade;
        }
        canPlayerClick = true;
    }



    public void OnClick_SelectPlayer()
    {
        if (canPlayerClick && !DataManager.Instance.isGameFirstTimeLoad)
        {
            DataManager.Instance.activePlayerIndex = index;
            DataManager.Instance.SetActivePlayerIndex(index);
            UIManager.instance.ui_PlayerSelect.SetCurrentActivePlayerData();
            this.gameObject.SetActive(false);
        }

    }

    public void OnClick_UpgradePlayer()
    {


        



        if (!DataManager.Instance.IsEnoughCoinsForPurchase(playerBuyAmount))
        {
            return;
        }

        if (index == DataManager.Instance.activePlayerIndex)
            UIManager.instance.ui_PlayerSelect.SetCurrentActivePlayerData();



        DataManager.Instance.DecreaseCoins(playerBuyAmount); // USe Coins


        

        //Decrease hero cards
        int playerCurrentCard = PlayerManager.Instance.all_Players[index].currentCards;
        int playerRequireCards = PlayerManager.Instance.all_Players[index].requireCardsToUnlock[PlayerManager.Instance.all_Players[index].currentLevel];
        PlayerManager.Instance.all_Players[index].currentCards = playerCurrentCard - playerRequireCards;
        playerBuyAmount = PlayerManager.Instance.all_Players[index].levelIncreaseAmount[PlayerManager.Instance.all_Players[index].currentLevel];

        DataManager.Instance.SetHeroCard(index, PlayerManager.Instance.all_Players[index].currentCards);

        //first increase player level
        PlayerManager.Instance.IncreasePlayerLevel(index);

        //Set Here Player Data
        txt_PlayerLevel.text = PlayerManager.Instance.all_Players[index].currentLevel.ToString();
        txt_CurrentCards.text = PlayerManager.Instance.all_Players[index].currentCards.ToString();
        txt_RequireCards.text = PlayerManager.Instance.all_Players[index].requireCardsToUnlock[PlayerManager.Instance.all_Players[index].currentLevel].ToString();
        txt_PlayerBuyAmount.text = playerBuyAmount.ToString(); ;



        //set selected player data in palyer select ui
        UIManager.instance.ui_PlayerSelect.SetAllPlayerData(index);

        PlayerManager.Instance.all_Players[index].playerState = PlayerState.HasCards;

        StartCoroutine(AnimationPlayerState());
        StartCoroutine(SliderAnimation());
    }




    public void OnClick_Close()
    {
        if (canPlayerClick && !DataManager.Instance.isGameFirstTimeLoad)
        {
            this.gameObject.SetActive(false);
        }
    }

}
