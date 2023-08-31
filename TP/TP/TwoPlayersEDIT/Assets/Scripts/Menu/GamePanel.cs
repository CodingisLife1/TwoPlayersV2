using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private GameObject justGame;
    [SerializeField] private Tournament tournament;

    void Update()
    {
        if (tournament.inTournament)
        {
            justGame.SetActive(false);
        }
        else
        {
            justGame.SetActive(true);
        }
    }

}
