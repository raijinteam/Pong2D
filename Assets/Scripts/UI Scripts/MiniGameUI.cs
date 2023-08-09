using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameUI : MonoBehaviour
{
    

    public void OnClick_StartMinigame()
    {
        MiniGameManager.Instance.StartMiniGame();
        UIManager.instance.ui_MiniGamePlay.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
