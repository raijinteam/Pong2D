using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePaddle : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 50f;

    [SerializeField] private float minLeftValue = 40f;


    [SerializeField] private bool isRotation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Roatae());
    }

    private IEnumerator Roatae()
    {
        Quaternion start = transform.rotation;
        Quaternion taregtAngle = Quaternion.Euler(0, 0, minLeftValue);

        float flt_CurrentTime = 0;

        while (flt_CurrentTime < 1)
        {
            flt_CurrentTime += Time.deltaTime/1;

            transform.localRotation = Quaternion.Slerp(start, taregtAngle, flt_CurrentTime);
            yield return null;
        }
        transform.localRotation = taregtAngle;
    }

    //// Update is called once per frame
    //void Update()
    //{


    //    if (isRotation)
    //    {
    //        if (transform.localEulerAngles.z < minLeftValue)
    //        {
    //            Debug.Log(transform.localEulerAngles.z);
    //            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    //        }
    //    }


    //}


    //IEnumerator RotatePlayer()
    //{
    //    if(transform.localEulerAngles.z < minLeftValue)
    //    {
    //        transform.eulerAngles = new Vector3(0, 0, (rotationSpeed % 360)* Time.deltaTime);
    //    }

    //    yield return null;
    //}
}
