using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterItems : MonoBehaviour
{
    private Shooter shooter;
    // Start is called before the first frame update
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("Shooter").GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet1")
        {
            shooter.score1 += 1;
            shooter.score1_txt.text = shooter.score1.ToString();
            if (shooter.score1 == 10)
            {
                shooter.redWin.SetActive(true);
                if (shooter.tournament.inTournament)
                {
                    shooter.tournament.PlusScore(true);
                }
                else
                {
                    shooter.totalScore.PlusPoint(true);
                }
                shooter.Win();
            }

        }
        else if (collision.gameObject.tag == "Bullet2")
        {
            shooter.score2 += 1;
            shooter.score2_txt.text = shooter.score2.ToString();
            if (shooter.score2 == 10)
            {
                shooter.blueWin.SetActive(true);
                if (shooter.tournament.inTournament)
                {
                    shooter.tournament.PlusScore(false);
                }
                else
                {
                    shooter.totalScore.PlusPoint(false);
                }
                shooter.Win();
            }
        }
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
