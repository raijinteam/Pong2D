using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyChallangeUI : MonoBehaviour
{
    public Image img_Cheast;
    public Image img_Runs;
    public TextMeshProUGUI txt_CurrentRuns;
    public TextMeshProUGUI txt_RequireRuns;
    public TextMeshProUGUI txt_Open;

    public int currentPlayerRuns;
    public int requireRuns;

    public bool canClaimReward;


    private void OnEnable()
    {
        SetData();
    }


    private void SetData()
    {
        currentPlayerRuns = DataManager.Instance.GetPlayerTotalRuns();

        img_Runs.gameObject.SetActive(true);
        txt_Open.gameObject.SetActive(false);
        canClaimReward = false;
        if (currentPlayerRuns >= requireRuns)
        {
            canClaimReward = true;
            img_Runs.gameObject.SetActive(false);
            txt_Open.gameObject.SetActive(true);
        }


        txt_CurrentRuns.text = currentPlayerRuns.ToString();
        txt_RequireRuns.text = requireRuns.ToString();
    }

    public void OnClick_ClaimReward()
    {
        if (canClaimReward)
        {
            canClaimReward = false;
            currentPlayerRuns = 0;
            DataManager.Instance.SetPlayerTotalRuns(0);
            GameManager.instance.playerTotalRuns = DataManager.Instance.GetPlayerTotalRuns();
            txt_CurrentRuns.text = GameManager.instance.playerTotalRuns.ToString();
            UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
            UIManager.instance.ui_RewardSummary.SetRewardSummaryData(img_Cheast.sprite, 50.ToString());
            Debug.Log("Reward Screen Show");
            SetData();
        }
    }
}
