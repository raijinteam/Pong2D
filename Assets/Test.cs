using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float rotationSpeed = 90f; // Adjust the speed of rotation if needed
   [SerializeField] private float currentRotation = 25f;
    [SerializeField]float targetRotation = 360f;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            //transform.eulerAngles = new Vector3(0, 0, currentRotation);
            StartCoroutine(RotateObjectCoroutine());
           
           
           
        }
    }

    IEnumerator RotateObjectCoroutine() {

        bool isRoate360 = false;
        if (targetRotation < currentRotation) {
            isRoate360 = true;
            targetRotation += 180;
        }
       
            while (targetRotation > currentRotation) {
                float rotationAmount = rotationSpeed * Time.deltaTime;
                transform.Rotate(0f, 0, rotationAmount);
                currentRotation += rotationAmount;
                yield return null;
            }

        if (isRoate360) {
            currentRotation -= 180;
            targetRotation -= 180;
            transform.eulerAngles = new Vector3(0, 0, currentRotation);
        }
    }

    IEnumerator RotateObjectCoroutine2() {


        while (targetRotation < currentRotation) {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
            currentRotation -= rotationAmount;
            yield return null;
        }




    }
}
