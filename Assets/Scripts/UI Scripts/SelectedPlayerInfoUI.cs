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


    [SerializeField] private Button btn_Select;
    [SerializeField] private Button btn_Upgrade;



    public void OnEnable()
    {

        btn_Select.gameObject.SetActive(true);
        btn_Upgrade.gameObject.SetActive(true);
        img_LevelUpIndicator.gameObject.SetActive(false);


        SetPlayerUpdateData(index);

        img_FillSlider.sprite = sprite_GreenFill;

        if (PlayerManager.Instance.all_Players[index].playerState == PlayerState.NoCards)
        {
            btn_Select.gameObject.SetActive(false);
            btn_Upgrade.gameObject.SetActive(false);
        } else if (PlayerManager.Instance.all_Players[index].playerState == PlayerState.EnoughCardsForUpgrade)
        {
            img_FillSlider.sprite = sprite_BlueFill;
            SetPlayerUpdateData(index);
        }

        int currentPlayerLevel = PlayerManager.Instance.all_Players[index].currentLevel;
        int currentCards = PlayerManager.Instance.all_Players[index].currentCards;
        int requireCards = PlayerManager.Instance.all_Players[index].requireCardsToUnlock[currentPlayerLevel];

        img_PlayerIcon.sprite = PlayerManager.Instance.all_Players[index].image;
        txt_PlayerName.text = PlayerManager.Instance.all_Players[index].name;

        txt_PlayerLevel.text = "Level " + currentPlayerLevel.ToString() ;

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
        int currentLevel = PlayerManager.Instance.all_Players[index].currentLevel;
        txt_NewUltimatime.gameObject.SetActive(false);
        txt_NewBatForce.gameObject.SetActive(false);
        txt_NewBowlingForce.gameObject.SetActive(false);
        txt_NewBowingSwing.gameObject.SetActive(false);

        float newBatForce = PlayerManager.Instance.all_Players[index].maxBatForce[currentLevel + 1] - PlayerManager.Instance.all_Players[index].maxBatForce[currentLevel];

        float newBowlingForce = PlayerManager.Instance.all_Players[index].maxBallForce[currentLevel + 1] - PlayerManager.Instance.all_Players[index].maxBallForce[currentLevel];

        float newBowlingSwing = PlayerManager.Instance.all_Players[index].maxSwingForce[currentLevel + 1] - PlayerManager.Instance.all_Players[index].maxSwingForce[currentLevel];

        float newultimateCount = PlayerManager.Instance.all_Players[index].ultimateCount[currentLevel + 1] - PlayerManager.Instance.all_Players[index].ultimateCount[currentLevel];


        if (PlayerManager.Instance.all_Players[index].playerState == PlayerState.EnoughCardsForUpgrade)
        {
            if(PlayerManager.Instance.all_Players[index].maxBatForce[currentLevel] != PlayerManager.Instance.all_Players[index].maxBatForce[currentLevel + 1])
            {
                txt_NewBatForce.gameObject.SetActive(true);
                txt_NewBatForce.text = "+" + newBatForce; 
            }
            if (PlayerManager.Instance.all_Players[index].maxBallForce[currentLevel] != PlayerManager.Instance.all_Players[index].maxBallForce[currentLevel + 1])
            {
                txt_NewBowlingForce.gameObject.SetActive(true);
                txt_NewBowlingForce.text = "+" + newBowlingForce;
            }
            if (PlayerManager.Instance.all_Players[index].maxSwingForce[currentLevel] != PlayerManager.Instance.all_Players[index].maxSwingForce[currentLevel + 1])
            {
                txt_NewBowingSwing.gameObject.SetActive(true);
                txt_NewBowingSwing.text = "+" + newBowlingSwing;
            }
            if (PlayerManager.Instance.all_Players[index].ultimateCount[currentLevel] != PlayerManager.Instance.all_Players[index].ultimateCount[currentLevel + 1])
            {
                txt_NewUltimatime.gameObject.SetActive(true);
                txt_NewUltimatime.text = "+" + newultimateCount;
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
        float currentTime = 0;
        while(currentTime < 1)
        {

            int currentPlayerLevel = PlayerManager.Instance.all_Players[index].currentLevel ;
            currentTime += Time.deltaTime / 1;
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

        StartCoroutine(SliderAnimation());
    }

    private IEnumerator SliderAnimation()
    {
        float currentTime = 0;
        while(currentTime < 1)
        {
            currentTime += Time.deltaTime / 5f;
            slider_Cards.value = Mathf.Lerp(slider_Cards.value, 0, currentTime);
            yield return null;
        }
        img_LevelUpIndicator.gameObject.SetActive(false);
        img_FillSlider.sprite = sprite_GreenFill;
    }

    public void OnClick_SelectPlayer()
    {
        this.gameObject.SetActive(false);
        DataManager.Instance.activePlayerIndex = index;
        DataManager.Instance.SetActivePlayerIndex(index);
        UIManager.instance.ui_PlayerSelect.SetCurrentActivePlayerData();
    }

    public void OnClick_UpgradePlayer()
    {
        PlayerManager.Instance.all_Players[index].currentLevel++;
        PlayerManager.Instance.all_Players[index].playerState = PlayerState.HasCards;
        PlayerManager.Instance.all_Players[index].currentCards = 0;
        UIManager.instance.ui_PlayerSelect.SetAllPlayerData(index);
        StartCoroutine(AnimationPlayerState());
    }




    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }

}
