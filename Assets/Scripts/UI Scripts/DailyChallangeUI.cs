using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailyChallangeUI : MonoBehaviour
{
    public TextMeshProUGUI txt_CurrentRuns;
    public TextMeshProUGUI txt_RequireRuns;

    public int currentPlayerRuns;
    public int requireRuns;

    public bool canClaimReward;


    private void OnEnable()
    {
        currentPlayerRuns = DataManager.Instance.GetPlayerTotalRuns();

        canClaimReward = false;
        if(currentPlayerRuns >= requireRuns)
        {
            canClaimReward = true;
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
            Debug.Log("Reward Screen Show");
        }
    }
}
