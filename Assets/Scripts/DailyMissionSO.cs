using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Archivements" , fileName ="new archivement")]
public class DailyMissionSO : ScriptableObject
{
    public RewardType rewardType;
    public Sprite img_RewardIcon;
    public string str_Description;
    public int rewardAmount;
    public int missionCompeleteAmount;
}
