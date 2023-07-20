using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotTimerUI : MonoBehaviour
{

    public int index;
    [SerializeField] private TextMeshProUGUI txt_SlotName;
    [SerializeField] private Image img_SlotIcon;
    [SerializeField] private Image img_RewardIcon;
    [SerializeField] private TextMeshProUGUI txt_UnlockTime;
    [SerializeField] private TextMeshProUGUI txt_NumberOfRewards;
    [SerializeField] private TextMeshProUGUI txt_GemsForUnlockReward;

    private float unlockTime;

    private void Update()
    {
        CalculateUnlockTime(TimeManager.Instance.currentSlotTime[index]);
    }

    public void SetSlotTimerData(string _name , Sprite _icon , float _unlockTime , int _numberOfRewards , int _requireAmountForUnlock)
    {
        txt_SlotName.text = _name;
        img_SlotIcon.sprite = _icon;
        CalculateUnlockTime(_unlockTime);
        txt_NumberOfRewards.text = "x"+_numberOfRewards.ToString();
        txt_GemsForUnlockReward.text = _requireAmountForUnlock.ToString();
    }

    private void CalculateUnlockTime(float unlockTime)
    {
        float hours = unlockTime / 3600;
        float minutes = (unlockTime % 3600) / 60;
        float seconds = unlockTime % 60;

        txt_UnlockTime.text = $"{(int)hours} : {(int)minutes} : {(int)seconds}";
    }


    public void OnClick_CompleteTimeWithGems()
    {
        DataManager.Instance.SetRewardSlotState(index, SlotState.RewardGenrated);
        UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();
        TimeManager.Instance.currentSlotTime[index] = SlotsManager.Instance.allSlots[index].timer;
        SlotsManager.Instance.isAnotherSlotHasActiveTime = false;
    }

    public void OnClick_ReduceTimeWithAds()
    {
        TimeManager.Instance.currentSlotTime[index] -= 60;
    }


    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }
}
