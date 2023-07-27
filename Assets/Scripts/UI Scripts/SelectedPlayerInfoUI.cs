using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedPlayerInfoUI : MonoBehaviour
{
    public int index;

    private float moveSpeed;
    private float batForce;
    private float ballForce;
    private float swingForce;


    [SerializeField] private TextMeshProUGUI txt_PlayerName;
    [SerializeField] private Image img_PlayerIcon;
    [SerializeField] private TextMeshProUGUI txt_PlayerLevel;

    [SerializeField] private TextMeshProUGUI txt_Currentspeed;
    [SerializeField] private TextMeshProUGUI txt_Updatespeed;

    [SerializeField] private TextMeshProUGUI txt_CurrentBatForce;
    [SerializeField] private TextMeshProUGUI txt_UpdateBatForce;

    [SerializeField] private TextMeshProUGUI txt_CurrentBallForce;
    [SerializeField] private TextMeshProUGUI txt_UpdateBallForce;

    [SerializeField] private TextMeshProUGUI txt_CurrentSwing;
    [SerializeField] private TextMeshProUGUI txt_UpdateSwing;

    [SerializeField] private Slider slider_Cards;
    [SerializeField] private Image img_FillSlider;

    [SerializeField] private TextMeshProUGUI txt_CurrentCards;
    [SerializeField] private TextMeshProUGUI txt_RequireCards;
    [SerializeField] private TextMeshProUGUI txt_Description;


    [SerializeField] private Button btn_Select;
    [SerializeField] private Button btn_Upgrade;



    public void OnEnable()
    {

        btn_Select.gameObject.SetActive(true);
        btn_Upgrade.gameObject.SetActive(true);

        txt_Updatespeed.gameObject.SetActive(false);
        txt_UpdateBatForce.gameObject.SetActive(false);
        txt_UpdateBallForce.gameObject.SetActive(false);
        txt_UpdateSwing.gameObject.SetActive(false);

        img_FillSlider.color = Color.blue;

        if (PlayerManager.Instance.all_Players[index].playerState == PlayerState.NoCards)
        {
            btn_Select.gameObject.SetActive(false);
            btn_Upgrade.gameObject.SetActive(false);
        } else if (PlayerManager.Instance.all_Players[index].playerState == PlayerState.EnoughCardsForUpgrade)
        {
            img_FillSlider.color = Color.yellow;
            txt_Updatespeed.gameObject.SetActive(true);
        txt_UpdateBallForce.gameObject.SetActive(true);
            txt_UpdateBatForce.gameObject.SetActive(true);
            txt_UpdateSwing.gameObject.SetActive(true);
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


    private void SetPlayerPropertiesData()
    {
        int currentLevel = PlayerManager.Instance.all_Players[index].currentLevel;
        moveSpeed = PlayerManager.Instance.all_Players[index].moveSpeed[currentLevel];
        batForce = PlayerManager.Instance.all_Players[index].maxBatForce[currentLevel];
        ballForce = PlayerManager.Instance.all_Players[index].maxBallForce[currentLevel];
        swingForce = PlayerManager.Instance.all_Players[index].maxSwingForce[currentLevel];

        txt_Currentspeed.text = moveSpeed.ToString();
        txt_CurrentBatForce.text = batForce.ToString();
        txt_CurrentBallForce.text = ballForce.ToString();
        txt_CurrentSwing.text = swingForce.ToString();
    }


    private IEnumerator AnimationPlayerState()
    {
        float currentTime = 0;
        while(currentTime < 1)
        {
            currentTime += Time.deltaTime / 1;
            moveSpeed = Mathf.Lerp(moveSpeed, PlayerManager.Instance.all_Players[index].moveSpeed[PlayerManager.Instance.all_Players[index].currentLevel] , currentTime);


            batForce = Mathf.Lerp(batForce, PlayerManager.Instance.all_Players[index].maxBatForce[PlayerManager.Instance.all_Players[index].currentLevel], currentTime);

            ballForce = Mathf.Lerp(ballForce, PlayerManager.Instance.all_Players[index].maxBallForce[PlayerManager.Instance.all_Players[index].currentLevel], currentTime);

            swingForce = Mathf.Lerp(swingForce, PlayerManager.Instance.all_Players[index].maxSwingForce[PlayerManager.Instance.all_Players[index].currentLevel], currentTime);

            txt_Currentspeed.text = moveSpeed.ToString("F0");
            txt_CurrentBatForce.text = batForce.ToString("F0");
            txt_CurrentBallForce.text = ballForce.ToString("F0");
            txt_CurrentSwing.text = swingForce.ToString("F0");

            yield return null;
        }
        txt_Updatespeed.gameObject.SetActive(false);
        txt_UpdateBatForce.gameObject.SetActive(false);
        txt_UpdateSwing.gameObject.SetActive(false);
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
        img_FillSlider.color = Color.blue;
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
        StartCoroutine(AnimationPlayerState());
        PlayerManager.Instance.all_Players[index].playerState = PlayerState.HasCards;
        PlayerManager.Instance.all_Players[index].currentCards = 0;
        UIManager.instance.ui_PlayerSelect.SetAllPlayerData(index);
    }




    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }

}
