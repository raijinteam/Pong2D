using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleContrallor : MonoBehaviour
{

    public bool isPlayer;
    public bool isBatting;


    [Header("Paddle Require Components")]
    public Transform tf_HitPoint;
    public Transform tf_LeftSwingPoint;
    public Transform tf_RightSwingPoint;
    public SweetPointForce sweetForce;

    [Header("Paddle Contrallor Properites")]
    public float flt_MoveSpeed = 5;
    public float flt_RotationSpeed = 5;

    [Header("Swing Properties")]
    public float flt_SwingForce;

}
