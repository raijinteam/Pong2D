using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSpin : MonoBehaviour
{

    [SerializeField] private float flt_SpinSpeed;
    [SerializeField] private float flt_SpinTimer;

    [SerializeField] private float flt_SpinTime;


    [SerializeField] private RectTransform[] all_Images;

    private float currentSpinTImer;
    private bool isSpnning = true;





    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpin());
    }


    private void Update()
    {
    }



    IEnumerator StartSpin()
    {

        int index = 0;

        float spinTime = 0;

        
        //Debug.Log(" first image position : " + all_Images[2].transform.position.y);
        while ( spinTime < 1)
        {
            if(index >= all_Images.Length - 1)
            {
                index = 0;
            }
            if(all_Images[index].transform.position.y >= 800)
            {
                all_Images[index].transform.position = new Vector2(all_Images[index].transform.position.x, 200f);
            }
            //Debug.Log(all_Images[4].transform.position.y);
            spinTime += Time.deltaTime / flt_SpinTime;
            all_Images[index].transform.position = new Vector2(all_Images[index].transform.position.x, all_Images[index].transform.position.y + 25);
            index++;
            
            yield return null;
        }
       // Debug.Log(index);

        all_Images[index].transform.position = new Vector2(all_Images[index].transform.position.x, this.transform.position.y);
    }



    /*IEnumerator StartSlot()
    {
        for (int i = 0; i < flt_SpinTimer; i++)
        {
            Debug.Log("Trsform : " + transform.position.y);
            if (transform.position.y >= 1000)
            {
                transform.position = new Vector2(transform.position.x, 350);
            }

            transform.position = new Vector2(transform.position.x, transform.position.y + 50);



            yield return new WaitForSeconds(flt_SpinSpeed);
        }
    }*/
}
