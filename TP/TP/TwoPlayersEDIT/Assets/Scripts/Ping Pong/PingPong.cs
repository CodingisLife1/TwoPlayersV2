using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    [Header("Paddles")]
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float moveSpeed;
    private bool canMove1;
    private bool canMove2;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    [Header("Ball")]
    [SerializeField] private Transform ball;
    [SerializeField] private float ballSpeed;
    private Vector2 direction;
    private float radius;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreRed_txt;
    [SerializeField] private TextMeshProUGUI scoreBlue_txt;
    private int scoreRed;
    private int scoreBlue;
    [SerializeField] private GameObject menu;
    [SerializeField] private TotalScore totalScore;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject redWin;
    [SerializeField] private GameObject blueWin;
    [SerializeField] private Tournament tournament;

    [SerializeField] private GameObject player2_button;
    [SerializeField] private PingPongAI ai;
    private bool mode;

    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));       
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        direction = Vector2.one.normalized;
        radius = ball.localScale.x / 2;

        
    }

    // Update is called once per frame
    void Update()
    {

        MovePlayers();

        MoveBall();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Move(true);
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            IdleState(true);
        }

        if (mode)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Move(false);
            }

            if (Input.GetKeyUp(KeyCode.M))
            {
                IdleState(false);
            }
        }
        

        

    }
    
    public void Move(bool player)
    {
        if (player)
        {
            canMove1 = true;
        }
        else
        {
            canMove2 = true;
        }
    }

    public void IdleState(bool player)
    {
        if (player)
        {
            canMove1 = false;
        }
        else
        {
            canMove2 = false;

        }
    }

    public void MovePlayers()
    {
        if (canMove1 && player1.position.y < topRight.y - 0.1f - player1.localScale.y * 2)
        {
            player1.Translate(player1.up * moveSpeed * Time.deltaTime);
        }

        if (canMove2 && player2.position.y < topRight.y - 0.1f - player2.localScale.y * 2)
        {
            player2.Translate(player2.up * moveSpeed * Time.deltaTime);
        }

        if (!canMove1 && player1.position.y > bottomLeft.y + 0.1f + player1.localScale.y * 2)
        {
            player1.Translate(player1.up * -moveSpeed * Time.deltaTime);
        }

        if (!canMove2 && player2.position.y > bottomLeft.y + 0.1f + player2.localScale.y * 2)
        {
            player2.Translate(player2.up * -moveSpeed * Time.deltaTime);
        }
    }

    public void MoveBall()
    {
        ball.Translate(direction * ballSpeed * Time.deltaTime);

        //Bounce off the top and bottom
        if (ball.position.y < bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }

        if (ball.position.y > topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        //Check win
        if (ball.position.x < bottomLeft.x + radius && direction.x < 0)
        {
            scoreBlue += 1;
            scoreBlue_txt.text = scoreBlue.ToString();
            
            if (scoreBlue == 5)
            {
                blueWin.SetActive(true);
                if (tournament.inTournament)
                {
                    tournament.PlusScore(false);
                }
                else
                {
                    totalScore.PlusPoint(false);
                }
                StartCoroutine(BackToMenu());
            }

            StartCoroutine(NewStart());
        }

        if (ball.position.x > topRight.x - radius && direction.x > 0)
        {
            scoreRed += 1;
            scoreRed_txt.text = scoreRed.ToString();

            if (scoreRed == 5)
            {

                redWin.SetActive(true);
                if (tournament.inTournament)
                {
                    tournament.PlusScore(true);
                }
                else
                {
                    totalScore.PlusPoint(true);
                }
                StartCoroutine(BackToMenu());
            }

            StartCoroutine(NewStart());
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle1") || collision.CompareTag("Paddle2"))
        {
            direction.x = -direction.x;
        }
    }

    IEnumerator NewStart()
    {
        if (!initSDK.mobile)
        {
            initSDK.redTutuor.text = "Z";
            initSDK.blueTutor.text = "M";
        }
        else
        {
            if (initSDK.language == "ru")
            {
                initSDK.redTutuor.text = "Клик";
                initSDK.blueTutor.text = "Клик"; 
            }
            if (initSDK.language == "en")
            {
                initSDK.redTutuor.text = "Click";
                initSDK.blueTutor.text = "Click";   
            }
            if (initSDK.language == "tr")
            {
                initSDK.redTutuor.text = "Tıkla";
                initSDK.blueTutor.text = "Tıkla"; 
            }
        }
        
        ballSpeed = 0;
        ball.position = Vector2.zero;
        yield return new WaitForSeconds(3);
        ballSpeed = 7f;
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2);
        initSDK.ShowInterstitialAd();
        initSDK.gameNumber += 1;
        if (initSDK.gameNumber == 2)
        {
            initSDK.RateGameFunc();
        }
        menu.SetActive(true);
        gamePanel.SetActive(false);
        blueWin.SetActive(false);
        redWin.SetActive(false);
        scoreBlue = 0;
        scoreBlue_txt.text = "0";
        scoreRed = 0;
        scoreRed_txt.text = "0";
    }

    public void StartNewGame()
    {
        StartCoroutine(NewStart());
    }

    public void ChooseMode(bool twoPlayers)
    {
        if (twoPlayers)
        {
            mode = true;
            player2.gameObject.GetComponent<PingPongAI>().enabled = false;
            player2_button.SetActive(true);
        }
        else
        {
            mode = false;
            player2.gameObject.GetComponent<PingPongAI>().enabled = true;
            player2_button.SetActive(false);
        }
    }
    

    public void Home()
    {
        menu.SetActive(true);
        gamePanel.SetActive(false);
        blueWin.SetActive(false);
        redWin.SetActive(false);
        scoreBlue = 0;
        scoreBlue_txt.text = "0";
        scoreRed = 0;
        scoreRed_txt.text = "0";
    }

    public void NewAIStart()
    {
        StartCoroutine(ai.StartAI());
    }
}

