using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Archivements" , fileName ="new archivement")]
public class AchievementSO : ScriptableObject
{
    public Sprite img_RewardIcon;
    public string str_RewardName;
    public string str_Description;
    public int rewardAmount;
    public int currentLevel;
    public int[] missionCompeleteAmount;
}
