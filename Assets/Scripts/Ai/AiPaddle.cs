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
    public bool isBatting;

    [Header("For Dubbiung")]
    [SerializeField] private float xPositionOffset = 0.5f;
    [SerializeField] private GameObject ball;


    [Header("Require Components")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform tf_BatHitPoint;
    public Transform tf_LeftSwingPoint;
    public Transform tf_RightSwingPoint;
    [SerializeField] private SweetPointForce sweetForce;

    [Header("Paddle Properties")]
    [SerializeField] private float flt_RotationRightANgle;
    [SerializeField] private float flt_RotationLeftAngle;
    [SerializeField] private float flt_MaxRightRotationAngle = 40;
    [SerializeField] private float flt_MaxLeftRotationAngle = 180;
    [SerializeField] private float flt_MinRightRotationAngle;
    [SerializeField] private float flt_MinLeftRotationAngle;
    public bool isHitBall;
    [SerializeField] private bool isBallInRange;



    [Header("AI Difficulty Propertys")]
    [SerializeField] private float flt_MoveSpeed;
    [SerializeField] private AiPaddleDifficulty aiDifficulty;
    [SerializeField] private float flt_LowMoveSpeed;
    [SerializeField] private float flt_MediumMoveSpeed;
    [SerializeField] private float flt_HighMoveSpeed;
    [SerializeField] private float flt_LowFollowDistance = 3f;
    [SerializeField] private float flt_MediumFollowDistance = 5f;
    [SerializeField] private float flt_HighFollowDistance = 7f;
    [SerializeField] private float flt_LowRotateSpeed;
    [SerializeField] private float flt_MediumRotateSpeed;
    [SerializeField] private float flt_HighRotateSpeed;

    [Header("Ball Swing Proprties")]
    [SerializeField] private float flt_Swing_Force;
    [SerializeField] private float flt_MinSwingForce;
    [SerializeField] private float flt_MaxSwingForce;


    private float remainingRotation;

    private void OnEnable()
    {
        transform.position = new Vector3(0, transform.position.y, 0);
    }

    private void Start()
    {
        SetRandomXOffset();
        //flt_MaxRotationAngle = Random.Range(flt_MinRotationAngle, flt_MaxRotationAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.GetBall != null)
        {
            ball = GameManager.instance.GetBall.gameObject;
        }

        AiMovement();
        RotatePlayer();
    }


    public void SetRandomXOffset()
    {
        xPositionOffset = Random.Range(-0.5f, 0.5f);
        flt_RotationRightANgle = Random.Range(flt_MinRightRotationAngle, flt_MaxRightRotationAngle);
        flt_RotationLeftAngle = Random.Range(flt_MinLeftRotationAngle, flt_MaxLeftRotationAngle);
    }

    private void AiMovement()
    {

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


        if (Vector3.Distance(ball.transform.position, this.transform.position) < followDistanceBaseOnDifficulty && !isHitBall)
        {
            isBallInRange = true;
        }

        if (isBallInRange)
        {
            //MOVE TORWARD TO BALL VIA AI DIFFICULTY
            Vector3 targetPosition = new Vector3(GameManager.instance.GetBall.transform.position.x + xPositionOffset, transform.position.y, transform.position.z);

            float step = /*flt_MoveSpeed * */speedBaseOnDifficulty * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
        else
        {
            Vector3 tragetPosition = new Vector3(0 , transform.position.y , 0);
            float step = flt_MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, tragetPosition, step);
        }
    }

    private void RotatePlayer()
    {
        Vector3 ballPosition = GameManager.instance.GetBall.transform.position;
        float rotateSpeedBaseOnDifficulty = 0;
        if (isBatting)
        {
            float followDistance = 0;

            if(aiDifficulty == AiPaddleDifficulty.Easy)
            {
                followDistance = flt_LowFollowDistance;
                rotateSpeedBaseOnDifficulty = flt_LowRotateSpeed;
            }
            else  if (aiDifficulty == AiPaddleDifficulty.Medium)
            {
                followDistance = flt_MediumFollowDistance;
                rotateSpeedBaseOnDifficulty = flt_MediumRotateSpeed;
            }
            if (aiDifficulty == AiPaddleDifficulty.Hard)
            {
                followDistance = flt_HighFollowDistance;
                rotateSpeedBaseOnDifficulty = flt_HighRotateSpeed;
            }

            if(Vector3.Distance(ballPosition , transform.position) < followDistance && !isHitBall)
            {
                isBallInRange = true;
            }
            else if(remainingRotation > 0)
            {
                float rotationStep = Mathf.Min(remainingRotation, 200 * Time.deltaTime);

                // Rotate the object in the counter-clockwise direction
                transform.Rotate(0, 0, rotationStep);

                // Reduce the remaining rotation
                remainingRotation -= rotationStep;



                //transform.Rotate(new Vector3(0, 0, remainingRotation * Time.deltaTime));

                /*                Quaternion targetRotation = Quaternion.Euler(0, 0, 180);

                                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
                */
            }
        }

        if (isBallInRange)
        {
            if (ballPosition.x > transform.position.x + 0.1f)
            {
                //Debug.Log("Rotate Left");
                if (transform.rotation.eulerAngles.z < flt_RotationRightANgle)
                {
                    transform.Rotate(new Vector3(0, 0, rotateSpeedBaseOnDifficulty * Time.deltaTime));
                }
            }
            else if (ballPosition.x < transform.position.x - 0.1f)
            {
                if ( transform.rotation.eulerAngles.z < flt_RotationLeftAngle)
                {
                    transform.Rotate(new Vector3(0, 0, rotateSpeedBaseOnDifficulty * Time.deltaTime));
                }
            }
        }
    }


    public float CaclulatePaddleForceForBall(Vector2 _traget)
    {
        isHitBall = true;
        isBallInRange = false;
        remainingRotation = 360 - (transform.rotation.eulerAngles.z % 180);
        if (!isBatting)
        {
            float distance = Mathf.Abs(_traget.x - tf_BatHitPoint.position.x);

            float force = 0;

            if (distance < 0.09f)
            {
                force = sweetForce.flt_MinHitForce;
            }
            else
            {
                force = sweetForce.flt_MaxHitForce;
            }
            
            return force;
        }
        else
        {
            return sweetForce.CalculateBatHitForce(_traget);
        }
        
    }

    public float CalculateDistanceFromSwingPoints(Vector3 _traget)
    {
        if (!isBatting)
        {
            float distance = 0;
            float rightSwingPointDistance = Vector3.Distance(tf_RightSwingPoint.position, _traget);
            float leftSwingPointDistance = Vector3.Distance(tf_LeftSwingPoint.position, _traget);

            if (leftSwingPointDistance > rightSwingPointDistance)
            {
                distance = leftSwingPointDistance;
                distance = -distance;
            }
            else
            {
                distance = rightSwingPointDistance;
            }

           // Debug.Log("Distance : " + distance);


            if (Mathf.Abs(distance) > 0 && Mathf.Abs(distance) < 0.7f)
            {
                distance = 0;
            }

            distance *= flt_Swing_Force;

            distance = Mathf.Clamp(distance, flt_MinSwingForce, flt_MaxSwingForce);
            return distance;
        }
        else
        {
            return 0;
        }
        
    }


}
