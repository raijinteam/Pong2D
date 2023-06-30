using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleMovement : MonoBehaviour
{
    [Header("Require Components")]
    [SerializeField] private Rigidbody2D rb_PlayerBody;


    [Header("Player Properties")]
    private bool isPlayerRotating;
    [SerializeField] private float flt_MoveSpeed;
    [SerializeField] private float flt_RotateSpeed;
    [SerializeField] private float fltClampOffset;
    [SerializeField] private float fltMaxClampOffset;


    [Header("Player Swipe Control")]
    private bool isSwiping;
    private bool isMoving;
    private Vector2 firstTouchPossition;
    private Vector2 endTouchPosition;
    private Vector2 desiredPosition;
    [SerializeField] private bool isLeftSwipe;
    [SerializeField] private bool isRightSwipe;
    [SerializeField] private float minSwipeDistance = 20f;
    private float startTime;
    [SerializeField] private float maxSwipeTime = 0.5f;
   [SerializeField] private float minSwipeDistande = 0.17f;

    [Header("Player Hitting ")]
    [SerializeField] private bool isPaddleHitting;
    [SerializeField] private bool canSwingBat = true;
    [SerializeField] private float flt_BatSwingCooldownTime;
    [SerializeField] private float currentBatSwingCooldownTime;

    [Header("Player Bat Rotation Properties")]
    public bool isPlayerSwingBat;
    [SerializeField] private float flt_MaxRotationAngle;
    [SerializeField] private float flt_RotationDuration;
    [SerializeField] private float flt_SwingForce;



    private Vector2 initialPos;
    private float flt_ScreenWidth;
    private float currentSwingForce;
    private float horizontalMove;
    private float VericalMove;



    private void Start()
    {
        canSwingBat = true;
        float screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize ;
        flt_ScreenWidth = screenHalfWidthInWorldUnits - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        UserInputs();
        //  BatLeftAndRightScale();
          PlayerSwipeMovement();
        RotatePlayer();




        if (Input.GetKey(KeyCode.Space))
        {
            isPlayerRotating = true;
        }else if (Input.GetKeyUp(KeyCode.Space))
        {
            isPlayerRotating = false;
        }


        //if (Input.GetKeyDown(KeyCode.Space) && canSwingBat)
        //{
        //    StartCoroutine(PlayerBatSwing(flt_MaxRotationAngle));
        //    canSwingBat = false;
        //}

        //if (isPlayerSwingBat)
        //{
        //    currentSwingForce -= Time.deltaTime;
        //}

        //if (!canSwingBat)
        //{
        //    currentBatSwingCooldownTime += Time.deltaTime;
        //    if (currentBatSwingCooldownTime >= flt_BatSwingCooldownTime)
        //    {
        //        currentBatSwingCooldownTime = 0;
        //        canSwingBat = true;
        //    }
        //}
    }

    private void FixedUpdate()
    {
        PlayerMovements();
    }

    private void UserInputs()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        VericalMove = Input.GetAxis("Vertical");
    }


   


   

    private void PlayerSwipeMovement()
    {
       if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {

                Vector3 newPostion = touch.deltaPosition * flt_MoveSpeed * Time.deltaTime;

                transform.position += new Vector3(newPostion.x ,0 , 0);


            }else if(touch.phase == TouchPhase.Ended)
            {
                rb_PlayerBody.velocity = Vector2.zero;
            }
        }
    }

    
    private void RotatePlayer()
    {
        if (isPlayerRotating)
        {
            transform.Rotate(transform.forward * Time.deltaTime /** VericalMove*/ * flt_RotateSpeed);
        }
    }

    private void PlayerMovements()
    {
        float moveAmount = horizontalMove * flt_MoveSpeed * Time.deltaTime;

        Vector2 tempPosition = rb_PlayerBody.position + new Vector2(moveAmount, 0);

        float clampX = Mathf.Clamp(tempPosition.x, -flt_ScreenWidth + fltClampOffset, flt_ScreenWidth - fltClampOffset); ;

        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);
        //Move Player using velocity

    }


    private void BatLeftAndRightScale()
    {
        Vector2 objectPosition = Camera.main.WorldToScreenPoint(transform.position);

        if(objectPosition.x < Screen.width / 2)
        {
            transform.localScale = new Vector3(-1.5f, transform.localScale.y, transform.localScale.z);
            if (Input.GetKeyDown(KeyCode.Space) && canSwingBat)
            {
                StartCoroutine(PlayerBatSwing(flt_MaxRotationAngle));
                canSwingBat = false;
            }
        }
        else if(objectPosition.x > Screen.width / 2)
        {
            transform.localScale = new Vector3(1.5f, transform.localScale.y, transform.localScale.z);
            if (Input.GetKeyDown(KeyCode.Space) && canSwingBat)
            {
                StartCoroutine(PlayerBatSwing(-flt_MaxRotationAngle));
                canSwingBat = false;
            }
        }
    }

    private IEnumerator PlayerBatSwing(float _rotationAngle)
    {
        isPlayerSwingBat = true;
        currentSwingForce = flt_SwingForce;
        isPaddleHitting = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, _rotationAngle);

        float _currentRotationTime = 0;

        while(_currentRotationTime < 1)
        {
            _currentRotationTime += Time.deltaTime / flt_RotationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, _currentRotationTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        transform.rotation = Quaternion.identity;
        isPlayerSwingBat = false;
        isPaddleHitting = false;
        currentSwingForce = flt_SwingForce;
    }

    public float CalculateBatForceWhenSwing()
    {
        return Mathf.Abs(currentSwingForce);
    }

    public void OnPointerDown_RotatePaddle()
    {
        isPlayerRotating = true;
    }

    public void OnPointerUp_StopRotatePaddle()
    {
        isPlayerRotating = false;
    }
}
