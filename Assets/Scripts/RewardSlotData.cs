using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSlotData : MonoBehaviour
{
    [SerializeField] private bool isFreeSlot;

    public bool isRewardGive;
    public bool isRewardCollected;

    public TextMeshProUGUI txt_EmptyBag;
    public Image img_BG;
    public Image img_SlotIcon;
    public TextMeshProUGUI txt_SlotName;
    public TextMeshProUGUI txt_SlotTimer;
    public Image img_Timer;


    public void EmptySlot()
    {
        txt_EmptyBag.gameObject.SetActive(true);
        img_SlotIcon.gameObject.SetActive(false);
        txt_SlotTimer.gameObject.SetActive(false);
        txt_SlotName.gameObject.SetActive(false);
        img_Timer.gameObject.SetActive(false);
    }

    public void ShowAllObjects()
    {
        txt_EmptyBag.gameObject.SetActive(false);
        img_SlotIcon.gameObject.SetActive(true);
        txt_SlotTimer.gameObject.SetActive(true);
        txt_SlotName.gameObject.SetActive(true);
        img_Timer.gameObject.SetActive(true);
    }

    public void SetSlotTime(float _time)
    {
        float hours = _time / 3600;
        float minutes = (_time % 3600) / 60;
        float seconds = _time % 60;


        txt_SlotTimer.text = $"{(int)hours} : {(int)minutes} : {(int)seconds}";
    }

    public void SetAllSlotData(Sprite _bagSprite , string _slotName , int _time)
    {
        img_SlotIcon.sprite = _bagSprite;
        txt_SlotName.text = _slotName;
        txt_SlotTimer.text = _time.ToString();
    }
}
