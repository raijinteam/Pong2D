using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AiPaddleDifficulty
{
    Easy,
    Medium,
    Hard
}

public class AiPaddle : MonoBehaviour
{
    [Header("Onlu for Dubbing")]
    public bool isDownAi;

    public float addBallForce;

    [Header("For Dubbiung")]
    [SerializeField] private float xPositionOffset = 0.5f;


    [Header("Require Components")]
    [SerializeField] private Transform tf_BallPositon;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform tf_BatHitPoint;
    public Transform tf_LeftSwingPoint;
    public Transform tf_RightSwingPoint;

    [Header("Paddle Properties")]
    [SerializeField] private float flt_MaxHitForce;
    [SerializeField] private float flt_MinHitForce;

    [Header("AI Difficulty Propertys")]
    [SerializeField] private float flt_MoveSpeed;
    [SerializeField] private AiPaddleDifficulty aiDifficulty;
    [SerializeField] private float flt_LowMoveSpeed;
    [SerializeField] private float flt_MediumMoveSpeed;
    [SerializeField] private float flt_HighMoveSpeed;
    [SerializeField] private float flt_LowFollowDistance = 3f;
    [SerializeField] private float flt_MediumFollowDistance = 5f;
    [SerializeField] private float flt_HighFollowDistance = 7f;
    [SerializeField] private float flt_ClampOffset = 1.8f;

    [Header("Ball Swing Proprties")]
    [SerializeField] private float flt_MinSwingAngle;
    [SerializeField] private float flt_MaxSwingAngle;
    [SerializeField] private float flt_Swing_Force;

    private float xMovement = 0;
    private void Start()
    {
        float screenHalfWidthInWroldUnits = Camera.main.aspect * Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
          AiMovement();
    }


    private void AiMovement()
    {
        Vector3 ballPosition = GameManager.instance.GetBall.transform.position;
        xMovement = 0;
        float followDistanceBaseOnDifficulty = 0;
        float speedBaseOnDifficulty = 0;

        if (aiDifficulty == AiPaddleDifficulty.Easy)
        {
            speedBaseOnDifficulty = flt_LowMoveSpeed;
            followDistanceBaseOnDifficulty = flt_LowFollowDistance;
        }
        else if (aiDifficulty == AiPaddleDifficulty.Medium)
        {
            speedBaseOnDifficulty = flt_MediumMoveSpeed;
            followDistanceBaseOnDifficulty = flt_MediumFollowDistance;
        }
        else if (aiDifficulty == AiPaddleDifficulty.Hard)
        {
            speedBaseOnDifficulty = flt_HighMoveSpeed;
            followDistanceBaseOnDifficulty = flt_HighFollowDistance;
        }


        if (Vector3.Distance(ballPosition, this.transform.position) < followDistanceBaseOnDifficulty)
        {
            //MOVE TORWARD TO BALL VIA AI DIFFICULTY


            Vector3 targetPosition = new Vector3(tf_BallPositon.position.x + xPositionOffset, transform.position.y, transform.position.z);

            float step = flt_MoveSpeed * speedBaseOnDifficulty * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        }
    }

    public float CaclulatePaddleForceForBall(Vector2 _traget)
    {

        Debug.Log("Add force by paddle");

        float distance = Mathf.Abs(_traget.x - tf_BatHitPoint.position.x);

        //Debug.Log("Hit point Distance" + distance); 

        Debug.Log("Sweet point distance : " + distance);
        float force = 0;

        if (distance < 0.08f)
        {
            force = flt_MaxHitForce;
        }
        else
        {
            force = flt_MinHitForce;
        }

        

        return force;
    }

    public float CalculateDistanceFromSwingPoints(Vector3 _traget)
    {
       // Debug.Log("Swing point Method called");
        float distance = 0;
        float rightSwingPointDistance = Vector3.Distance(tf_RightSwingPoint.position, _traget);
        float leftSwingPointDistance = Vector3.Distance(tf_LeftSwingPoint.position, _traget) ;


        if (leftSwingPointDistance > rightSwingPointDistance)
        {
            distance = leftSwingPointDistance;
            
        }
        else
        {
            distance = rightSwingPointDistance;
            distance = -distance;
        }

        if(Mathf.Abs(distance) > 0 && Mathf.Abs(distance)  < 1.05f)
        {
            distance = 0;
        }
        distance *= flt_Swing_Force;

        distance = Mathf.Clamp(distance, flt_MinSwingAngle, flt_MaxSwingAngle);
        return distance;
    }


}
