using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    [SerializeField] private Transform ball;
    public AiPaddle aiPaddle;

    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
    }

    public Transform GetBall
    {
        get
        {
            return ball;
        }
    }
}
