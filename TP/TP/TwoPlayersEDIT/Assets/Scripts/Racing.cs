using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Racing : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float speed1;
    [SerializeField] private float speed2;
    [SerializeField] private Transform finish;
    [SerializeField] private Slider player1_Slider;
    [SerializeField] private Slider player2_Slider;
    private float stopTouch1;
    private float stopTouch2;
    [SerializeField] private Transform startPos1;
    [SerializeField] private Transform startPos2;

    private bool mode;

    [SerializeField] private GameObject redWin;
    [SerializeField] private GameObject blueWin;
    [SerializeField] private GameObject menu;
    [SerializeField] private TotalScore totalScore;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Tournament tournament;
    [SerializeField] private GameObject player2_Button;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
        speed1 = 0f;
        speed2 = 0f;

        player1_Slider.maxValue = finish.position.y - player1.position.y;
        player2_Slider.maxValue = finish.position.y - player2.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        player1.Translate(player1.up * speed1 * Time.deltaTime);
        player2.Translate(player2.up * speed2 * Time.deltaTime);

        player1_Slider.value = player1_Slider.maxValue - (finish.position.y - player1.position.y);
        player2_Slider.value = player2_Slider.maxValue - (finish.position.y - player2.position.y);

        //Ñëåäîâàíèå êàìåðû çà èãðîêîì âïåðåäè
        if (player1.position.y > player2.position.y && player1.position.y > 0f)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, player1.position.y, Camera.main.transform.position.z);
        }
        if (player2.position.y > player1.position.y && player2.position.y > 0f)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, player2.position.y, Camera.main.transform.position.z);
        }


        if (player1.position.y >= finish.position.y)
        {
            player1.gameObject.SetActive(false);
            player1.position = startPos1.position;
            player2.position = startPos2.position;
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

        if (player2.position.y >= finish.position.y)
        {
            player2.gameObject.SetActive(false);
            player2.position = startPos2.position;
            player1.position = startPos1.position;
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Move(true);
        }

        if (mode)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Move(false);
            }
        }
        
    }

    public void Move(bool player)
    {
        if (player)
        {
            speed1 += 1f;
            stopTouch1 = 0f;
        }
        else
        {
            speed2 += 1f;
            stopTouch2 = 0f;
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            stopTouch1 += 0.25f;
            stopTouch2 += 0.25f;

            if (stopTouch1 >= 0.5f && speed1 >= 1f)
            {
                speed1 -= 1f;

            }

            if (mode)
            {
                if (stopTouch2 >= 0.5f && speed2 >= 1f)
                {
                    speed2 -= 1f;
                }
            }
            
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
        speed1 = 0;
        speed2 = 0;
        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(true);
        player1_Slider.value = 0;
        player2_Slider.value = 0;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        player1.position = startPos1.position;
        player2.position = startPos2.position;
    }

    public void ChooseMode(bool twoPlayers)
    {
        if (twoPlayers)
        {
            mode = true;
            player2_Button.SetActive(true);
        }
        else
        {
            mode = false;
            player2_Button.SetActive(false);
            StartCoroutine(AISpeed());
        }

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
    }

    IEnumerator AISpeed()
    {
        speed2 = 2f;
        yield return new WaitForSeconds(1);
        speed2 = 6f;
        yield return new WaitForSeconds(1);
        speed2 = 10f;
    }

    public void Home()
    {
        menu.SetActive(true);
        gamePanel.SetActive(false);
        blueWin.SetActive(false);
        redWin.SetActive(false);
        speed1 = 0;
        speed2 = 0;
        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(true);
        player1_Slider.value = 0;
        player2_Slider.value = 0;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        player1.position = startPos1.position;
        player2.position = startPos2.position;
    }
}
