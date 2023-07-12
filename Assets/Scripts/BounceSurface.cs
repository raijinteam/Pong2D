using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSurface : MonoBehaviour
{

    [SerializeField] private float flt_MaxReflectionAngle = 45f;
    [SerializeField] private Vector3 dir;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
           /* Debug.Log("Get Ball");

            Vector2 relativeVelocity = collision.relativeVelocity;
            Vector2 collisionNormal = collision.contacts[0].normal;


            float incidentAngle = Mathf.Acos(Vector2.Dot(relativeVelocity, collisionNormal) / (relativeVelocity.magnitude * collisionNormal.magnitude));
            incidentAngle = incidentAngle * Mathf.Rad2Deg;

            if ( incidentAngle >= 0 && incidentAngle < 10)
            {
                //Transform direction = collision.gameObject.GetComponent<BallMovement>().dirction;
                //  dir = Vector3.Reflect(direction.right, collisionNormal) ;

                //direction.right = dir;


                bool isTouchByPlayer = collision.gameObject.GetComponent<BallMovement>().isTouchByPlayer;

                if (isTouchByPlayer)
                {
                    Debug.Log("Go Down Direction");
                    collision.transform.localRotation = Quaternion.Euler(0, 0, -flt_MaxReflectionAngle);
                }
                else
                {
                    Debug.Log("Go Up Direaction");
                    collision.transform.localRotation = Quaternion.Euler(0, 0, flt_MaxReflectionAngle);
                }
*/

                //ballbody.AddForce(direction.right * (direction.position.y * flt_MaxReflectionAngle)) ;

                /*if (collision.transform.position.y > 0)
                {
                Debug.Log("y is greather then 0");
                }
                else
                {
                Debug.Log("y is less then 0");
                ballbody.AddForce(new Vector2(ballbody.velocity.x, ballbody.velocity.y + flt_MaxReflectionAngle));
                }*/
            }
    
    }
}
