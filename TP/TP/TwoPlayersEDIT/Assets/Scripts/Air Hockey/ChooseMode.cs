using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMode : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Init initSDK;
    

    public void Mode(bool twoPlayers)
    {
        if (twoPlayers)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<AI>().enabled = false;
        }
        else
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<AI>().enabled = true;
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
}
