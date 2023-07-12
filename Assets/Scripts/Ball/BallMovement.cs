using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D ball_Rb;

    public Transform dirction;
    [SerializeField] private Vector3 dir;

    [Header("Ball Properites")]
    [SerializeField] private float flt_BallMoveSpeed;
    [SerializeField] private bool canAddRuns = false;
    [SerializeField] private bool startRunDelayTimer;
    [SerializeField] private float flt_RunDelayTimer = 2;
    [SerializeField] private float currentRunDelayTimer;
    [SerializeField] private float flt_BallMaxSpeed = 30f;
    [SerializeField] private float flt_MaxWallReflectionAngle = 70f;

    private bool isTouchByPlayer;

    [Header("Ball Swing Properties")]
    [SerializeField] private bool isSwing;
    [SerializeField] private float flt_SwingAngle;
    [SerializeField] private float fltSwingForce;

    public float swingDir;


    private void OnEnable()
    {
        transform.position = Vector3.zero;
        transform.root.localEulerAngles = new Vector3(0, 0, Random.Range(-90, 90));
        if(transform.rotation.eulerAngles.z == 90 || transform.rotation.eulerAngles.z == -90)
        {
            transform.root.localEulerAngles = new Vector3(0, 0, Random.Range(-90, 90));
        }
        SetAngle();
        ball_Rb.AddForce(dirction.right * flt_BallMoveSpeed, ForceMode2D.Impulse);
        GameManager.instance.aiPaddle.isHitBall = false;

    }

    private void Update()
    {
        if (startRunDelayTimer)
        {
            currentRunDelayTimer += Time.deltaTime;
            if(currentRunDelayTimer >= flt_RunDelayTimer)
            {
                startRunDelayTimer = false;
                currentRunDelayTimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {

        if(ball_Rb.velocity.y < 1 && ball_Rb.velocity.y > -1)
        {
          //  Debug.Log("ball velocity is low : " + ball_Rb.velocity.y);
            //ball_Rb.AddForce(new Vector2( ball_Rb.velocity.x, ball_Rb.velocity.y * flt_MaxWallReflectionAngle));
            ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y * flt_MaxWallReflectionAngle);
        }

        if (isSwing)
        {
            float xDirection = Mathf.Sin(flt_SwingAngle);
            float yDirection = Mathf.Cos(flt_SwingAngle);

            Vector2 swingDirection = new Vector2(xDirection * fltSwingForce, transform.position.y);


            swingDir = ball_Rb.velocity.x + fltSwingForce * Time.deltaTime;
            ball_Rb.velocity = new Vector2(swingDir, ball_Rb.velocity.y);
        }
    }

    public float GetCurrentSpeed()
    {
        return ball_Rb.velocity.magnitude;
    }


    private void SetAngle()
    {
        dirction.localEulerAngles = transform.localEulerAngles;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        dir = Vector3.Reflect(dirction.right, collision.contacts[0].normal);

        dirction.right = dir;

        if (collision.gameObject.CompareTag("UpWall"))
        {
              isSwing = false;
            ball_Rb.velocity = Vector2.zero;
            ball_Rb.angularVelocity = 0;


            ball_Rb.AddForce(dirction.right * flt_BallMoveSpeed , ForceMode2D.Impulse); // get batsman speed
        }

        if (collision.gameObject.CompareTag("BottomWall"))
        {
            GameManager.instance.aiPaddle.isHitBall = false;
            ball_Rb.velocity = Vector2.zero;
            ball_Rb.angularVelocity = 0;


            ball_Rb.AddForce(dirction.right * flt_BallMoveSpeed, ForceMode2D.Impulse); // get batsman speed
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            ball_Rb.velocity = Vector2.zero;
            ball_Rb.angularVelocity = 0;

            isTouchByPlayer = true;

            PlayerPaddleMovement player = collision.gameObject.GetComponent<PlayerPaddleMovement>();
            isSwing = false;
            canAddRuns = true;
            if (!player.isBatting)
            {
                isSwing = true;
                canAddRuns = false;
            }



            float batForce = player.CaclulatePaddleForceForBall(transform.position);


            GameManager.instance.aiPaddle.isHitBall = false;

            fltSwingForce = player.CalculateDistanceFromSwingPoints(transform.position);


            ball_Rb.AddForce(dirction.right * flt_BallMoveSpeed * batForce, ForceMode2D.Impulse); // get batsman speed
        }

        if (collision.gameObject.CompareTag("AI"))
        {

            canAddRuns = false;
            ball_Rb.velocity = Vector2.zero;
            ball_Rb.angularVelocity = 0;

            isTouchByPlayer = false;

            isSwing = true;

            AiPaddle aiPaddle = collision.gameObject.GetComponent<AiPaddle>();
            if (aiPaddle.isBatting)
            {
                isSwing = false;
                canAddRuns = true;
            }

            aiPaddle.CaclulatePaddleForceForBall(transform.position);

            float totalForce = aiPaddle.CaclulatePaddleForceForBall(transform.position);

            fltSwingForce = collision.gameObject.GetComponent<AiPaddle>().CalculateDistanceFromSwingPoints(transform.position);

            ball_Rb.AddForce(dirction.right * flt_BallMoveSpeed * totalForce, ForceMode2D.Impulse);
            aiPaddle.SetRandomXOffset();

        }
       
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAddRuns)
        {
            if (collision.CompareTag("Boundries") && !startRunDelayTimer)
            {
                startRunDelayTimer = true;
                int _score = collision.gameObject.GetComponent<Runs>().runs;
                GameManager.instance.IncreaseScore(_score);
               // ScoreManager.instance.AddScore(_score);
            }
        }

        if (collision.CompareTag("Wicket"))
        {
            GameManager.instance.IncreaseWicket();
            GameManager.instance.SpawnNewBall();
            //ScoreManager.instance.AddWicket();
        }
    }
}
