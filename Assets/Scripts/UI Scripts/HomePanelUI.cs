using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanelUI : MonoBehaviour
{
    public HomeScreenUi ui_HomeScreen;
    public AllLevelUI ui_Level;


    private void OnEnable()
    {
        ui_Level.gameObject.SetActive(false);
        ui_HomeScreen.gameObject.SetActive(true);
    }

    public void onclickPanel1()
    {
        Debug.Log("Clikck  Panel 1");
    }
}
