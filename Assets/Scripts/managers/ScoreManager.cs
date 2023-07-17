using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;


    [SerializeField] private int score;
    [SerializeField] private int wicket;

    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UIManager.instance.ui_PlayScreen.txt_Score.text = score.ToString();
        UIManager.instance.ui_PlayScreen.txt_Wickets.text = wicket.ToString();
    }

    public void AddScore(int _score)
    {
        score += _score;
        UIManager.instance.ui_PlayScreen.txt_Score.text = score.ToString();
    }

    public void AddWicket()
    {
        wicket += 1;
        UIManager.instance.ui_PlayScreen.txt_Wickets.text = wicket.ToString();
    }
}
