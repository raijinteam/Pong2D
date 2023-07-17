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
    public bool isGameTimeOver;
    [SerializeField] private float flt_ActiveGameTime; // When game start timer for game play
    public int roundWicketCount = 0; // total wickets in one inning
    public int roundRunsCount = 0; // total runs in one inning
    [SerializeField] private int maxWicketsForPlay; // the max wickets for complate one inning
    public bool isPlayerBatting; // flag for check is player bet
    public bool isAiBatting; // flag for check is ai bat
    public float currentActiveGameTime = 0; //current active game time



    public bool isTargetChased; //flag for check target is chased

    public string winnerName; // store winner name

    public int inningIndex = 0; // store inning 

    public int playerTotalRuns; // player total run when player is batting
    public int playerTotalWickets; // player total wickets in one round
    public int aiTotalRuns; // ai total run when ai is batting
    public int aiTotalWickets; //ai Total Wickets in one round

    private Vector3 aiPaddlePosition; //Store Ai paddle position
    private Vector3 playerPaddlePosition; // Store Player paddle position


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
        if(isGamePlaying)
            ball = Instantiate(pf_Ball.gameObject, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        //Just FOr Testing
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 1 , 1);
            SlotsManager.Instance.CheckForSecondSlotIsEmpty();
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 1, 0);
            SlotsManager.Instance.CheckForStartTimeSlot2();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY , 1);
            SlotsManager.Instance.CheckForFirstSlotIsEmpty();
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME , 0);
            SlotsManager.Instance.CheckForStartTimeSlot1();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 2, 1);
            SlotsManager.Instance.CheckForThiredSlotIsEmpty();
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 2, 0);
            SlotsManager.Instance.CheckForStartTimeSlot3();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_EMPTY + 3, 1);
            SlotsManager.Instance.CheckForForthSlotIsEmpty();
            PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_REWARD_SLOT_START_TIME + 3, 0);
            SlotsManager.Instance.CheckForStartTimeSlot4();
        }



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
                isGameTimeOver = true;
            }


            //Check FOr target is Chased
            CheckForTargetIsChase();
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
            roundWicketCount = 0;
        }
    }

    //Check of target is Chased if chaseed by player 2 and set gameover
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
            winnerName = "Player";
            isGamePlaying = false;
            player.gameObject.SetActive(false);
            aiPaddle.gameObject.SetActive(false);
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

    public void ResetScoreWHenGameover()
    {
        isGamePlaying = true;
        isTargetChased = false;
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
        UIManager.instance.ui_PlayScreen.txt_Score.text = roundRunsCount.ToString();
        UIManager.instance.ui_PlayScreen.txt_Wickets.text = roundWicketCount.ToString();
        SpawnNewBall();
    }

    public void IncreaseWicket()
    {
        roundWicketCount++;
        if (player.GetComponent<PlayerPaddleMovement>().isBatting)
        {
            playerTotalWickets++;
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
