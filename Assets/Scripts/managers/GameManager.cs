using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Required Components")]
    public Transform pf_Ball;
    public AiPaddle aiPaddle;
    public PlayerData player;

    [Header("Game Properites")]
    public bool isGamePlaying;
    [SerializeField] private float flt_ActiveGameTime; // When game start timer for game play
    public int roundWicketCount = 0; // total wickets in one inning
    public int roundRunsCount = 0; // total runs in one inning
    [SerializeField] private int maxWicketsForPlay; // the max wickets for complate one inning
    public bool isPlayerBatting; // flag for check is player bet
    public bool isAiBatting; // flag for check is ai bat
    public float currentActiveGameTime = 0;

    public bool isTargetChased;

    public string winnerName;

    public int inningIndex = 0;

    public int playerTotalRuns; // player total run when player is batting
    public int playerTotalWickets; // player total wickets in one round
    public int aiTotalRuns; // ai total run when ai is batting
    public int aiTotalWickets; //ai Total Wickets in one round

    private Vector3 aiPaddlePosition;
    private Vector3 playerPaddlePosition;


    [SerializeField] private GameObject ball;

    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }

        inningIndex = 1;
    }

    private void Start()
    {
        ball = Instantiate(pf_Ball.gameObject, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if(isGamePlaying){
            currentActiveGameTime += Time.deltaTime;
            UIManager.instance.ui_PlayScren.txt_GameTime.text = currentActiveGameTime.ToString("00:00");
            if(currentActiveGameTime >= flt_ActiveGameTime)
            {
                //Check if player batting or bowing
                Debug.Log("Change Ining");
                currentActiveGameTime = 0;
               // ChangeInning();
            }
        }
    }

    //CHeck for wickets 
    public void CheckIsAnyBatsmanRemaing()
    {
        if(roundWicketCount >= maxWicketsForPlay - 1)
        {
            //all Out change ining
            Debug.Log("All Out");
            ChangeInning();
            roundWicketCount = 0;
        }
    }


    public void CheckForTargetIsChase()
    {
        if(aiTotalRuns > playerTotalRuns)
        {
            Destroy(ball);
            SwapPositions();
            isTargetChased = true;
            //ChangeInning();
            Debug.Log("Ai Wins");
            winnerName = "Ai";
            isGamePlaying = false;
            player.gameObject.SetActive(false);
            aiPaddle.gameObject.SetActive(false);
            UIManager.instance.ui_GameOver.gameObject.SetActive(true);
            UIManager.instance.ui_PlayScren.gameObject.SetActive(false);
        } 
    }


    private void SwapPositions()
    {
        aiPaddlePosition = aiPaddle.transform.position;
        playerPaddlePosition = player.transform.position;

        player.transform.position = aiPaddlePosition;
        aiPaddle.transform.position = playerPaddlePosition;

        CheckForAiBattingOrPlayer();
    }

    public void ChangeInning()
    {

        ball.gameObject.SetActive(false);

        isGamePlaying = false;

        if(inningIndex == 2 && !isTargetChased)
        {
            winnerName = "Player";
            isGamePlaying = false;
            player.gameObject.SetActive(false);
            aiPaddle.gameObject.SetActive(false);
            UIManager.instance.ui_GameOver.gameObject.SetActive(true);
            UIManager.instance.ui_PlayScren.gameObject.SetActive(false);
        }

        SwapPositions();

        inningIndex++;


       

        UIManager.instance.ui_TimerScreen.gameObject.SetActive(true);
        UIManager.instance.ui_PlayScren.gameObject.SetActive(false);


        if (player.GetComponent<PlayerPaddleMovement>().isBatting)
        {
            isPlayerBatting = false;
            isAiBatting = true;
            roundRunsCount = 0;
            
        }

        if (aiPaddle.GetComponent<AiPaddle>().isBatting)
        {
            isPlayerBatting = true;
            isAiBatting = false;
        }

        ResetScoresOnInningChange();
    }

    private void CheckForAiBattingOrPlayer()
    {
        if (aiPaddle.GetComponent<AiPaddle>().isBatting)
        {
            aiPaddle.GetComponent<AiPaddle>().isBatting = false;
        }
        else
        {
            aiPaddle.GetComponent<AiPaddle>().isBatting = true;
        }

        player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (player.GetComponent<PlayerPaddleMovement>().isBatting)
        {
            player.GetComponent<PlayerPaddleMovement>().isBatting = false;
        }
        else
        {
            player.GetComponent<PlayerPaddleMovement>().isBatting = true;
        }
    }


    public void SpawnNewBall()
    {
        if (isGamePlaying)
        {
            Destroy(ball);


            ball = Instantiate(pf_Ball.gameObject, transform.position, Quaternion.identity);
        }
        
    }

    public void ResetScoresOnInningChange()
    {
        roundRunsCount = 0;
        roundWicketCount = 0;
        UIManager.instance.ui_PlayScren.txt_Score.text = roundRunsCount.ToString();
        UIManager.instance.ui_PlayScren.txt_Wickets.text = roundWicketCount.ToString();
    }

    public void ResetScoreWHenGameover()
    {
        isGamePlaying = true;
        isTargetChased = false;
        ball.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        aiPaddle.gameObject.SetActive(true);
        currentActiveGameTime = 0;
        inningIndex = 1;
        aiTotalRuns = 0;
        playerTotalRuns = 0;
        playerTotalWickets = 0;
        aiTotalWickets = 0;
        roundRunsCount = 0;
        roundWicketCount = 0;
        UIManager.instance.ui_PlayScren.txt_Score.text = roundRunsCount.ToString();
        UIManager.instance.ui_PlayScren.txt_Wickets.text = roundWicketCount.ToString();
    }

    public void IncreaseWicket()
    {
        CheckIsAnyBatsmanRemaing();
        roundWicketCount++;
        if (player.GetComponent<PlayerPaddleMovement>().isBatting)
        {
            playerTotalWickets++;
        }
        else
        {
            aiTotalWickets++;
        }
        UIManager.instance.ui_PlayScren.txt_Wickets.text = roundWicketCount.ToString();
    }

    public void IncreaseScore(int runs)
    {
        roundRunsCount += runs;
        if(inningIndex == 2)
        {
            CheckForTargetIsChase();
        }
        UIManager.instance.ui_PlayScren.txt_Score.text = roundRunsCount.ToString();
        if (player.GetComponent<PlayerPaddleMovement>().isBatting)
        {
            playerTotalRuns += runs;
        }
        else
        {
            aiTotalRuns += runs;
        }
    }

    public Transform GetBall
    {
        get
        {
            if (ball != null)
                return ball.transform;
            else
                return null;
        }
    }
}
