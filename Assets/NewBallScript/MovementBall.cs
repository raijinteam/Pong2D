using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBall : MonoBehaviour
{

    [SerializeField] private Rigidbody2D ball_Rb;

    public float flt_MoveSpeed;
    public Transform direction;
    public Vector3 moveDirection;
    [SerializeField] private float flt_MaxSpeed = 30f;


    [SerializeField] private float rigidBodyForce = 2;

    private float playerHitForce;
    private float aiHitForce;

    [SerializeField] private bool isSwinging = false;
    public float swingForce;

    [SerializeField] private bool startRunDelayTimer;
    [SerializeField] private bool canAddRuns;
    [SerializeField] private float delayTimer;
    private float currentDelayTimer;
    private string Boundries = "Boundries";
    private string wicket = "Wicket";


    private bool isLeftSideTriggerTutorial;
    private bool isRightSideTriggerTutorial;
    private float currentTriggerTimerTutorial = 0;


    private void Start()
    {
        currentDelayTimer = delayTimer;

        moveDirection = Vector3.down;


        ball_Rb.velocity = Vector3.down * flt_MoveSpeed ;
    }


    private void Update()
    {
        if (startRunDelayTimer)
        {
            currentDelayTimer -= Time.deltaTime;
            if(currentDelayTimer<= 0)
            {
                startRunDelayTimer = false;
                currentDelayTimer = delayTimer;
            }
        }


        if (isLeftSideTriggerTutorial)
        {
            currentTriggerTimerTutorial += Time.deltaTime;
            if(currentTriggerTimerTutorial >= 1)
            {
                currentTriggerTimerTutorial = 0;
                UIManager.instance.ui_Tutorial.toutorialState = TutorialState.BallRightSwingMSG;
                Time.timeScale = 0;
                isLeftSideTriggerTutorial = false;
            }
        }

        if (isRightSideTriggerTutorial)
        {
            currentTriggerTimerTutorial += Time.deltaTime;
            if(currentTriggerTimerTutorial >= 1)
            {
                currentTriggerTimerTutorial = 0;
                UIManager.instance.ui_Tutorial.toutorialState = TutorialState.BasicComplete;
                Time.timeScale = 0;
                isRightSideTriggerTutorial = false;
            }
        }
    }

    private void FixedUpdate()
    {

        if (isSwinging)
        {
            ball_Rb.velocity = new Vector2(swingForce + ball_Rb.velocity.x, ball_Rb.velocity.y);
        }

        if(GetCurrentSpeed() >= flt_MaxSpeed)
        {
            
        }

    }


    public float GetCurrentSpeed()
    {
        return ball_Rb.velocity.magnitude;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") )
        {

            if (collision.gameObject.GetComponent<Player>().playerMovement.isBatting)
            {
                isSwinging = false;
                canAddRuns = true;
            }
            else
            {
                isSwinging = true; 
                canAddRuns = false;
            }

            playerHitForce = collision.gameObject.GetComponent<SweetPointForce>().CalculateBatHitForce(transform.position);

            swingForce = collision.gameObject.GetComponent<PlayerPaddleMovement>().CalculateDistanceFromSwingPoints(transform.position);


            ball_Rb.velocity = ball_Rb.velocity.normalized * 10;

        }
        else if (collision.gameObject.tag.Equals("UpWall"))
        {
            isSwinging = false;
            Debug.Log("Collide with wall");
            ball_Rb.velocity = ball_Rb.velocity.normalized * 10;

           
        }
        else if (collision.gameObject.tag.Equals("AI"))
        {
            isSwinging = true;
            aiHitForce = collision.gameObject.GetComponent<AiPaddle>().CaclulatePaddleForceForBall(transform.position);

            canAddRuns = false;
            if (collision.gameObject.TryGetComponent(out AiPaddle aiPaddle))
            {
                aiPaddle.isHitBall = false;
                if(aiPaddle.isBatting)
                {
                    canAddRuns = true;
                }
            }

            ball_Rb.velocity = ball_Rb.velocity.normalized * 10;

            swingForce = collision.gameObject.GetComponent<AiPaddle>().CalculateDistanceFromSwingPoints(transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAddRuns)
        {
            if (collision.CompareTag(Boundries) && !startRunDelayTimer)
            {
                startRunDelayTimer = true;
                int _score = collision.gameObject.GetComponent<Runs>().runs;
                GameManager.instance.IncreaseScore(_score);
                Debug.Log("Runs Add : " + _score);
                // ScoreManager.instance.AddScore(_score);
            }
        }


        var notStartBatTutorial = GameManager.instance.isBatTutorialGameStart == false;
        var notStateBallTutorial = GameManager.instance.isBallTutorialStart == false;
        var isNotMinigameActive = MiniGameManager.Instance.isMiniGameStart == false;


        //if not start bat tutorial
        //if not start ball tutorial
        //if not active minigame
        //then add wicket otherwise not 

        if (collision.CompareTag(wicket) && notStartBatTutorial && notStateBallTutorial && isNotMinigameActive)
        {
            Debug.Log("Wickets Down");
            GameManager.instance.IncreaseWicket();
            GameManager.instance.SpawnNewBall();
            //ScoreManager.instance.AddWicket();
        }



        //Check if tutorial is active
        if (DataManager.Instance.isGameFirstTimeLoad)
        {

            var isBowlingLeftSideSwingTutorial = UIManager.instance.ui_Tutorial.toutorialState == TutorialState.BallLeftSwingPoint;
            var isBowlingrightSideSwingTutorial = UIManager.instance.ui_Tutorial.toutorialState == TutorialState.BallRightSwingPoint;

            if (collision.gameObject.CompareTag("LeftSwingPoint") && isBowlingLeftSideSwingTutorial)
            {
                //Go right side
                isLeftSideTriggerTutorial = true;
                Debug.Log("Left swing point trigger");
            }

            if(collision.gameObject.CompareTag("RightSwingPoint") && isBowlingrightSideSwingTutorial)
            {
                isRightSideTriggerTutorial = true;
                Debug.Log("Right swing point trigger");
            }

        }
    }

}
