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

    public GameObject allMenus;
    public HomePanelUI ui_HomePanel;
    public PlauerMatchMakerUI ui_MatchMaker;
    public GameOverUI ui_GameOver;
    public PlayerScreenUI ui_PlayScreen;
    public TimerScreenUI ui_TimeScreen;
    public RewardSummaryUI ui_RewardSummary;
    public AchivementUI ui_Achievement;
    public DailyRewardUI ui_DailyReward;
    public RewardSlotsUI ui_RewardSlot;
    public SlotTimerUI ui_SlotTimer;
}
