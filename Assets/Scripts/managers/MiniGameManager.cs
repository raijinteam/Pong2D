using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;
    private void Awake()
    {
        if (Instance != this)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool isMiniGameStart;
    public int score;

    public int totalNumberOfBalls;
    public int targetHitCount;
    public int currentBallCount;

    public GameObject pf_Ball;
    public GameObject pf_Target;
    public GameObject pf_ActivePlayer;
    public Transform playerSpawnPosition;

    public Transform targetSpawnPosition;

    private GameObject curremtBall;

    public int rewardAmount;

    [Header("Random Ball Position")]
    [SerializeField] private float minPosX;
    [SerializeField] private float maxPosX;
    [SerializeField] private float minPosY;
    [SerializeField] private float maxPosY;



    public void SpawnNewTarget()
    {
        float randomX = Random.Range(minPosX, maxPosX);
        float randomY = Random.Range(minPosY, maxPosY);

        Debug.Log("Spawn Position : " + randomX + " Y : " + randomY);

        targetSpawnPosition.position = new Vector2(randomX, randomY);

        pf_Target.transform.position = targetSpawnPosition.position;
        pf_Target.SetActive(true);

    }

    public void CheckForAllBallUsed()
    {
        if(currentBallCount <= 0)
        {
            //Gane Complete
            //GAme complete ui show
            UIManager.instance.ui_MiniGamePlay.gameObject.SetActive(false);
            isMiniGameStart = false;
            Destroy(curremtBall);
            pf_ActivePlayer.SetActive(false);

        }
    }


    public void StartMiniGame()
    {
        isMiniGameStart = true;
        SpawnNewTarget();
        SpawnNewBall();
        SpawnActivePlayer();
    }

    public void SpawnNewBall()
    {

        if (isMiniGameStart)
        {
            curremtBall = Instantiate(pf_Ball, Vector2.zero, Quaternion.identity);

        }
    }


    public void SpawnActivePlayer()
    {
        pf_ActivePlayer.transform.position = playerSpawnPosition.position;
        pf_ActivePlayer.SetActive(true);
    }
}
