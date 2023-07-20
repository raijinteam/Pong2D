using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyRewardUI : MonoBehaviour
{
    [Header("Daily Reward Data")]
    [SerializeField] private DailyRewardButtonUI[] allDaysRewardButtons;
    [SerializeField] private TextMeshProUGUI txt_Timer;

    private int index = 0;

    private List<DailyRewardButtonUI> dailyRewardButtons = new List<DailyRewardButtonUI>();

    private List<Sprite> sprites = new List<Sprite>();
    private List<string> strings = new List<string>();

    private void OnEnable()
    {
        sprites.Clear();
        strings.Clear();

    }

    // Start is called before the first frame update
    void Start()
    {

        index = PlayerPrefs.GetInt(PlayerPrefsKeys.KEY_DAYS_COUNT);

        for (int i = 0; i < allDaysRewardButtons.Length; i++)
        {
            allDaysRewardButtons[i].GetComponent<Button>().interactable = false;
            allDaysRewardButtons[i].img_ClimedReward.gameObject.SetActive(false);
        }

        //Set Clamied Reward
        for(int i = 0; i < index; i++)
        {
            allDaysRewardButtons[i].img_ClimedReward.gameObject.SetActive(true);
        }

       // allDaysRewardButtons[index].GetComponent<Button>().interactable = true;

    }


    private void Update()
    {
        if(DataManager.Instance.GetDailyRewardActiveTimeForBool() == 1)
        {
            int timeInSeconds = (int)TimeManager.Instance.currentTime;

            float hours = timeInSeconds / 3600;
            float minutes = (timeInSeconds % 3600) / 60;
            float seconds = timeInSeconds % 60;


            txt_Timer.text = $"{(int)hours} : {(int)minutes} : {(int)seconds}";

        }
        else
        {
            if (DataManager.Instance.GetDailyRewardActiveTimeForBool() == 0)
            {
                allDaysRewardButtons[index].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void OnClick_ActiveNextRewardButton(int _buttonIndex)
    {

        if (_buttonIndex == index && index <= allDaysRewardButtons.Length - 1)
        {
            DataManager.Instance.SetDailyRewardActiveTime(1);
            GameManager.instance.CheckForDailyRewardTImeIsStart();
            Debug.Log("Set Reward Time");
            allDaysRewardButtons[index].GetComponent<Button>().interactable = false;
            allDaysRewardButtons[index].img_ClimedReward.gameObject.SetActive(true);


            if (index == allDaysRewardButtons.Length - 1)
            {
                int childCount = allDaysRewardButtons[index].transform.GetChild(0).childCount;

                for (int i = 0; i < childCount; i++)
                {
                    sprites.Add(allDaysRewardButtons[index].transform.GetChild(0).transform.GetChild(i).GetComponent<Image>().sprite);

                    strings.Add(allDaysRewardButtons[index].transform.GetChild(1).transform.GetChild(i).GetComponent<TextMeshProUGUI>().text);
                }


                UIManager.instance.ui_RewardSummary.SetMultiplRewardSummaryData(sprites, strings);
                UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
            }
            else
            {
                if (index >= allDaysRewardButtons.Length - 1)
                {
                    index = 0;
                }

                UIManager.instance.ui_RewardSummary.SetRewardSummaryData(allDaysRewardButtons[index].img_Icon.sprite, allDaysRewardButtons[index].txt_RewardAmount.text);
                UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);


                index++;
                PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_DAYS_COUNT, index);
                //allDaysRewardButtons[index].GetComponent<Button>().interactable = true;
            }


            //print(index);
        }
    }


    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
    }
}
