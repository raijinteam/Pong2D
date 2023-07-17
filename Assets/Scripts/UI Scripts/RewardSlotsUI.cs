using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSlotsUI : MonoBehaviour
{
    public RewardSlotData[] all_RewardSlots;


    private void Update()
    {
        if (!SlotsManager.Instance.isFirstSlotEmpty && !SlotsManager.Instance.isStartTimerSlot1)
        {
            all_RewardSlots[0].ShowAllObjects();
            all_RewardSlots[0].img_BG.color = Color.red;
            all_RewardSlots[0].SetSlotTime(SlotsManager.Instance.unlockTimeSlot1);
        }
        else if (!SlotsManager.Instance.isFirstSlotEmpty && SlotsManager.Instance.isStartTimerSlot1)
        {
            all_RewardSlots[0].SetSlotTime(TimeManager.Instance.currentUnlockTimeSlot1);
            all_RewardSlots[0].img_BG.color = Color.green;
        }
        else if(SlotsManager.Instance.isRewardGiveSlot1 && !all_RewardSlots[0].isRewardCollected)
        {
            all_RewardSlots[0].img_BG.color = Color.yellow;
            all_RewardSlots[0].txt_SlotName.text = "Open";
            all_RewardSlots[0].txt_SlotTimer.text = "0";
        }
        else
        {
            all_RewardSlots[0].EmptySlot();
            all_RewardSlots[0].img_BG.color = Color.black;
        }



        if (!SlotsManager.Instance.isSecondSlotEmpty && !SlotsManager.Instance.isStartTimerSlot2)
        {
            all_RewardSlots[1].SetSlotTime(SlotsManager.Instance.unlockTimeSlot2);
            all_RewardSlots[1].ShowAllObjects();
            all_RewardSlots[1].img_BG.color = Color.red;
            //Debug.Log("Set Data");
        }
        else if (!SlotsManager.Instance.isSecondSlotEmpty && SlotsManager.Instance.isStartTimerSlot2)
        {
            all_RewardSlots[1].SetSlotTime(TimeManager.Instance.currentUnlockTimeSlot2);
            all_RewardSlots[1].img_BG.color = Color.green;
        }
        else if (SlotsManager.Instance.isRewardGiveSlot2 && !all_RewardSlots[1].isRewardCollected)
        {
            all_RewardSlots[1].img_BG.color = Color.yellow;
            all_RewardSlots[1].txt_SlotName.text = "Open";
            all_RewardSlots[1].txt_SlotTimer.text = "0";
        }
        else
        {
            all_RewardSlots[1].EmptySlot();
            all_RewardSlots[1].img_BG.color = Color.black;
        }




        if (!SlotsManager.Instance.isThiredSlotEmpty && !SlotsManager.Instance.isStartTimerSlot3)
        {
            all_RewardSlots[2].SetSlotTime(SlotsManager.Instance.unlockTimeSlot3);
            all_RewardSlots[2].img_BG.color = Color.red;
            all_RewardSlots[2].ShowAllObjects();
        }
        else if (!SlotsManager.Instance.isThiredSlotEmpty && SlotsManager.Instance.isStartTimerSlot3)
        {
            all_RewardSlots[2].SetSlotTime(TimeManager.Instance.currentUnlockTimeSlot3);
            all_RewardSlots[2].img_BG.color = Color.green;
        }
        else if (SlotsManager.Instance.isRewardGiveSlot3 && !all_RewardSlots[2].isRewardCollected)
        {
            all_RewardSlots[2].img_BG.color = Color.yellow;
            all_RewardSlots[2].txt_SlotName.text = "Open";
            all_RewardSlots[2].txt_SlotTimer.text = "0";
        }
        else
        {
            all_RewardSlots[2].EmptySlot();
            all_RewardSlots[2].img_BG.color = Color.black;
        }




        if (!SlotsManager.Instance.isForthSlotEmpty && !SlotsManager.Instance.isStartTimerSlot4)
        {
            all_RewardSlots[3].SetSlotTime(SlotsManager.Instance.unlockTimeSlot4);
            all_RewardSlots[3].img_BG.color = Color.red;
            all_RewardSlots[3].ShowAllObjects();
        }
        else if (!SlotsManager.Instance.isForthSlotEmpty && SlotsManager.Instance.isStartTimerSlot4)
        {
            all_RewardSlots[3].SetSlotTime(TimeManager.Instance.currentUnlockTimeSlot4);
            all_RewardSlots[3].img_BG.color = Color.green;
        }
        else if (SlotsManager.Instance.isRewardGiveSlot4 && !all_RewardSlots[3].isRewardCollected)
        {
            all_RewardSlots[3].img_BG.color = Color.yellow;
            all_RewardSlots[3].txt_SlotName.text = "Open";
            all_RewardSlots[3].txt_SlotTimer.text = "0";
        }
        else
        {
            all_RewardSlots[3].EmptySlot();
            all_RewardSlots[3].img_BG.color = Color.black;
        }
    }


    public void OnClick_SlotStartTime(int index)
    {
        if(index == 0)
        {
            if (SlotsManager.Instance.isRewardGiveSlot1)
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE, 0);
                SlotsManager.Instance.CheckForRewardGiveSlot1();
                all_RewardSlots[0].EmptySlot();
                all_RewardSlots[0].img_BG.color = Color.black;
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME, 1);
                SlotsManager.Instance.CheckForStartTimeSlot1();
            }
        }
        else if (index == 1)
        {
            if (SlotsManager.Instance.isRewardGiveSlot2)
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 1, 0);
                SlotsManager.Instance.CheckForRewardGiveSlot2();
                all_RewardSlots[1].EmptySlot();
                Debug.Log("Empty SLot");
                all_RewardSlots[1].img_BG.color = Color.black;
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 1, 1);
                SlotsManager.Instance.CheckForStartTimeSlot2();
            }
        }
        else if (index == 2)
        {
            if (SlotsManager.Instance.isRewardGiveSlot3)
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 2, 0);
                SlotsManager.Instance.CheckForRewardGiveSlot3();
                all_RewardSlots[2].EmptySlot();
                all_RewardSlots[2].img_BG.color = Color.black;
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 2, 1);
                SlotsManager.Instance.CheckForStartTimeSlot3();
            }
        }
        else if (index == 3)
        {
            if (SlotsManager.Instance.isRewardGiveSlot4)
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_GIVE + 3, 0);
                SlotsManager.Instance.CheckForRewardGiveSlot4();
                all_RewardSlots[3].EmptySlot();
                all_RewardSlots[3].img_BG.color = Color.black;
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 3, 1);
                SlotsManager.Instance.CheckForStartTimeSlot4();
            }
        }

    }
}
