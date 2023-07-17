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
    [SerializeField] private Collider2D myCollider;
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

    public bool canRotateRight = true;
    public bool canRotateLeft = true;

    [Header("Paddle Rotation")]
    public float rotationSpeed = 90f; // Adjust the speed of rotation if needed
    [SerializeField] private float flt_MinRotatinToReset = 50f;
    [SerializeField] private float flt_MaxRotationToReset = 40f;
    [SerializeField] private float flt_StartRotation;
    [SerializeField] private float targetRotation;
    [SerializeField] private float flt_RoatationSpeed;

    private Coroutine coro_Reset;
    private Coroutine coro_Rotate;

    private void OnEnable()
    {
        transform.position = new Vector3(0, transform.position.y, 0);
        if (!isBatting)
            transform.rotation = Quaternion.identity;

        
    }

    private void Start()
    {
        SetRandomAngleAndPostion();
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
                Debug.Log("Reset COur Run");

                if (coro_Reset == null && coro_Rotate == null) {
                    coro_Reset = StartCoroutine(ResetPaddle());
                }

                canPaddleResetRotation = false;
                currentRotationDelay = 0;
            }
        }

    }


    public void SetRandomAngleAndPostion()
    {
        xPositionOffset = Random.Range(-0.5f, 0.5f);
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
            else {
                isBallInRange = false;
            }


            if (isBallInRange)
            {

                if (ballPosition.x > transform.position.x + 0.1f) {

                    if (canRotateRight) {

                        if (coro_Reset == null && coro_Rotate == null) {
                            coro_Rotate = StartCoroutine(RotatePaddle(flt_RotationLeftAngle));
                        }
                       
                        canRotateRight = false;
                    }
                }
                else if (ballPosition.x < transform.position.x - 0.1f) {

                    if (canRotateLeft) {

                        if (coro_Reset == null && coro_Rotate == null) {
                            coro_Rotate = StartCoroutine(RotatePaddle(flt_RotationLeftAngle));
                        }
                        canRotateLeft = false;
                    }

                }


            }
        }
        
    }

    //private IEnumerator ResetPaddle()
    //{
    //    Vector3 startVector = transform.eulerAngles;

    //    Vector3 stopRotation = new Vector3(0, 0, 180);


    //    if ((transform.eulerAngles.z > flt_MinRotatinToReset &&  transform.eulerAngles.z < flt_MaxRotationToReset))
    //    {
    //        float fltCurrentTime = 0;
    //        while (fltCurrentTime < 1)
    //        {
    //            fltCurrentTime += Time.deltaTime / 0.5f;

    //            transform.eulerAngles = Vector3.Slerp(startVector, stopRotation, fltCurrentTime);
    //            yield return null;
    //        }
    //        transform.eulerAngles = stopRotation;
    //        transform.eulerAngles = new Vector3(0,0,Random.Range(0,15));
    //    }
    //}

    
    IEnumerator RotatePaddleRightSIde()
    {
        float currentRotation = transform.eulerAngles.z;
        float targetRotation = flt_RotationRightANgle;
        bool isRoate360 = false;
        if (targetRotation < currentRotation)
        {
            isRoate360 = true;
            targetRotation += 180;
        }

        while (targetRotation > currentRotation)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotationAmount);
            currentRotation += rotationAmount;
            yield return null;
        }

        if (isRoate360)
        {
            currentRotation -= 180;
            targetRotation -= 180;
            transform.eulerAngles = new Vector3(0, 0, currentRotation);
        }
    }

    IEnumerator RotatePaddleLeftSide()
    {
        float currentRotation = transform.eulerAngles.z;
        float targetRotation = flt_RotationLeftAngle;
        bool isRotate360 = false;
        if(targetRotation < currentRotation)
        {
            //Debug.Log("Current ROtation is big");
           // Debug.Log("Current Rotation is : " + currentRotation + " Target Rotation is : " + targetRotation);
            isRotate360 = true;
            targetRotation += 180;
        }

        while(targetRotation > currentRotation)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationAmount);
            currentRotation += rotationAmount;
            yield return null;
        }
        if (isRotate360)
        {
            currentRotation -= 180;
            targetRotation -= 180;
            transform.eulerAngles = new Vector3(0, 0, currentRotation);
        }

    }


    public float CaclulatePaddleForceForBall(Vector2 _traget)
    {
       
        isHitBall = true;
        isBallInRange = false;
        canRotateRight = true;
       
        canRotateLeft = true;
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

    private IEnumerator ResetPaddle() {

       float moduleValue360 = flt_StartRotation % 360;
        transform.localEulerAngles = new Vector3(0, 0, moduleValue360);

        if (moduleValue360 > 180) {

            int Value = ((int)(flt_StartRotation / 360));
            float index = Random.Range(-5, 5);
            targetRotation = flt_StartRotation + (360 * (Value + 1) - flt_StartRotation + index);

        }
        else {

            int Value = ((int)(flt_StartRotation / 180));
            float index = Random.Range(-5, 5);
            targetRotation = flt_StartRotation + (180 * (Value + 1) - flt_StartRotation + index);
        }


        Quaternion target = Quaternion.Euler(0, 0, targetRotation);

        while (transform.rotation != target) {
            float step = flt_RoatationSpeed * Time.deltaTime;
            Debug.Log("Coro Reset Start");
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);
            yield return null;
        }
        flt_StartRotation = targetRotation;
        coro_Reset = null;

    }

    private IEnumerator RotatePaddle(float angle) {

        targetRotation += angle;

        Quaternion target = Quaternion.Euler(0, 0, targetRotation);

        while (transform.rotation != target) {
            float step = flt_RoatationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);
            Debug.Log("Coro Rotate Start");
            yield return null;
        }
        coro_Rotate = null;
        flt_StartRotation = targetRotation;
    }


}
