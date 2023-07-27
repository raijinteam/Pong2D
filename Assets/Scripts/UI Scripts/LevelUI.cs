using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{

    public bool isLevelUnlocked;


    [Header("Locked Properites")]
    public Image img_LockBG;
    public Image img_LockContainer;
    public TextMeshProUGUI txt_UnlockTrophiesCount;

    [Header("Unlock Properites")]
    public Image img_LevelIcon;
    public TextMeshProUGUI txt_LevelName;
    public Image img_TrophiesContainer;
    public TextMeshProUGUI txt_WinningTorphies;
    public Slider slider_Trophies;
    public TextMeshProUGUI txt_CurrentTrophies;
    public TextMeshProUGUI txt_RequireTrophiesForIncreaseLevel;
    public TextMeshProUGUI txt_LossingTrophies;
    public TextMeshProUGUI txt_WinningPrice;
    public TextMeshProUGUI txt_EntryFees;
}
