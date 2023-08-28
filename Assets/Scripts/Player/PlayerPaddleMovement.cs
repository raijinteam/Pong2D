using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleMovement : MonoBehaviour
{
    [Header("Require Components")]
    [SerializeField] private Rigidbody2D rb_PlayerBody;
    [SerializeField] private Transform tf_BatHitPoint;
    [SerializeField] private Transform tf_RightSwingPoint;
    [SerializeField] private Transform tf_LeftSwingPoint;
    [SerializeField] private SweetPointForce sweetForce;
    [SerializeField] private float flt_MinSwingForce;
    [SerializeField] private float flt_MaxSwingForce;


    [Header("Player Properties")]
    public bool isPlayerMoving;
    public bool isBatting;
    public bool isPlayerRotating;
    [SerializeField] private float flt_MoveSpeed;
    [SerializeField] private float flt_RotateSpeed;
    [SerializeField] private float fltClampOffset;


    [Header("Swing Properites")]
    [SerializeField] private float flt_Swing_Force;


    private float flt_ScreenWidth;
    private float horizontalMove;



    private void OnEnable()
    {
        isPlayerRotating = false;
        transform.position = new Vector3(0, transform.position.y, 0);
    }

    private void Start()
    {
        float screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        flt_ScreenWidth = screenHalfWidthInWorldUnits - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        UserInputs();
        PlayerSwipeMovement();
        RotatePlayer();


        if (isBatting)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isPlayerRotating = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isPlayerRotating = false;
            }
        }
        else
        {
            //transform.Rotate(Vector3.zero);
            transform.rotation = Quaternion.identity;
        }
    }

    private void FixedUpdate()
    {
        PlayerMovements();
    }

    private void UserInputs()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        //VericalMove = Input.GetAxis("Vertical");
    }

    private void PlayerSwipeMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {

                Vector3 newPostion = touch.deltaPosition * flt_MoveSpeed * Time.deltaTime;

                transform.position += new Vector3(newPostion.x, 0, 0);


            }
            else if (touch.phase == TouchPhase.Ended)
            {
                rb_PlayerBody.velocity = Vector2.zero;
            }
        }
    }


    private void RotatePlayer()
    {
        if (isPlayerRotating)
        {
            transform.Rotate(new Vector3(0, 0, flt_RotateSpeed * Time.deltaTime) /** VericalMove*/);
        }
    }

    private void PlayerMovements()
    {
        float moveAmount = horizontalMove * flt_MoveSpeed * Time.deltaTime;


        //Check For Player Move
        //this checker is used in user tutorial for check user has move for some time
        if(moveAmount > 0 || moveAmount < 0)
        {
            isPlayerMoving = true;
        }
        else
        {
            isPlayerMoving = false;
        }



        Vector2 tempPosition = rb_PlayerBody.position + new Vector2(moveAmount, 0);

        float clampX = Mathf.Clamp(tempPosition.x, -flt_ScreenWidth + fltClampOffset, flt_ScreenWidth - fltClampOffset); ;

        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);
        //Move Player using velocity

    }

    public float CaclulatePaddleForceForBall(Vector2 _traget)
    {
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

            if (Mathf.Abs(distance) > 0 && Mathf.Abs(distance) < 1.10f)
            {
                distance = 0;
            }

            // distance *= flt_Swing_Force;

            distance = Mathf.Clamp(distance, flt_MinSwingForce, flt_MaxSwingForce);
            //Debug.Log("Swing Froce : " + distance);
            return distance;
        }
        else
        {
            return 0;
        }

    }
}
