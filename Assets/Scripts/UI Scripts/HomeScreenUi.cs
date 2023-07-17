using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenUi : MonoBehaviour
{




    private void OnEnable()
    {
    

    }

    public void OnClick_StartGame()
    {
        this.gameObject.SetActive(false);
        UIManager.instance.ui_HomePanel.ui_Level.gameObject.SetActive(true);
    }
}
