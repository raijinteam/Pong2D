using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSurface : MonoBehaviour
{

    [SerializeField] private float flt_MaxReflectionAngle = 45f; 


    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Ball"))
            {
                Vector2 relativeVelocity = collision.relativeVelocity;
                Vector2 collisionNormal = collision.contacts[0].normal;

                float incidentAngle = Mathf.Acos(Vector2.Dot(relativeVelocity, collisionNormal) / (relativeVelocity.magnitude * collisionNormal.magnitude));
                incidentAngle = incidentAngle * Mathf.Rad2Deg;

                if (Mathf.Approximately(incidentAngle, 0) || incidentAngle < 15f)
                {
                    Rigidbody2D ballbody = collision.gameObject.GetComponent<Rigidbody2D>();

                    if (collision.transform.position.y > 0.1f)
                    {
                        ballbody.AddForce(new Vector2(ballbody.velocity.x, ballbody.velocity.y - flt_MaxReflectionAngle));
                    }
                    else if (collision.transform.position.y < -0.1f)
                    {
                        ballbody.AddForce(new Vector2(ballbody.velocity.x, ballbody.velocity.y + flt_MaxReflectionAngle));
                    }
                    else if (ballbody.velocity.y == 0)
                    {
                        ballbody.AddForce( new Vector2( ballbody.velocity.x + flt_MaxReflectionAngle, ballbody.velocity.y + flt_MaxReflectionAngle));
                    }
                }
            }
        
    }
}
