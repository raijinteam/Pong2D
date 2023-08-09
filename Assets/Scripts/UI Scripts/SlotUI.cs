using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    public RectTransform img_SlotClick;
    public TextMeshProUGUI txt_FilledSlotTimer;
    public TextMeshProUGUI txt_RunningSlotTimer;
    public GameObject img_EmptySlot;
    public GameObject img_slotFilled;
    public GameObject img_SlotRunning;
    public GameObject img_SlotFinish;


    public void EmptySlot()
    {
        img_EmptySlot.gameObject.SetActive(true);
        img_slotFilled.gameObject.SetActive(false);
        img_SlotRunning.gameObject.SetActive(false);
        img_SlotFinish.gameObject.SetActive(false);
    }


    public void SetSlotFilled()
    {
        img_EmptySlot.gameObject.SetActive(false);
        img_slotFilled.gameObject.SetActive(true);
        img_SlotRunning.gameObject.SetActive(false);
        img_SlotFinish.gameObject.SetActive(false);
    }

    public void SetSlotRunning()
    {
        img_EmptySlot.gameObject.SetActive(false);
        img_slotFilled.gameObject.SetActive(false);
        img_SlotRunning.gameObject.SetActive(true);
        img_SlotFinish.gameObject.SetActive(false);
    }

    public void SetSlotFinished()
    {
        img_EmptySlot.gameObject.SetActive(false);
        img_slotFilled.gameObject.SetActive(false);
        img_SlotRunning.gameObject.SetActive(false);
        img_SlotFinish.gameObject.SetActive(true);
    }

    public void ShowAllObjects()
    {
        txt_FilledSlotTimer.gameObject.SetActive(true);
        txt_RunningSlotTimer.gameObject.SetActive(true);
    }

    public void SetSlotTime(float _time)
    {
        float hours = _time / 3600;
        float minutes = (_time % 3600) / 60;
        float seconds = _time % 60;


        txt_RunningSlotTimer.text = $"{(int)hours}h {(int)minutes}m {(int)seconds}s";
    }

    public void SetAllSlotData(Sprite _bagSprite, string _slotName, int _time)
    {
        txt_FilledSlotTimer.text = _time.ToString();
        txt_RunningSlotTimer.text = _time.ToString();
    }
}
