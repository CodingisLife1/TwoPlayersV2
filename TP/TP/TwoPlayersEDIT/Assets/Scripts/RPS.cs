using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RPS : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    [SerializeField] private GameObject[] rps1;
    [SerializeField] private GameObject[] rps2;
    [SerializeField] private bool ready1;
    [SerializeField] private bool ready2;
    [SerializeField] private bool gameInprocess;
    [SerializeField] private int choice1;
    [SerializeField] private int choice2;
    [SerializeField] private TextMeshProUGUI scoreRed_txt;
    [SerializeField] private TextMeshProUGUI scoreBlue_txt;
    [SerializeField] private Animator redAnim;
    [SerializeField] private Animator blueAnim;
    private int scoreRed;
    private int scoreBlue;
    private bool mode;

    [SerializeField] private GameObject readyTrueRed;
    [SerializeField] private GameObject readyTrueBlue;
    [SerializeField] private GameObject readyFalseRed;
    [SerializeField] private GameObject readyFalseBlue;

    [SerializeField] private GameObject redWin;
    [SerializeField] private GameObject blueWin;
    [SerializeField] private GameObject menu;
    [SerializeField] private TotalScore totalScore;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Tournament tournament;

    [SerializeField] private GameObject compButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode)
        {
            compButtons.SetActive(true);
            if (ready1 && ready2 && !gameInprocess)
            {
                StartCoroutine(AnimPlay());


                rps1[0].SetActive(false);
                rps1[choice1].SetActive(true);

                rps2[0].SetActive(false);
                rps2[choice2].SetActive(true);

                CheckWin();

                ready1 = false;
                ready2 = false;
            }
        }
        else
        {
            compButtons.SetActive(false);
            if (ready1 && ready2 && !gameInprocess)
            {

                StartCoroutine(AnimPlay());

                rps1[0].SetActive(false);
                rps1[choice1].SetActive(true);

                rps2[0].SetActive(false);
                rps2[choice2].SetActive(true);

                CheckWin();

                ready1 = false;
                ready2 = false;
            }
        }
        
    }

    public void PlayerChoose1(int choose)
    {
        choice1 = choose;
        ready1 = true;
        readyTrueRed.SetActive(true);
        readyFalseRed.SetActive(false);


        if (!mode)
        {
            PlayerChoose2(Random.Range(0, 3));
        }
    }

    public void PlayerChoose2(int choose)
    {
        choice2 = choose;
        ready2 = true;
        readyTrueBlue.SetActive(true);
        readyFalseBlue.SetActive(true);
    }


    public void CheckWin()
    {
        if (choice1 == 0 && choice2 == 1)
        {
            scoreRed++;
            scoreRed_txt.text = scoreRed.ToString();

            StartCoroutine(StartPos());
        }
        else if (choice1 == 1 && choice2 == 2)
        {
            scoreRed++;
            scoreRed_txt.text = scoreRed.ToString();

            StartCoroutine(StartPos());
        }
        else if (choice1 == 2 && choice2 == 0)
        {
            scoreRed++;
            scoreRed_txt.text = scoreRed.ToString();

            StartCoroutine(StartPos());
        }
        else if (choice2 == 0 && choice1 == 1)
        {
            scoreBlue++;
            scoreBlue_txt.text = scoreBlue.ToString();

            StartCoroutine(StartPos());
        }
        else if (choice2 == 1 &&  choice1 == 2)
        {
            scoreBlue++;
            scoreBlue_txt.text = scoreBlue.ToString();

            StartCoroutine(StartPos());
        }
        else if (choice2 == 2 && choice1 == 0)
        {
            scoreBlue++;
            scoreBlue_txt.text = scoreBlue.ToString();

            StartCoroutine(StartPos());
        }
        else if (choice1 == 0 && choice2 == 0)
        {

            StartCoroutine(StartPos());
        }
        else if (choice1 == 1 && choice2 == 1)
        {

            StartCoroutine(StartPos());
        }
        else if (choice1 == 2 && choice2 == 2)
        {

            StartCoroutine(StartPos());
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
    }

    IEnumerator StartPos()
    {
        yield return new WaitForSeconds(2);
        rps1[0].SetActive(true);
        rps1[1].SetActive(false);
        rps1[2].SetActive(false);

        rps2[0].SetActive(true);
        rps2[1].SetActive(false);
        rps2[2].SetActive(false);
    }

    IEnumerator AnimPlay()
    {
        gameInprocess = true;
        redAnim.SetBool("RedTrigger", true);
        blueAnim.SetBool("BlueTrigger", true);

        readyTrueRed.SetActive(true);
        readyTrueBlue.SetActive(true);
        readyFalseRed.SetActive(false);
        readyFalseBlue.SetActive(false);

        yield return new WaitForSeconds(2);

        readyTrueRed.SetActive(false);
        readyTrueBlue.SetActive(false);
        readyFalseRed.SetActive(true);
        readyFalseBlue.SetActive(true);

        redAnim.SetBool("RedTrigger", false);
        blueAnim.SetBool("BlueTrigger", false);
        gameInprocess = false;
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
        readyTrueRed.SetActive(false);
        readyTrueBlue.SetActive(false);
        readyFalseRed.SetActive(true);
        readyFalseBlue.SetActive(true);
    }

    public void ChooseMode(bool twoPlayers)
    {
        if (twoPlayers)
        {
            mode = true;
        }
        else
        {
            mode = false;
        }

        if (!initSDK.mobile)
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
                initSDK.redTutuor.text = "Ticla";
                initSDK.blueTutor.text = "Ticla"; 
            }
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
                initSDK.redTutuor.text = "Ticla";
                initSDK.blueTutor.text = "Ticla"; 
            }
        }
    }
}
