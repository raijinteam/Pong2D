using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    NoCards,
    HasCards,
    EnoughCardsForUpgrade

}

public class PlayerData : MonoBehaviour
{

    public PlayerState playerState;

    [Space]
    public string Name;
    public Sprite image;
    public int currentLevel;
    public int currentCards;
    public int[] requireCardsToUnlock;
    public float[] moveSpeed;
    public float[] minBatForce;
    public float[] maxBatForce;
    public float[] minBallForce;
    public float[] maxBallForce;
    public float[] minSwingForce;
    public float[] maxSwingForce;
}
