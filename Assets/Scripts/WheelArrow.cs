using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelArrow : MonoBehaviour
{
    [SerializeField] private FortuneWheelSpinUI fortuneWheel;
    [SerializeField] private RectTransform rect;


    [SerializeField] private Vector3 position;
    private void Start()
    {
        position = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (fortuneWheel.isLastSpin)
        {
            Debug.Log("is Last Spin");
            rect.DORotate(new Vector3(0, 0, -30), 0.5f);
        }
        else
            rect.DORotate(new Vector3(0, 0, -60), 0.5f);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rect.DORotate(new Vector3(0, 0, 0), 1f);
    }
    
}
