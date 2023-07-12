using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetPointForce : MonoBehaviour
{
    [SerializeField] private Transform tf_BatHitPoint;

    [Space]
    public float flt_MinHitForce;
    public float flt_MaxHitForce;

    [SerializeField] private float flt_MinHitPointDistacneOnBallTouch;
    [SerializeField] private float flt_MaxHitPointDistranceOnBallTouch;



    public float CalculateBatHitForce(Vector2 _traget)
    {
        //float distance = Mathf.Abs(_traget.x - tf_BatHitPoint.position.x);
        float distance = Vector2.Distance(_traget, tf_BatHitPoint.position);

       // Debug.Log("Hit point Distance" + distance); 

        float force = (distance - flt_MinHitPointDistacneOnBallTouch) * (flt_MinHitForce - flt_MaxHitForce) / (flt_MaxHitPointDistranceOnBallTouch - flt_MinHitPointDistacneOnBallTouch) + flt_MaxHitForce;

        force = Mathf.Clamp(force, flt_MinHitForce, flt_MaxHitForce);

       // Debug.Log("Sweet point distance : " + distance);

        return force;
    }
}
