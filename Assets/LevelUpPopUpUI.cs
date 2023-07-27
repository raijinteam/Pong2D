using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelUpPopUpUI : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }


    private IEnumerator StartAnimation()
    {
        this.transform.DOScale(1, 0.5f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(2f);
        this.transform.DOScale(0, 0.5f).SetEase(Ease.OutSine);
    }
}
