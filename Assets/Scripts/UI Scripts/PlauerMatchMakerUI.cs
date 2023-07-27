using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlauerMatchMakerUI : MonoBehaviour
{

    [SerializeField] private float flt_DelayTime;
    [SerializeField] private TextMeshProUGUI txt_Waitting;
    [SerializeField] private TextMeshProUGUI txt_CurrentPlayerTorpies;
    [SerializeField] private TextMeshProUGUI txt_WinAmount;

    private float currentDelayTime;

    private void OnEnable()
    {
        currentDelayTime = flt_DelayTime;
        txt_CurrentPlayerTorpies.text = DataManager.Instance.totalTrophies.ToString();
        txt_WinAmount.text = "Price " + GameManager.instance.all_LevelWinningPrice[GameManager.instance.currentLevelIndex].ToString(); ;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentDelayTime -= Time.deltaTime;
        txt_Waitting.text = "Match Start in " + (int)currentDelayTime + "..";
        if(currentDelayTime <= 0)
        {
            this.gameObject.SetActive(false);
            UIManager.instance.ui_UseableResouce.gameObject.SetActive(false);
            UIManager.instance.ui_TimeScreen.gameObject.SetActive(true);
            //GameManager.instance.StartGame();
            currentDelayTime = flt_DelayTime;
        }
    }
}
