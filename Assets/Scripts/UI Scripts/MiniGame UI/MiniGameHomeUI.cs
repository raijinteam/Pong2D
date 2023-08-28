using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameHomeUI : MonoBehaviour
{
    public void OnClick_StartMinigame()
    {
        this.gameObject.SetActive(false);
        MiniGameManager.Instance.StartMiniGame();
        UIManager.instance.ui_Tutorial.gameObject.SetActive(false);
        //UIManager.instance.ui_MiniGamePlay.gameObject.SetActive(true);
    }
}
