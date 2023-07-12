using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScreenUI : MonoBehaviour
{
    public TextMeshProUGUI txt_GameTime;
    public TextMeshProUGUI txt_Score;
    public TextMeshProUGUI txt_Wickets;


    private void Start()
    {
        txt_GameTime.text = GameManager.instance.currentActiveGameTime.ToString("00:00");
        txt_Score.text = GameManager.instance.roundRunsCount.ToString();
        txt_Wickets.text = GameManager.instance.roundWicketCount.ToString();
    }
}
