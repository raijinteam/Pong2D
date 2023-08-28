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
    public GameObject pf_Player;
    public GameObject pf_Ai;
    public GameObject boundriesSystem;


    [Header("User Tutorail")]
    public bool isTutorialRunning;
    public bool isTutorialFirstInning;
    public bool isBatTutorialGameStart;
    public bool isBallTutorialStart;
    public Transform tf_TargteParent;
    public GameObject pf_Target;
    public GameObject currentTarget;
    public int testTargetHitCount;
    public int testBallTargetHitCount;
    public float minTargetSpawmPosX;
    public float maxTargetSpawmPosX;
    public float minTargetSpawmPosY;
    public float maxTargetSpawmPosY;


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
    public float flt_BattingPosition = 4.5f;
    public float flt_BowlwingPosition = -4.5f;

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

        if (isGamePlaying)
        {
            isGameTimeOver = false;
            currentActiveGameTime += Time.deltaTime;
            UIManager.instance.ui_PlayScreen.txt_GameTime.text = currentActiveGameTime.ToString("00:00");
            if (currentActiveGameTime >= flt_ActiveGameTime)
            {
                //Check if player batting or bowing
                //Debug.Log("Change inning");
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





    #region Tutorial Game All Functions


    public void StartTutorialGame()
    {
        player.gameObject.SetActive(true);
        boundriesSystem.SetActive(false);
        UIManager.instance.ui_PlayScreen.gameObject.SetActive(false);
        UIManager.instance.ui_HomePanel.gameObject.SetActive(false);
        UIManager.instance.ui_Navigation.gameObject.SetActive(false);
        UIManager.instance.ui_UseableResouce.gameObject.SetActive(false);
        UIManager.instance.ui_PlayScreen.SetTragetCount(0, 5);
    }


    public void IncreaseTargetHitCountTutorial()
    {
        if (isBatTutorialGameStart)
        {
            testTargetHitCount++;
            UIManager.instance.ui_PlayScreen.SetTragetCount(testTargetHitCount, 5);
            //check if hit
            if (testTargetHitCount >= 5)
            {
                isBatTutorialGameStart = false;
                ball.gameObject.SetActive(false);
                Destroy(ball);
                //Debug.Log("5 Target Is Hit");
            }
        }
        else if (isBallTutorialStart)
        {
            testBallTargetHitCount++;
            if (testBallTargetHitCount >= 5)
            {
                isBallTutorialStart = false;
                ball.gameObject.SetActive(false);
                Destroy(ball);
                // Debug.Log("5 Target Is Hit");
            }
        }


    }

    public void SpawnNewTargetTutorial()
    {
        Destroy(currentTarget);
        if (isBatTutorialGameStart)
        {
            Vector2 position = new Vector2(Random.Range(minTargetSpawmPosX, maxTargetSpawmPosX), Random.Range(minTargetSpawmPosY, maxTargetSpawmPosY));

            //Debug.Log("Spawn new Target");

            currentTarget = Instantiate(pf_Target, position, Quaternion.identity, tf_TargteParent);
        }
        else if (isBallTutorialStart)
        {
            Vector2 position = new Vector2(Random.Range(minTargetSpawmPosX, maxTargetSpawmPosX), Random.Range(Mathf.Abs(minTargetSpawmPosY), Mathf.Abs(maxTargetSpawmPosY)));

            // Debug.Log("Spawn new Target");

            currentTarget = Instantiate(pf_Target, position, Quaternion.identity, tf_TargteParent);
        }

    }


    public void BowlingTutorialStart()
    {
        // Debug.Log("Bowling tutorial Stary");
        isBallTutorialStart = true;
        player.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        player.playerMovement.isBatting = false;
        SpawnNewBall();
    }

    #endregion



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
        player.gameObject.SetActive(true);
        aiPaddle.gameObject.SetActive(true);
        boundriesSystem.SetActive(true);

        if (DataManager.Instance.isGameFirstTimeLoad)
        {
            player.playerMovement.isBatting = true;
            aiPaddle.isBatting = false;
            SwapPositions();
        }
        if(!isBatTutorialGameStart)
        {
            UIManager.instance.ui_PlayScreen.SetTragetCount(0, 0);
        }
        SpawnNewBall();
        CheckWhoesBatting();
        UIManager.instance.ui_PlayScreen.gameObject.SetActive(true);
        UIManager.instance.allMenus.SetActive(false);


    }



    private void CheckWhoesBatting()
    {
        if (player.playerMovement.isBatting)
        {
            isPlayerBatting = true;
            isAiBatting = false;
        }
        else
        {
            isPlayerBatting = false;
            isAiBatting = true;
        }
    }



    //CHeck for wickets 
    public void CheckIsAnyBatsmanRemaing()
    {
        if (roundWicketCount == maxWicketsForPlay - 1)
        {
            //all Out change ining
            Debug.Log("All Out");
            ChangeInning();
            currentActiveGameTime = flt_ActiveGameTime;

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
        if (aiTotalRuns > playerTotalRunsInOneRound)
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

        if (!DataManager.Instance.isGameFirstTimeLoad)
        {
            SpawnBattingSides();
        }


        if (DataManager.Instance.isGameFirstTimeLoad && inningIndex == 1 && !isBallTutorialStart)
        {
            player.playerMovement.isBatting = true;
            aiPaddle.isBatting = false;
        }
        else if(isTutorialFirstInning)
        {
            player.playerMovement.isBatting = true;
            aiPaddle.isBatting = false;
        }else if(isTutorialFirstInning && inningIndex == 2)
        {
            player.playerMovement.isBatting = false;
            aiPaddle.isBatting = true;
        }

        if (player.playerMovement.isBatting)
        {
            player.transform.position = new Vector2(player.transform.position.x, 4.5f);
            aiPaddle.transform.position = new Vector2(aiPaddle.transform.position.x, -4.5f);
        }
        else
        {
            player.transform.position = new Vector2(player.transform.position.x, -4.5f);
            aiPaddle.transform.position = new Vector2(aiPaddle.transform.position.x, 4.5f);
        }
    }

    //Change inning when one inning is Complate
    public void ChangeInning()
    {
        ball.gameObject.SetActive(false);

        isGamePlaying = false;

        player.gameObject.SetActive(false);
        aiPaddle.gameObject.SetActive(false);

        //When Chaning inning check if target is not chased but time is over or all wickets 
        if (inningIndex == 2 && !isTargetChased)
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

    //if player batting spawn side of [layer 
    private void SpawnBattingSides()
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

        if (isBatTutorialGameStart)
        {
            Destroy(ball);
            ball = Instantiate(pf_Ball.gameObject, transform.position, Quaternion.identity);
        }
        else if (isBallTutorialStart)
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
        ResetDataForGameOver();
        UIManager.instance.ui_PlayScreen.txt_Score.text = roundRunsCount.ToString();
        UIManager.instance.ui_PlayScreen.txt_Wickets.text = roundWicketCount.ToString();
    }

    public void ResetDataForGameOver()
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
        if (inningIndex == 2)
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
