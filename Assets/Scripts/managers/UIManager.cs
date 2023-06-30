using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public PlayerScreenUI ui_PlayScren;


    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
        }
    }
}
