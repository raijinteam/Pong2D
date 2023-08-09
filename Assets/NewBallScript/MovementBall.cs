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
    private bool isCollideWithPlayer = false;
    private bool isCollideWithAI = false;

    [SerializeField] private bool isSwinging = false;
    public float swingForce;


    private void Start()
    {
        moveDirection = Vector3.down;
        //direction.right = moveDirection;



        ball_Rb.velocity = Vector3.down * flt_MoveSpeed ;
    }


    private void FixedUpdate()
    {

        Debug.Log("Velocity Megnitutad : " + ball_Rb.velocity.magnitude);




        if (isSwinging)
        {
            ball_Rb.velocity = new Vector2(-1 + ball_Rb.velocity.x, ball_Rb.velocity.y);
            //ball_Rb.AddForce(new Vector2(-1000 * Time.fixedDeltaTime, 0));
        }



        // else
        // {

        //     if(GetCurrentSpeed() < flt_MaxSpeed)
        //     {
        //         ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y);
        //     }

        //     /*  if (isCollideWithPlayer)
        //       {
        //           ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y + playerHitForce * Time.deltaTime);
        //       }*//*
        //   else if (isCollideWithAI)
        //   {
        //       ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y + aiHitForce) * Time.deltaTime;
        //   }*//*
        //       else
        //       {
        //           ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y);
        //       }
        //       //ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y);*/
        // }


        ///* if (GetCurrentSpeed() < flt_MaxSpeed)
        // {
        //     if (isCollideWithPlayer)
        //     {
        //         ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y + playerHitForce * Time.deltaTime);
        //     }
        //     else if(isCollideWithAI)
        //     {
        //         ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y + aiHitForce * Time.deltaTime);
        //     }
        //     else
        //     {
        //         ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y + flt_MoveSpeed * Time.deltaTime);
        //     }
        // }*/


        //     //Debug.Log("Ball Velocity : " + ball_Rb.velocity);


        // //    //ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y);



        // //    if (isSwinging)
        // //    {
        // //        float swingDir = ball_Rb.velocity.x + swingForce;
        // //        ball_Rb.velocity = new Vector2(swingDir, ball_Rb.velocity.y) * Time.deltaTime;
        // //    }
        // //    else
        // //    {
        // //        ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y) * Time.deltaTime * 10;
        // //    }
    }


    public float GetCurrentSpeed()
    {
        return ball_Rb.velocity.magnitude;
    }


    private void Update()
    {
        // ball_Rb.velocity = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y);

        
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //moveDirection = Vector3.Reflect(direction.right, collision.contacts[0].normal);

        //direction.right = moveDirection;

        if (collision.gameObject.tag.Equals("Player") )
        {
            isSwinging = false;
            isCollideWithAI = false;
            isCollideWithPlayer = true;
            playerHitForce = collision.gameObject.GetComponent<SweetPointForce>().CalculateBatHitForce(transform.position);
            ball_Rb.velocity = ball_Rb.velocity.normalized * 10;


            // Debug.Log("Reset Velocity");

            // Debug.Log("Player Hit FIrce : " + playerHitForce);

            
            //ball_Rb.AddForce( direction * 40, ForceMode2D.Force);
        }
        else if (collision.gameObject.tag.Equals("UpWall"))
        {
            isSwinging = false;
            isCollideWithAI = false;
            //Vector2 direction = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y);
            ball_Rb.velocity = ball_Rb.velocity.normalized * 10;
            // ball_Rb.AddForce(direction * 40, ForceMode2D.Force);

           
        }
        else if (collision.gameObject.tag.Equals("AI"))
        {
            isSwinging = true;
            isCollideWithPlayer = false;
            isCollideWithAI = true;
            aiHitForce = collision.gameObject.GetComponent<AiPaddle>().CaclulatePaddleForceForBall(transform.position);
           // Debug.Log("Au Hit force " + aiHitForce);

            
           // Vector2 direction = new Vector2(ball_Rb.velocity.x, ball_Rb.velocity.y);

            ball_Rb.velocity = ball_Rb.velocity.normalized * 10;

            //Debug.Log("Reset Velocity");

            // ball_Rb.AddForce(direction * 40, ForceMode2D.Force);



          //  Debug.Log("Ball velocity : " + ball_Rb.velocity);

            swingForce = collision.gameObject.GetComponent<AiPaddle>().CalculateDistanceFromSwingPoints(transform.position);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
       
    //}
}
