using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Tutorial")]
    public TutorialUI ui_Tutorial;

    [Header("All Scripts")]
    public GameObject allMenus;
    public HomePanelUI ui_HomePanel;
    public PlauerMatchMakerUI ui_MatchMaker;
    public GameOverUI ui_GameOver;
    public PlayerScreenUI ui_PlayScreen;
    public TimerScreenUI ui_TimeScreen;
    public RewardSummaryUI ui_RewardSummary;
    public DailyMissionUI ui_Achievement;
    public DailyRewardUI ui_DailyReward;
    public RewardSlotsUI ui_RewardSlot;
    public SlotTimerUI ui_SlotTimer;
    public UseableResourcesBarUI ui_UseableResouce;
    public SelectedPlayerInfoUI ui_SelectedPlayerInfo;
    public NavigationUI ui_Navigation;
    public ShopUI ui_Shop;
    public PlayerSelectUI ui_PlayerSelect;
    public PlayerProfileUI ui_PlayerProfile;
    public FortuneWheelSpinUI ui_DailySpin;
    public GameObject ui_Reward;
    public MiniGameUI ui_MiniGame;
    public NotEnoughCurrencyPopUpUI ui_CurrencyPopup;
    public DailyChallangeUI ui_DailyChallange;

    [Header("Required Components")]
    public GameObject pf_LevelUpPopUp;
    public RectTransform popUpSpwanPosition;
    public RectTransform cardAnimationPosition;



    public void LevelUpPopUpCalled()
    {
        Instantiate(pf_LevelUpPopUp, popUpSpwanPosition.position, Quaternion.identity , popUpSpwanPosition);
    }
}
