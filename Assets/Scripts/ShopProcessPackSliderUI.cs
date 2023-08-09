using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopProcessPackSliderUI : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float scrollPos;
    public RectTransform[] all_Images;
    public float[] all_Pos;
    public float snapSpeed = 0.2f;



    private void Update()
    {
        all_Pos = new float[all_Images.Length];
        float distance = 1f / (all_Images.Length - 1f);
        for (int i = 0; i < all_Pos.Length; i++)
        {
            all_Pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.value;
        }
        else
        {
            for (int i = 0; i < all_Pos.Length; i++)
            {
                if (scrollPos < all_Pos[i] + (distance / 2) && scrollPos > all_Pos[i] - (distance / 2))
                {
                    scrollbar.value = Mathf.Lerp(scrollbar.value, all_Pos[i], snapSpeed * Time.deltaTime);
                }
            }
        }
    }
}
