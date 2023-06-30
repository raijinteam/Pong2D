using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    

    [SerializeField] private Rigidbody2D ball_Rb;

    [SerializeField] private Transform dirction;
    [SerializeField] private Vector3 dir;

    [Header("Ball Properites")]
    [SerializeField] private float flt_BallMoveSpeed;
    [SerializeField] private bool canAddRuns = false;
    [SerializeField] private bool startRunDelayTimer;
    [SerializeField] private float flt_RunDelayTimer = 2;
    [SerializeField] private float currentRunDelayTimer;
    [SerializeField] private float flt_BallMaxSpeed = 30f;

    [Header("Ball Swing Properties")]
    [SerializeField] private bool isSwing;
    [SerializeField] private float flt_SwingAngle;
    [SerializeField] private float fltSwingForce;


    private void Start() {
        SetAngle();
        ball_Rb.AddForce(-dirction.up * flt_BallMoveSpeed, ForceMode2D.Impulse);

    }


    private void FixedUpdate()
    {
        if (isSwing && flt_SwingAngle != 0)
        {

            float xDirection = Mathf.Sin(flt_SwingAngle);
            float yDirection = Mathf.Cos(flt_SwingAngle);

            Vector2 swingDirection = new Vector2(xDirection * fltSwingForce, yDirection);

            if(GetCurrentSpeed() < flt_BallMaxSpeed)
            {
                ball_Rb.AddForce((swingDirection) );
            }

           // Debug.Log("Ball speed: " + GetCurrentSpeed());

        }
    }

    public float GetCurrentSpeed()
    {
        return ball_Rb.velocity.magnitude;
    }


    private void SetAngle() {
        dirction.localEulerAngles = transform.localEulerAngles;
    }



    private void OnCollisionEnter2D(Collision2D collision) 
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            ball_Rb.velocity = Vector2.zero;
            isSwing = false;

            canAddRuns = true;

            SweetPointForce batForce = collision.gameObject.GetComponent<SweetPointForce>();
            
            float totalForce = batForce.CalculateBatHitForce(transform.position);

            ball_Rb.velocity = ball_Rb.velocity.normalized * flt_BallMoveSpeed * totalForce; // get batsman speed
        }

        else if (collision.gameObject.CompareTag("AI"))
        {
            ball_Rb.velocity = Vector2.zero;
            isSwing = true;

            AiPaddle aiForce = collision.gameObject.GetComponent<AiPaddle>();

            aiForce.CaclulatePaddleForceForBall(transform.position);

            float totalForce = aiForce.CaclulatePaddleForceForBall(transform.position);

             fltSwingForce = collision.gameObject.GetComponent<AiPaddle>().CalculateDistanceFromSwingPoints(transform.position);

            // ball_Rb.velocity = ball_Rb.velocity.normalized * flt_BallMoveSpeed /** totalForce*/;  // get bowler speed
            Debug.Log("Directioon : " + dirction.position.normalized);

            ball_Rb.AddForce(collision.relativeVelocity.normalized * flt_BallMoveSpeed);
        }

    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAddRuns)
        {
            if (collision.CompareTag("Boundries") &&  !startRunDelayTimer)
            {
                startRunDelayTimer = true;
                int _score = collision.gameObject.GetComponent<Runs>().runs;
                ScoreManager.instance.AddScore(_score);
            }
        }

        if (collision.CompareTag("Wicket"))
        {
            ScoreManager.instance.AddWicket();
        }
    }
}
