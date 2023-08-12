using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotEnoughCurrencyPopUpUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Message;
    [SerializeField] private float flt_Duratiuon = 1f;
    private Transform reachPoint;



    private void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    public void SetMessage(string Massage)
    {
        txt_Message.text = Massage;
    }

    public IEnumerator StartAnimation()
    {
        transform.DOScale(1, flt_Duratiuon / 2);
        yield return new WaitForSeconds(flt_Duratiuon);
        transform.DOScale(0, flt_Duratiuon / 2);
        yield return new WaitForSeconds(flt_Duratiuon / 2);
        this.gameObject.SetActive(false);
    }
}
