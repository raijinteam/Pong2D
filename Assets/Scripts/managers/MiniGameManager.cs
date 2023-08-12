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

    public int targetHitCount;
    public float miniGameRunningTimer;
    public float currentMiniGameTimer;

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


    private void Start()
    {
        currentMiniGameTimer = miniGameRunningTimer;
    }

    private void Update()
    {
        if (isMiniGameStart)
        {
            currentMiniGameTimer -= Time.deltaTime;
            if(currentMiniGameTimer <= 0)
            {
                MiniGameOver();
            }
        }
    }

    public void SpawnNewTarget()
    {
        float randomX = Random.Range(minPosX, maxPosX);
        float randomY = Random.Range(minPosY, maxPosY);

        Debug.Log("Spawn Position : " + randomX + " Y : " + randomY);

        targetSpawnPosition.position = new Vector2(randomX, randomY);

        pf_Target.transform.position = targetSpawnPosition.position;
        pf_Target.SetActive(true);

    }


    public void MiniGameOver()
    {
        isMiniGameStart = false;
        currentMiniGameTimer = miniGameRunningTimer;
        UIManager.instance.ui_MiniGame.ui_MiniGameCompelete.gameObject.SetActive(true);
        UIManager.instance.ui_MiniGame.ui_MiniGamePlay.gameObject.SetActive(false);
        Destroy(curremtBall);
        pf_Target.gameObject.SetActive(false);
        pf_ActivePlayer.SetActive(false);
    }


    public void StartMiniGame()
    {
        isMiniGameStart = true;
        SpawnNewTarget();
        SpawnNewBall();
        SpawnActivePlayer();
        UIManager.instance.ui_MiniGame.ui_MiniGamePlay.gameObject.SetActive(true);
        UIManager.instance.ui_Navigation.gameObject.SetActive(false);
        UIManager.instance.ui_UseableResouce.gameObject.SetActive(false);
    }

    public void SpawnNewBall()
    {

        if (isMiniGameStart)
        {
            curremtBall = Instantiate(pf_Ball, new Vector3(0 , 3 , 0), Quaternion.identity);

        }
    }

    public int CalculateRewardAmount()
    {
        return targetHitCount;
    }

    public void SpawnActivePlayer()
    {
        pf_ActivePlayer.transform.position = playerSpawnPosition.position;
        pf_ActivePlayer.SetActive(true);
    }
}
