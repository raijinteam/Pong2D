using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NavigationUI : MonoBehaviour
{

    [SerializeField] private GameObject[] all_MenuPanel; // ALL MENUS PANEL IN GAME
    [SerializeField] private RectTransform[] all_MenusBG; // ALL BUTTONS RECT TRANSFORM
    [SerializeField] private Image[] all_IconsBG; // all BG for highlight selected item
    [SerializeField] private RectTransform[] all_MenuIcons; // ALL MENUS ICONS
    [SerializeField] private RectTransform[] all_MenuNames; // all menus name

    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private float xPositionOffset = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        OnClick_MenuActivate(2);
    }

    public void OnClick_MenuActivate(int index)
    {

        float startingPosition = 0;


        for (int i = 0; i < all_MenusBG.Length; i++)
        {
            //I IS EQULS TO IDEX INCREASE SIZE OF BUTTON AND SET ACTIVE THAT BUTTON
            if (i == index)
            {

                all_MenusBG[i].DOAnchorMin(new Vector2(startingPosition, all_MenusBG[i].anchorMin.y), animationDuration);
                all_IconsBG[i].color = Color.gray;
                startingPosition += xPositionOffset;
                all_MenusBG[i].DOAnchorMax(new Vector2(startingPosition, 0.9f), animationDuration);
                all_MenuIcons[i].DOScale(1.3f, animationDuration);
                all_MenuIcons[i].DOMoveY(250f, animationDuration);
                all_MenuNames[i].DOMoveY(80, animationDuration);
                all_MenuPanel[i].SetActive(true);

            }
            //DECEREASE SIZE OF ALL OTHER BUTTONS
            else
            {
                all_MenusBG[i].DOAnchorMin(new Vector2(startingPosition, all_MenusBG[i].anchorMin.y), animationDuration);
                all_IconsBG[i].color = Color.white;
                startingPosition += xPositionOffset - 0.0375f;
                all_MenusBG[i].DOAnchorMax(new Vector2(startingPosition, .7f), animationDuration);
                all_MenuIcons[i].DOScale(1f, 0.001f);
                all_MenuIcons[i].DOMoveY(100f, 0.001f);
                all_MenuNames[i].DOMoveY(-200, animationDuration);
                all_MenuPanel[i].SetActive(false);
            }
        }

    }
}
