using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyChallangeUI : MonoBehaviour
{
    public Image img_Cheast;
    public TextMeshProUGUI txt_Progress;
    public Slider slider_Progress;
    public TextMeshProUGUI txt_Timer;
    public RectTransform panel_ChallangeRunning;
    public RectTransform panel_ChallangeCompleted;
    public RectTransform panel_ChallangeWaitting;

    public float reduceTime;

    public int currentPlayerRuns;
    public int requireRuns;

    public bool isChallangeFinished;
    public bool isRewarsClaimed;



    private void Update()
    {
        //For testing
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseRuns(1);
        }

        if (isRewarsClaimed)
        {
            int timeInSeconds = (int)TimeManager.Instance.currentDailyChallangeTime;

            float hours = timeInSeconds / 3600;
            float minutes = (timeInSeconds % 3600) / 60;
            float seconds = timeInSeconds % 60;


            txt_Timer.text = $"{(int)hours} : {(int)minutes} : {(int)seconds}";
        }
        else if(!isChallangeFinished && !isRewarsClaimed)
        {
            panel_ChallangeRunning.gameObject.SetActive(true);
            panel_ChallangeWaitting.gameObject.SetActive(false);
            isRewarsClaimed = false;
            DataManager.Instance.SetDailyChallangeRewardClaimedState(isRewarsClaimed);
        }

    }


    private void IncreaseRuns(int amount)
    {
        currentPlayerRuns += amount;
        DataManager.Instance.SetPlayerTotalRuns(currentPlayerRuns);
        SetData();
    }

    public void SetData()
    {

        isChallangeFinished = DataManager.Instance.GetDailyChallangeFinishedState();
        isRewarsClaimed = DataManager.Instance.GetDailyChallangeRewardClaimedState();



        if (isChallangeFinished)
            panel_ChallangeCompleted.gameObject.SetActive(true);
        else if (isRewarsClaimed)
            panel_ChallangeWaitting.gameObject.SetActive(true);
        else
            panel_ChallangeRunning.gameObject.SetActive(true);



        slider_Progress.maxValue = requireRuns;
        slider_Progress.value = currentPlayerRuns;
        currentPlayerRuns = DataManager.Instance.GetPlayerTotalRuns();

        if (currentPlayerRuns >= requireRuns)
        {
            isChallangeFinished = true;
            DataManager.Instance.SetDailyChallangeFinishedState(isChallangeFinished);
            panel_ChallangeRunning.gameObject.SetActive(false);
            panel_ChallangeCompleted.gameObject.SetActive(true);
        }

        txt_Progress.text = $"{currentPlayerRuns} / {requireRuns}";
    }



    public void OnClick_SkipTime()
    {
        TimeManager.Instance.currentDailyChallangeTime -= reduceTime;
    }

    public void OnClick_ClaimReward()
    {
        if (isChallangeFinished)
        {
            Debug.Log("Daily Challange Reward Claim");
            panel_ChallangeWaitting.gameObject.SetActive(true);
            panel_ChallangeCompleted.gameObject.SetActive(false);
            isChallangeFinished = false;
            isRewarsClaimed = true;
            DataManager.Instance.SetDailyChallangeFinishedState(isChallangeFinished);
            DataManager.Instance.SetDailyChallangeRewardClaimedState(isRewarsClaimed);
            currentPlayerRuns = 0;
            DataManager.Instance.SetPlayerTotalRuns(0);
            GameManager.instance.playerTotalRuns = DataManager.Instance.GetPlayerTotalRuns();
            txt_Progress.text = GameManager.instance.playerTotalRuns.ToString();
            UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
            UIManager.instance.ui_RewardSummary.SetRewardSummaryData(img_Cheast.sprite, 50.ToString());
            Debug.Log("Reward Screen Show");
            SetData();
        }
    }
}
