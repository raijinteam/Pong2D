using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerProfileUI : MonoBehaviour
{


    [SerializeField] private Image img_PlayerProfile;
    [SerializeField] private TextMeshProUGUI txt_PlayerLevel;
    [SerializeField] private TMP_InputField input_PlayerName;


    [SerializeField] private string name;

    private void OnEnable()
    {
        int currentActivePlauer = DataManager.Instance.activePlayerIndex;
        name = DataManager.Instance.GetPlayerName();
        input_PlayerName.text = name;
        img_PlayerProfile.sprite = PlayerManager.Instance.all_Players[currentActivePlauer].image;
       // txt_PlayerLevel.text = DataManager.Instance.playerLevel.ToString();
    }

    public void OnClick_SaveName()
    {
        name = input_PlayerName.text;
        DataManager.Instance.SetPlayerName(name);
    }

    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }
}
