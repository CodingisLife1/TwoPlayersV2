using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Painting : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    [SerializeField] private DynamicJoystick joystickRed;
    [SerializeField] private DynamicJoystick joystickBlue;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform playerRed;
    [SerializeField] private Transform playerBlue;
    [SerializeField] private Transform startPosRed;
    [SerializeField] private Transform startPosBlue;
    [SerializeField] private GameObject[] field;
    public TextMeshProUGUI scoreRed_txt;
    public TextMeshProUGUI scoreBlue_txt;
    public int scoreRed;
    public int scoreBlue;
    private bool mode;

    [SerializeField] private GameObject redWin;
    [SerializeField] private GameObject blueWin;
    [SerializeField] private GameObject menu;
    [SerializeField] private TotalScore totalScore;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Tournament tournament;
    [SerializeField] private PaintingAI ai;

    [SerializeField] private Init init;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (init.mobile)
        {
            playerRed.Translate(joystickRed.Horizontal * moveSpeed * Time.deltaTime, joystickRed.Vertical * moveSpeed * Time.deltaTime, 0);
            playerBlue.Translate(joystickBlue.Horizontal * moveSpeed * Time.deltaTime, joystickBlue.Vertical * moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            playerRed.Translate(Input.GetAxis("HorizontalRed") * moveSpeed * Time.deltaTime, Input.GetAxis("VerticalRed") * moveSpeed * Time.deltaTime, 0);
            if (mode)
            {
                playerBlue.Translate(Input.GetAxis("HorizontalBlue") * moveSpeed * Time.deltaTime, Input.GetAxis("VerticalBlue") * moveSpeed * Time.deltaTime, 0);

            }

            joystickRed.gameObject.SetActive(false);
            joystickBlue.gameObject.SetActive(false);
        }
        
    }

    public void NewStart()
    {
        if (initSDK.mobile)
        {
            joystickBlue.gameObject.SetActive(true);
            joystickRed.gameObject.SetActive(true);
        }
        else
        {
            joystickBlue.gameObject.SetActive(false);
            joystickRed.gameObject.SetActive(false);
        }

        if (!initSDK.mobile)
        {
            if (initSDK.language == "ru")
            {
                initSDK.redTutuor.text = "WASD";
                initSDK.blueTutor.text = "Стрелки"; 
            }
            if (initSDK.language == "en")
            {
                initSDK.redTutuor.text = "WASD";
                initSDK.blueTutor.text = "Arrows";   
            }
            if (initSDK.language == "tr")
            {
                initSDK.redTutuor.text = "WASD";
                initSDK.blueTutor.text = "Oklar"; 
            }
        }
        else
        {
            if (initSDK.language == "ru")
            {
                initSDK.redTutuor.text = "Джойстик";
                initSDK.blueTutor.text = "Джойстик"; 
            }
            if (initSDK.language == "en")
            {
                initSDK.redTutuor.text = "Joystick";
                initSDK.blueTutor.text = "Joystick";   
            }
            if (initSDK.language == "tr")
            {
                initSDK.redTutuor.text = "Oyun kolu";
                initSDK.blueTutor.text = "Oyun kolu"; 
            }
        }
        
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(30f);
        if (scoreRed > scoreBlue)
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
        else
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
        playerRed.position = startPosRed.position;
        playerBlue.position = startPosBlue.position;

        foreach (var item in field)
        {
            item.GetComponent<SpriteRenderer>().color = Color.white;
        }
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
        playerRed.position = startPosRed.position;
        playerBlue.position = startPosBlue.position;

        foreach (var item in field)
        {
            item.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void ChooseMode(bool twoPlayers)
    {
        if (twoPlayers)
        {
            mode = true;
            joystickBlue.gameObject.SetActive(true);
            playerBlue.gameObject.GetComponent<PaintingAI>().enabled = false;
        }
        else
        {
            mode = false;
            joystickBlue.gameObject.SetActive(false);
            playerBlue.gameObject.GetComponent<PaintingAI>().enabled = true;
        }
    }

    public void NewAIStart()
    {
        StartCoroutine(ai.AIStart());
    }
}
