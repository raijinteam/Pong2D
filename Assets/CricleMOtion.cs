using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricleMOtion : MonoBehaviour
{
    [SerializeField] private bool invertMove;

    [SerializeField] private float timeCounter;
    [SerializeField] private float flt_Angle;
    [SerializeField] private float direction;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invertMove)
        {
            timeCounter += direction * Time.deltaTime; // multiply all this with some speed variable (* speed);
            float x = Mathf.Cos(timeCounter);
            float y = Mathf.Sin(timeCounter);
            float z = 0;
            transform.position = new Vector3(x, y, z) * flt_Angle;
        }
        else
        {
            timeCounter -= direction * Time.deltaTime; // multiply all this with some speed variable (* speed);
            float x = Mathf.Cos(timeCounter);
            float y = Mathf.Sin(timeCounter);
            float z = 0;
            transform.position = new Vector3(-x, -y, z) * flt_Angle;
        }

        
    }


}
