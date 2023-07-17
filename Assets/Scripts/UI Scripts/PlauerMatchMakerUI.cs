using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlauerMatchMakerUI : MonoBehaviour
{

    [SerializeField] private float flt_DelayTime;
    [SerializeField] private TextMeshProUGUI txt_Waitting;

    private float currentDelayTime;

    private void Start()
    {
        currentDelayTime = flt_DelayTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentDelayTime -= Time.deltaTime;
        txt_Waitting.text = "Match Start in " + (int)currentDelayTime + "..";
        if(currentDelayTime <= 0)
        {
            this.gameObject.SetActive(false);
            GameManager.instance.StartGame();
            currentDelayTime = flt_DelayTime;
        }
    }
}
