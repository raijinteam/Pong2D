using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public PlayerScreenUI ui_PlayScren;
    public TimerScreenUI ui_TimerScreen;
    public GameOverUI ui_GameOver;


    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
        }
    }
}
