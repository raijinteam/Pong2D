using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform dircetion;
   // [SerializeField] private Vector3 direction;

    private void FixedUpdate() {

        transform.Translate(dircetion.right * 10 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {

        dircetion.right = -dircetion.right;
    }



    //[SerializeField] private float targetRotation;
    //[SerializeField] private float flt_StartRotation;
    //[SerializeField] private float flt_RotationSpeed;
    //[SerializeField] private bool IsRotate360;

    //[SerializeField] private float rotationAmount;
    //[SerializeField] private float moduleValue360;




    //private void Update() {


    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        transform.localEulerAngles = new Vector3(0, 0, flt_StartRotation);
    //        StartCoroutine(RotatePaddle());
    //    }
    //    if (Input.GetKeyDown(KeyCode.Backspace)) {
    //        transform.localEulerAngles = new Vector3(0, 0, flt_StartRotation);
    //        StartCoroutine(ResetPaddle());

    //    }
    //}

    //private IEnumerator ResetPaddle() {

    //    moduleValue360 = flt_StartRotation % 360;
    //    transform.localEulerAngles = new Vector3(0, 0, moduleValue360);

    //    if (moduleValue360 > 180) {

    //        int Value = ((int)(flt_StartRotation / 360));
    //        targetRotation = flt_StartRotation + (360 * (Value + 1) - flt_StartRotation);

    //    }
    //    else {

    //        int Value = ((int)(flt_StartRotation / 180));
    //        targetRotation = flt_StartRotation + (180 * (Value + 1) - flt_StartRotation);
    //    }


    //    Quaternion target = Quaternion.Euler(0, 0, targetRotation);

    //    while (transform.rotation != target) {
    //        float step = flt_RotationSpeed * Time.deltaTime;
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);
    //        yield return null;
    //    }
    //    flt_StartRotation = targetRotation;


    //}

    //private IEnumerator RotatePaddle() {

    //    targetRotation += Random.Range(20,40);

    //    Quaternion target = Quaternion.Euler(0,0,targetRotation);

    //    while (transform.rotation != target) {
    //        float step = flt_RotationSpeed * Time.deltaTime;
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);
    //        yield return null;
    //    }
    //    flt_StartRotation = targetRotation;
    //}
}
