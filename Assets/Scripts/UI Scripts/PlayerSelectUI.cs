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

    [Header("Active Player UI")]
    [SerializeField] private Image img_SelectedPlayer;
    [SerializeField] private TextMeshProUGUI txt_SelectedPlayerLevel;
    [SerializeField] private TextMeshProUGUI txt_ActivePlayerSpeed;
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
            all_PlayerDetails[upgradeIndex].img_SliderFill.color = Color.blue;
            if (PlayerManager.Instance.all_Players[upgradeIndex].playerState == PlayerState.HasCards)
            {

                Sprite sprite = PlayerManager.Instance.all_Players[upgradeIndex].image;

                PlayerManager.Instance.all_Players[upgradeIndex].currentCards++;
                Debug.Log("CUrrent Cards : " + PlayerManager.Instance.all_Players[upgradeIndex].currentCards);
                int currentCards = PlayerManager.Instance.all_Players[upgradeIndex].currentCards;

                int requireCards = PlayerManager.Instance.all_Players[upgradeIndex].requireCardsToUnlock[PlayerManager.Instance.all_Players[upgradeIndex].currentLevel];

                all_PlayerDetails[upgradeIndex].SetPlayerData(sprite, currentCards, requireCards);
                PlayerManager.Instance.CheckIfPlayerHasEnoughCardsForUpgrade(upgradeIndex);

                if (PlayerManager.Instance.all_Players[upgradeIndex].playerState == PlayerState.EnoughCardsForUpgrade)
                {
                    all_PlayerDetails[upgradeIndex].img_SliderFill.color = Color.yellow;
                    all_PlayerDetails[upgradeIndex].img_UpgradeIcon.gameObject.SetActive(true);
                }
            }
        }
    }


    public void SetCurrentActivePlayerData()
    {
        int currentActivePlayerIndex = DataManager.Instance.activePlayerIndex;

        Sprite playerSprite = PlayerManager.Instance.all_Players[currentActivePlayerIndex].image;

        int currentPlayerLevel = PlayerManager.Instance.all_Players[currentActivePlayerIndex].currentLevel;

        int currentCards = PlayerManager.Instance.all_Players[currentActivePlayerIndex].currentCards;

        int requireCards = PlayerManager.Instance.all_Players[currentActivePlayerIndex].requireCardsToUnlock[currentPlayerLevel];
       
        img_SliderFill.color = Color.blue;

        if(PlayerManager.Instance.IsEnoughCardsForUpgradePlayer(currentActivePlayerIndex))
            img_SliderFill.color = Color.yellow;

        txt_ActivePlayerSpeed.text = PlayerManager.Instance.all_Players[currentActivePlayerIndex].moveSpeed[currentPlayerLevel].ToString();
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


        if(PlayerManager.Instance.all_Players[index].playerState == PlayerState.NoCards)
        {
            all_PlayerDetails[index].slider_Cards.gameObject.SetActive(false);
        }

        all_PlayerDetails[index].img_SliderFill.color = Color.blue;
        all_PlayerDetails[index].img_UpgradeIcon.gameObject.SetActive(false);


        if (PlayerManager.Instance.IsEnoughCardsForUpgradePlayer(index))
        {
            all_PlayerDetails[index].img_SliderFill.color = Color.yellow;
            all_PlayerDetails[index].img_UpgradeIcon.gameObject.SetActive(true);
        }


        Sprite playerSprite = PlayerManager.Instance.all_Players[index].image;

        int currentPlayerLevel = PlayerManager.Instance.all_Players[index].currentLevel;

        int currentCards = PlayerManager.Instance.all_Players[index].currentCards;

        int requireCards = PlayerManager.Instance.all_Players[index].requireCardsToUnlock[currentPlayerLevel];

        all_PlayerDetails[index].SetPlayerData(playerSprite, currentCards, requireCards);
    }


    public void OnClick_PlayerDetails(int _index)
    {

        if (PlayerManager.Instance.all_Players[_index].playerState == PlayerState.NoCards)
            return;

        UIManager.instance.ui_SelectedPlayerInfo.index = _index;
        UIManager.instance.ui_SelectedPlayerInfo.gameObject.SetActive(true);
    }

}
