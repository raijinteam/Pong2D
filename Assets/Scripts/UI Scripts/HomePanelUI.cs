using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanelUI : MonoBehaviour
{
    public HomeScreenUi ui_HomeScreen;
    public LevelUI ui_Level;


    private void OnEnable()
    {
        ui_Level.gameObject.SetActive(false);
        ui_HomeScreen.gameObject.SetActive(true);
    }
}
