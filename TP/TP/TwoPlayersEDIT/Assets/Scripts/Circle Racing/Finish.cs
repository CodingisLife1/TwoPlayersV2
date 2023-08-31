using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private CRAdditional circleRacing;

    // Start is called before the first frame update
    void Start()
    {
        circleRacing = GameObject.FindGameObjectWithTag("CircleRacing").GetComponent<CRAdditional>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Car1")
        {
            
            if (circleRacing.checkpoint1)
            {
                circleRacing.lapCount1 += 1;
                circleRacing.lapCount1_txt.text = circleRacing.lapCount1.ToString();

                if (circleRacing.lapCount1 >= 7)
                {
                    circleRacing.redWin.SetActive(true);
                    if (circleRacing.tournament.inTournament)
                    {
                        circleRacing.tournament.PlusScore(true);
                    }
                    else
                    {
                        circleRacing.totalScore.PlusPoint(true);
                    }

                    StartCoroutine(BackToMenu());
                }

                circleRacing.checkpoint1 = false;
            }

        }
        else
        {

            if (circleRacing.checkpoint2)
            {
                circleRacing.lapCount2 += 1;
                circleRacing.lapCount2_txt.text = circleRacing.lapCount2.ToString();

                if (circleRacing.lapCount2 >= 7)
                {
                    circleRacing.blueWin.SetActive(true);
                    if (circleRacing.tournament.inTournament)
                    {
                        circleRacing.tournament.PlusScore(false);
                    }
                    else
                    {
                        circleRacing.totalScore.PlusPoint(false);
                    }

                    StartCoroutine(BackToMenu());
                }

                circleRacing.checkpoint2 = false;
            }



        }
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2);
        circleRacing.menu.SetActive(true);
        circleRacing.gamePanel.SetActive(false);
        circleRacing.blueWin.SetActive(false);
        circleRacing.redWin.SetActive(false);
        circleRacing.player1.transform.position = circleRacing.startPos1.position;

        circleRacing.player2.transform.position = circleRacing.startPos2.position;
        circleRacing.bot.transform.position = circleRacing.startPos2.position;
        circleRacing.lapCount1 = 0;
        circleRacing.lapCount2 = 0;
        circleRacing.lapCount1_txt.text = "0";
        circleRacing.lapCount2_txt.text = "0";
    }

    public void BotFinish()
    {
        circleRacing.lapCount2 += 1;
        Debug.Log(circleRacing.lapCount2);
                circleRacing.lapCount2_txt.text = circleRacing.lapCount2.ToString();

                if (circleRacing.lapCount2 >= 7)
                {
                    circleRacing.blueWin.SetActive(true);
                    if (circleRacing.tournament.inTournament)
                    {
                        circleRacing.tournament.PlusScore(false);
                    }
                    else
                    {
                        circleRacing.totalScore.PlusPoint(false);
                    }

                    StartCoroutine(BackToMenu());
                }

                circleRacing.checkpoint2 = false;
    }

}
