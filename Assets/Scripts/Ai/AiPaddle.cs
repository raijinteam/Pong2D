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
    [HideInInspector]
    public bool isHitBall;
    [SerializeField] private bool canPaddleResetRotation;
    [SerializeField] private float flt_RotationDelay;
    [SerializeField] private float currentRotationDelay;

    [Header("For Dubbiung")]
    //[HideInInspector]
    [SerializeField] private float xPositionOffset = 0.5f;
    [HideInInspector]
    [SerializeField] private GameObject ball;


    [Header("Require Components")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform tf_BatHitPoint;
    public Transform tf_LeftSwingPoint;
    public Transform tf_RightSwingPoint;
    [SerializeField] private SweetPointForce sweetForce;

    [Header("Paddle Properties")]
    [SerializeField] private float flt_RotationRightANgle;
    [SerializeField] private float flt_MinRightRotationAngle;
    [SerializeField] private float flt_MaxRightRotationAngle = 40;
    [Space]
    [SerializeField] private float flt_RotationLeftAngle;
    [SerializeField] private float flt_MinLeftRotationAngle;
    [SerializeField] private float flt_MaxLeftRotationAngle = 180;

    
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
    [HideInInspector]
    [SerializeField] private float flt_MinSwingForce;
    [HideInInspector]
    [SerializeField] private float flt_MaxSwingForce;


    [SerializeField] private float remainingRotation;

    private void OnEnable()
    {
        transform.position = new Vector3(0, transform.position.y, 0);
        if (!isBatting)
            transform.rotation = Quaternion.identity;
    }

    private void Start()
    {
        SetRandomXOffset();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetBall != null)
        {
            ball = GameManager.instance.GetBall.gameObject;
        }

        AiMovement();
        RotatePlayer();

        if (canPaddleResetRotation)
        {
            currentRotationDelay += Time.deltaTime;
            if (currentRotationDelay >= flt_RotationDelay)
            {
                //remainingRotation = 180 - (transform.localEulerAngles.z % 180);
                StartCoroutine(ResetPaddle());
                canPaddleResetRotation = false;
                currentRotationDelay = 0;
            }
        }

    }


    public void SetRandomXOffset()
    {
        //xPositionOffset = Random.Range(-0.5f, 0.5f);
        if (isBatting)
        {
            flt_RotationRightANgle = Random.Range(flt_MinRightRotationAngle, flt_MaxRightRotationAngle);
            flt_RotationLeftAngle = Random.Range(flt_MinLeftRotationAngle, flt_MaxLeftRotationAngle);
        }
        
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
            Vector3 tragetPosition = new Vector3(0, transform.position.y, 0);
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

            if (aiDifficulty == AiPaddleDifficulty.Easy)
            {
                followDistance = flt_LowFollowDistance;
                rotateSpeedBaseOnDifficulty = flt_LowRotateSpeed;
            }
            else if (aiDifficulty == AiPaddleDifficulty.Medium)
            {
                followDistance = flt_MediumFollowDistance;
                rotateSpeedBaseOnDifficulty = flt_MediumRotateSpeed;
            }
            if (aiDifficulty == AiPaddleDifficulty.Hard)
            {
                followDistance = flt_HighFollowDistance;
                rotateSpeedBaseOnDifficulty = flt_HighRotateSpeed;
            }

            if (Vector3.Distance(ballPosition, transform.position) < followDistance && !isHitBall)
            {
                isBallInRange = true;
            }
            /*else if ((remainingRotation > 50 && remainingRotation < 90) || (remainingRotation > 180 && remainingRotation < 150))
            {
                Debug.Log("Reset Rotation");

                float rotationStep = remainingRotation;
                float maxRotationStep = 50 * Time.deltaTime;

                if (rotationStep > maxRotationStep)
                {
                    rotationStep = maxRotationStep;
                }

                // Rotate the object in the counter-clockwise direction
                transform.localEulerAngles = new Vector3(0, 0, remainingRotation * maxRotationStep);

                // Reduce the remaining rotation
                remainingRotation -= rotationStep;
            }*/


            if (isBallInRange)
            {
                if (ballPosition.x > transform.position.x + 0.1f)
                {
                    
                    if (transform.localEulerAngles.z < flt_RotationRightANgle)
                    {
                        float rotationAmount = rotateSpeedBaseOnDifficulty * Time.deltaTime;
                        float newZRotation = transform.localEulerAngles.z + rotationAmount;

                        if (newZRotation > flt_RotationRightANgle)
                        {
                            newZRotation = flt_RotationRightANgle;
                        }
                        transform.localEulerAngles = new Vector3(0, 0, newZRotation % 180);
                    }
                    else if (transform.localEulerAngles.z > flt_RotationRightANgle)
                    {
                        float rotationAmount = rotateSpeedBaseOnDifficulty * Time.deltaTime;
                        float newZRotation = (transform.localEulerAngles.z) + rotationAmount;

                        Debug.Log("Rotation Amount : " + rotationAmount + " New Rotation : " + newZRotation);

                        if (newZRotation < flt_RotationRightANgle)
                        {
                            newZRotation = flt_RotationRightANgle;
                        }else if(newZRotation >= flt_RotationRightANgle)
                        {                             
                            newZRotation = flt_RotationRightANgle;
                        }

                        transform.localEulerAngles = new Vector3(0, 0, newZRotation % 180);
                        // StartCoroutine(RotatePaddle());
                    }
                }
                else if (ballPosition.x < transform.position.x - 0.1f)
                {
                    if ((transform.localEulerAngles.z < flt_RotationLeftAngle))
                    {
                        //Debug.Log("In If block");
                        float rotationAmount = rotateSpeedBaseOnDifficulty * Time.deltaTime;
                        float newZRotation = transform.localEulerAngles.z + rotationAmount;

                        if (newZRotation > flt_RotationLeftAngle)
                        {
                            newZRotation = flt_RotationLeftAngle;
                        }

                        transform.localEulerAngles = new Vector3(0, 0, newZRotation % 180);
                    }
                    else if (transform.localEulerAngles.z > flt_RotationLeftAngle)
                    {
                        //Debug.Log("In else block");
                        float rotationAmount = rotateSpeedBaseOnDifficulty * Time.deltaTime;
                        float newZRotation = transform.localEulerAngles.z + rotationAmount;

                        if (newZRotation < flt_RotationLeftAngle)
                        {
                            newZRotation = flt_RotationLeftAngle;
                        }

                        transform.localEulerAngles = new Vector3(0, 0, newZRotation % 180);
                    }
                }
            }
        }
        else
        {
            transform.localEulerAngles = (Vector3.zero);
        }
    }

    private IEnumerator ResetPaddle()
    {
        Vector3 startVector = transform.localEulerAngles;
        Vector3 targetVector = new Vector3(0, 0, Random.Range(0, 15));

        if(transform.localEulerAngles.z > 50 && transform.localEulerAngles.z < 90)
        {
            float fltCurrentTime = 0;
            while(fltCurrentTime < 1)
            {
                fltCurrentTime += Time.deltaTime / 1;

                transform.localEulerAngles = Vector3.Slerp(startVector, targetVector, fltCurrentTime);
                yield return null;
            }
            transform.localEulerAngles = targetVector;
        }
    }

    private IEnumerator RotatePaddle()
    {
        Quaternion startVector = transform.localRotation;
        Quaternion stopVector = Quaternion.Euler(0, 0, 180 );
        Quaternion targetVector = Quaternion.Euler(0, 0, 270);
        Quaternion reachVector = Quaternion.Euler(0, 0, flt_RotationRightANgle);

        if (transform.localEulerAngles.z > flt_RotationRightANgle)
        {
            float fltCurrentTIme = 0;
            while (fltCurrentTIme < 1)
            {
                fltCurrentTIme += Time.deltaTime / 0.2f;
                transform.localRotation = Quaternion.Slerp(startVector, stopVector, fltCurrentTIme);
                yield return null;
            }
            transform.localRotation = stopVector;
            fltCurrentTIme = 0;
            startVector = transform.localRotation;
           while (fltCurrentTIme < 1)
            {
                fltCurrentTIme += Time.deltaTime / 0.2f;
                transform.localRotation = Quaternion.Slerp(startVector, targetVector, fltCurrentTIme);
                yield return null;
            }
            transform.localRotation = targetVector;
            fltCurrentTIme = 0;
            startVector = transform.localRotation;
            while (fltCurrentTIme < 1)
            {
                fltCurrentTIme += Time.deltaTime / 0.2f;
                transform.localRotation = Quaternion.Slerp(startVector, reachVector, fltCurrentTIme);
                yield return null;
            }
            transform.localRotation = reachVector;
        }
    }

    public float CaclulatePaddleForceForBall(Vector2 _traget)
    {
        isHitBall = true;
        isBallInRange = false;
        if (isBatting)
        {
            canPaddleResetRotation = true;/*
           /* if (transform.localEulerAngles.z >= 180)
            {
                transform.rotation = Quaternion.identity;
            }*/
        }

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
