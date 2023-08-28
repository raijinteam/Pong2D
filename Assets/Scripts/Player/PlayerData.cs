using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    NoCards,
    HasCards,
    EnoughCardsForUpgrade

}

public enum PlayerType
{
    Common,
    Rare,
    Epic
}

public class PlayerData : MonoBehaviour
{
    public PlayerType playerType;
    public PlayerState playerState;

    [Space]
    public string Name;
    public Sprite image;
    public Sprite paddleSprite;
    public int currentLevel;
    public int currentCards;
    public int[] levelIncreaseAmount;
    public int[] requireCardsToUnlock;

    public float[] minBatForce;
    public float[] maxBatForce;
    public float[] minBallForce;
    public float[] maxBallForce;
    public float[] minSwingForce;
    public float[] maxSwingForce;

    public float[] ultimateCount;
}
