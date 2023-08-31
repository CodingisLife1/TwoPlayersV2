using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    [SerializeField] private Transform startPointPuck;
    [SerializeField] private Transform startPoint1;
    [SerializeField] private Transform startPoint2;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private TextMeshProUGUI scoreRed_txt;
    [SerializeField] private TextMeshProUGUI scoreBlue_txt;
    [SerializeField] private GameObject redWin;
    [SerializeField] private GameObject blueWin;
    [SerializeField] private GameObject menu;
    [SerializeField] private TotalScore totalScore;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Tournament tournament;
    [SerializeField] private Rigidbody2D rb;
    private int scoreRed;
    private int scoreBlue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Red Gates"))
        {
            scoreBlue++;
            scoreBlue_txt.text = scoreBlue.ToString();
        }
        else if (collision.CompareTag("Blue Gates"))
        {
            scoreRed++;
            scoreRed_txt.text = scoreRed.ToString();
        }


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

        rb.velocity = Vector3.zero;
        transform.position = startPointPuck.position;
        player1.position = startPoint1.position;
        player2.position = startPoint2.position;
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
        scoreRed = 0;
        scoreRed_txt.text = "0";
        scoreBlue = 0;
        scoreBlue_txt.text = "0";
        transform.position = startPointPuck.position;
        player1.position = startPoint1.position;
        player2.position = startPoint2.position;
    }

    public void Home()
    {
        menu.SetActive(true);
        gamePanel.SetActive(false);
        blueWin.SetActive(false);
        redWin.SetActive(false);
        scoreRed = 0;
        scoreRed_txt.text = "0";
        scoreBlue = 0;
        scoreBlue_txt.text = "0";
        transform.position = startPointPuck.position;
        player1.position = startPoint1.position;
        player2.position = startPoint2.position;
    }
}
