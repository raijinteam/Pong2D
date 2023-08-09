using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessPackSlider : MonoBehaviour
{
    [SerializeField] private int index;

    [SerializeField] private int startPosition;
    [SerializeField] private float[] allPositions;
    [SerializeField] private int offset;
    [SerializeField] private RectTransform parent;

    [SerializeField] private float flt_LerpDuratiuon;

    // Start is called before the first frame update
    void Start()
    {
        parent.anchoredPosition = new Vector3(startPosition, parent.anchoredPosition.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.position.x <= allPositions[1])
            index = 1;
        else if (parent.position.x <= allPositions[2])
            index = 2;
        else if (parent.position.x <= allPositions[3])
            index = 3;
        else
            index = 4;

        if (Input.GetKey(KeyCode.RightArrow))
            SlideAnimationRight();
        else if (Input.GetKey(KeyCode.LeftArrow))
            SlideAnimationLeft();
    }


    private void SlideAnimation(int moveAmount)
    {
        float newPosition = parent.anchoredPosition.x - moveAmount;
        Vector2 tempPos = parent.anchoredPosition;




        tempPos.x = Mathf.Lerp(tempPos.x, newPosition, Time.deltaTime * flt_LerpDuratiuon);

        parent.anchoredPosition = tempPos;
    }

    private void SlideAnimationRight()
    {
        float newPosition = parent.anchoredPosition.x - offset;

        Vector2 tempPos = parent.anchoredPosition;

        tempPos.x = Mathf.Lerp(tempPos.x, newPosition, Time.deltaTime * flt_LerpDuratiuon);

        parent.anchoredPosition = tempPos;       
    }

    private void SlideAnimationLeft()
    {
        float newPosition = parent.anchoredPosition.x + offset;

        Vector2 tempPos = parent.anchoredPosition;

        tempPos.x = Mathf.Lerp(tempPos.x, newPosition, Time.deltaTime * flt_LerpDuratiuon);

        parent.anchoredPosition = tempPos;
    }
}
