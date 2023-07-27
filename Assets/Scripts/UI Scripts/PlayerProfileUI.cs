using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerProfileUI : MonoBehaviour
{


    [SerializeField] private Image img_PaddleIcon;
    [SerializeField] private TextMeshProUGUI txt_PlayerLevel;
    [SerializeField] private Slider slider_PlayerXP;
    [SerializeField] private TextMeshProUGUI txt_CurrentXP;
    [SerializeField] private TextMeshProUGUI txt_RequiredXP;



    private void OnEnable()
    {
        int currentActivePlauer = DataManager.Instance.activePlayerIndex;

        img_PaddleIcon.sprite = PlayerManager.Instance.all_Players[currentActivePlauer].image;
        txt_PlayerLevel.text = DataManager.Instance.playerLevel.ToString();
        txt_CurrentXP.text = DataManager.Instance.playerXP.ToString();

        int requireXP = (int)DataManager.Instance.requireXPForLevelup;

        txt_RequiredXP.text = requireXP.ToString();

        slider_PlayerXP.maxValue = requireXP;

        slider_PlayerXP.value = DataManager.Instance.playerXP;
    }


    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }
}
