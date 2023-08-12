using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Required Components")]
    public Transform pf_Ball;
    public AiPaddle aiPaddle;
    public Player player;


    [Header("All Levels Data")]
    public int currentLevelIndex;
    public int[] all_LevelsEntryFees;
    public int[] all_LevelWinningPrice;
    public int[] all_LevelWinningTrophies;
    public int[] all_LevelLossingTrophies;
    public int[] all_RequireTrophiesForLevelUp;


    [Space]
    [Header("Game Properites")]
    public bool isGamePlaying;
    public bool isGameTimeOver;
    public bool isDailyRewardCollected;
    public bool isTargetChased; //flag for check target is chased
    public string winnerName; // store winner name
    public int inningIndex = 0; // store inning 

    [Header("Active Rounds Properites")]
    [SerializeField] private float flt_ActiveGameTime; // When game start currentTimer for game play
    public int roundWicketCount = 0; // total wickets in one inning
    public int roundRunsCount = 0; // total runs in one inning
    [SerializeField] private int maxWicketsForPlay; // the max wickets for complate one inning
    public float currentActiveGameTime = 0; //current active game time
    

    [Header("Player All Properites")]
    public int playerTotalRunsInOneRound; // player total run when player is batting
    public int playerTotalWicketsInOneRound; // player total wickets in one round
    public int playerTotalRuns;
    private Vector3 playerPaddlePosition; // Store Player paddle position
    public bool isPlayerBatting; // flag for check is player bet

    [Header("Ai All Properites")]
    public bool isAiBatting; // flag for check is ai bat
    public int aiTotalRuns; // ai total run when ai is batting
    public int aiTotalWickets; //ai Total Wickets in one round
    private Vector3 aiPaddlePosition; //Store Ai paddle position


    [HideInInspector]
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
        CheckForDailyRewardTImeIsStart();

        playerTotalRuns = DataManager.Instance.GetPlayerTotalRuns();


        if (isGamePlaying)
            ball = Instantiate(pf_Ball.gameObject, transform.position, Quaternion.identity);
    }

    private void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.A))
        {
            UIManager.instance.ui_Achievement.increasemissinValue(1, 5);
        }*/


        
        if(isGamePlaying){
            isGameTimeOver = false;
            currentActiveGameTime += Time.deltaTime;
            UIManager.instance.ui_PlayScreen.txt_GameTime.text = currentActiveGameTime.ToString("00:00");
            if(currentActiveGameTime >= flt_ActiveGameTime)
            {
                //Check if player batting or bowing
                Debug.Log("Game over");
                ChangeInning();
                currentActiveGameTime = 0;

                //Set Player Total Runs
                if (isPlayerBatting)
                {
                    playerTotalRuns = DataManager.Instance.GetPlayerTotalRuns() + playerTotalRunsInOneRound;
                    DataManager.Instance.SetPlayerTotalRuns(playerTotalRuns);
                }

                isGameTimeOver = true;
            }


            //Check FOr target is Chased
            CheckForTargetIsChase();
        }
    }

    public void CheckForDailyRewardTImeIsStart()
    {
        if (DataManager.Instance.GetDailyRewardActiveTimeForBool() == 0)
        {
            isDailyRewardCollected = false;
        }
        else
        {
            isDailyRewardCollected = true;
        }
    }


    public void StartGame()
    {
        isGamePlaying = true;
        SpawnNewBall();
        player.gameObject.SetActive(true);
        aiPaddle.gameObject.SetActive(true);
        UIManager.instance.ui_PlayScreen.gameObject.SetActive(true);
        UIManager.instance.allMenus.SetActive(false);
    }


    //CHeck for wickets 
    public void CheckIsAnyBatsmanRemaing()
    {
        if(roundWicketCount == maxWicketsForPlay - 1)
        {
            //all Out change ining
            Debug.Log("All Out");
            ChangeInning();

            if (isPlayerBatting)
            {
                playerTotalRuns = DataManager.Instance.GetPlayerTotalRuns() + playerTotalRunsInOneRound;
                DataManager.Instance.SetPlayerTotalRuns(playerTotalRuns);
            }

            roundWicketCount = 0;
        }
    }

    //Check of target is Chased if chaseed by player 2 and set gameover
    public void CheckForTargetIsChase()
    {
        if(aiTotalRuns > playerTotalRunsInOneRound)
        {
            Destroy(ball);
            SwapPositions();
            isTargetChased = true;
            //ChangeInning();
            Debug.Log("Ai Wins");
            DataManager.Instance.DecreaseTrophies(all_LevelLossingTrophies[currentLevelIndex]);
            winnerName = "Ai";
            isGamePlaying = false;
            player.gameObject.SetActive(false);
            aiPaddle.gameObject.SetActive(false);
            UIManager.instance.ui_GameOver.gameObject.SetActive(true);
            UIManager.instance.ui_PlayScreen.gameObject.SetActive(false);
        } 
    }

    //Spawn position of players when inning change
    private void SwapPositions()
    {
        aiPaddlePosition = aiPaddle.transform.position;
        playerPaddlePosition = player.transform.position;

        player.transform.position = aiPaddlePosition;
        aiPaddle.transform.position = playerPaddlePosition;

        CheckForAiBattingOrPlayer();
    }

    //Change inning when one inning is Complate
    public void ChangeInning()
    {
        ball.gameObject.SetActive(false);

        isGamePlaying = false;

        player.gameObject.SetActive(false);
        aiPaddle.gameObject.SetActive(false);

        //When Chaning inning check if target is not chased but time is over or all wickets 
        if(inningIndex == 2 && !isTargetChased)
        {
            isGamePlaying = false;
            winnerName = "Player";
            player.gameObject.SetActive(false);
            aiPaddle.gameObject.SetActive(false);

            //Give SLot
            SlotsManager.Instance.GiveSlotIfEmpty();
            UIManager.instance.ui_RewardSlot.SetSlotsDataWhenChangeState();

            DataManager.Instance.IncreaseCoins(all_LevelWinningPrice[currentLevelIndex]);
            DataManager.Instance.IncreaseTrophies(all_LevelWinningTrophies[currentLevelIndex]);

            UIManager.instance.ui_GameOver.gameObject.SetActive(true);
            UIManager.instance.ui_PlayScreen.gameObject.SetActive(false);
        }
        else
        {

            inningIndex++;

            UIManager.instance.ui_TimeScreen.gameObject.SetActive(true);
            UIManager.instance.ui_PlayScreen.gameObject.SetActive(false);
        }
        SwapPositions();

        

       

        //when inning is changing if player is batting set player is bowling and player 2 set batting
        if (player.GetComponent<PlayerPaddleMovement>().isBatting)
        {
            isPlayerBatting = false;
            isAiBatting = true;
        }

        //when inning is changing if player2 is batting set player is bowling and player 1 set batting
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
        UIManager.instance.ui_PlayScreen.txt_Score.text = roundRunsCount.ToString();
        UIManager.instance.ui_PlayScreen.txt_Wickets.text = roundWicketCount.ToString();
    }

    public void Gameover()
    {
        isGamePlaying = false;
        isTargetChased = false;
        player.gameObject.SetActive(false);
        aiPaddle.gameObject.SetActive(false);
        currentActiveGameTime = 0;
        inningIndex = 1;
        aiTotalRuns = 0;
        playerTotalRunsInOneRound = 0;
        playerTotalWicketsInOneRound = 0;
        aiTotalWickets = 0;
        roundRunsCount = 0;
        roundWicketCount = 0;
        UIManager.instance.ui_PlayScreen.txt_Score.text = roundRunsCount.ToString();
        UIManager.instance.ui_PlayScreen.txt_Wickets.text = roundWicketCount.ToString();
    }

    public void IncreaseWicket()
    {
        roundWicketCount++;
        if (player.GetComponent<PlayerPaddleMovement>().isBatting)
        {
            playerTotalWicketsInOneRound++;
        }
        else
        {
            aiTotalWickets++;
        }
        UIManager.instance.ui_PlayScreen.txt_Wickets.text = roundWicketCount.ToString();
        CheckIsAnyBatsmanRemaing();
    }

    public void IncreaseScore(int runs)
    {
        roundRunsCount += runs;
        if(inningIndex == 2)
        {
            CheckForTargetIsChase();
        }
        UIManager.instance.ui_PlayScreen.txt_Score.text = roundRunsCount.ToString();
        if (player.GetComponent<PlayerPaddleMovement>().isBatting)
        {
            playerTotalRunsInOneRound += runs;
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
