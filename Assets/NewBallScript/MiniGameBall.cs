using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ball_Rb;

    public float flt_MoveSpeed;
    public Transform direction;
    public Vector3 moveDirection;
    [SerializeField] private float flt_MaxSpeed = 30f;


    [SerializeField] private float rigidBodyForce = 2;

    private float playerHitForce;


    private void Start()
    {
        moveDirection = Vector3.down;

        ball_Rb.velocity = Vector3.down * flt_MoveSpeed;
    }



    public float GetCurrentSpeed()
    {
        return ball_Rb.velocity.magnitude;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Name of collide : " + collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerHitForce = collision.gameObject.GetComponent<SweetPointForce>().CalculateBatHitForce(transform.position);
            ball_Rb.velocity = ball_Rb.velocity.normalized * 10;
        }
        else if (collision.gameObject.tag.Equals("UpWall"))
        {
            Debug.Log("Collide with ball");
            
            MiniGameManager.Instance.CheckForAllBallUsed();
            MiniGameManager.Instance.currentBallCount--;
            MiniGameManager.Instance.SpawnNewBall();
            UIManager.instance.ui_MiniGamePlay.SetMiniGamePlayData();
            ball_Rb.velocity = ball_Rb.velocity.normalized * 10;
            Destroy(gameObject);
        }
    }
}
