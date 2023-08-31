using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CRAdditional : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    public bool checkpoint1;
    public bool checkpoint2;
    public bool finish1;
    public bool finish2;
    public int lapCount1;
    public int lapCount2;
    public TextMeshProUGUI lapCount1_txt;
    public TextMeshProUGUI lapCount2_txt;
    public Transform startPos1;
    public Transform startPos2;

    public GameObject redWin;
    public GameObject blueWin;
    public GameObject menu;
    public TotalScore totalScore;
    public GameObject gamePanel;

    public Tournament tournament;

    public bool mode;
    public GameObject player1;
    public GameObject player2;
    public GameObject bot;
    [SerializeField] private GameObject player2_joystick;
    

    public void ChooseMode(bool twoPlayers)
    {
        if (twoPlayers)
        {
            player2.GetComponent<CarInput>().enabled = true;
            player2_joystick.SetActive(true);
            player2.SetActive(true);
            bot.SetActive(false);
        }
        else
        {
            player2.SetActive(false);
            bot.SetActive(true);
            player2_joystick.SetActive(false);
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
    }

    public void Home()
    {
        initSDK.ShowInterstitialAd();
        initSDK.gameNumber += 1;
        if (initSDK.gameNumber == 2)
        {
            initSDK.RateGameFunc();
        }
        menu.SetActive(true);
        gamePanel.SetActive(false);
        player1.transform.position = startPos1.position;
        Debug.Log(player1.transform.position);
        Debug.Log(startPos1.position);
        player2.transform.position = startPos2.position;
        bot.transform.position = startPos2.position;
        lapCount1 = 0;
        lapCount2 = 0;
        lapCount1_txt.text = "0";
        lapCount2_txt.text = "0";
    }
}
