using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int score;
    public float moveSpeed;
    private bool red;
    private Fishing fishing;

    // Start is called before the first frame update
    void Start()
    {
        fishing = GameObject.FindGameObjectWithTag("Fishing").GetComponent<Fishing>();
    }

    // Update is called once per frame
    void Update()
    {
       
        gameObject.transform.Translate(gameObject.transform.right * moveSpeed * Time.deltaTime);

        if (gameObject.transform.position.y >= 4.5f)
        {
            if (red)
            {
                fishing.isCatched1 = false;
            }
            else
            {
                fishing.isCatched2 = false;
            }
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hook1")
        {
            if (tag != "Shark")
            {
                gameObject.transform.parent = collision.transform;
                gameObject.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);        
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, -90f);
                moveSpeed = 0;
                red = true;
                fishing.score1 += score;
                fishing.score1_txt.text = fishing.score1.ToString();
                if (fishing.score1 >= 100)
                {
                    fishing.redWin.SetActive(true);
                    if (fishing.tournament.inTournament)
                    {
                        fishing.tournament.PlusScore(true);
                    }
                    else
                    {
                        fishing.totalScore.PlusPoint(true);
                    }

                    fishing.Win();
                }
                fishing.isCatched1 = true;
            }
            else
            {
                if (fishing.score1 + score < 0)
                {
                    fishing.score1 = 0;
                }
                else
                {
                    fishing.score1 += score;
                }
                
                fishing.score1_txt.text = fishing.score1.ToString();
            }
            
        }

        if (collision.gameObject.tag == "Hook2")
        {
            if (tag != "Shark")
            {
                gameObject.transform.parent = collision.transform;
                gameObject.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, -90f);
                moveSpeed = 0;
                fishing.score2 += score;
                fishing.score2_txt.text = fishing.score2.ToString();
                if (fishing.score2 >= 100)
                {
                    fishing.blueWin.SetActive(true);
                    if (fishing.tournament.inTournament)
                    {
                        fishing.tournament.PlusScore(false);
                    }
                    else
                    {
                        fishing.totalScore.PlusPoint(false);
                    }

                    fishing.Win();
                }
                fishing.isCatched2 = true;
            }
            else
            {
                if (fishing.score2 + score < 0)
                {
                    fishing.score2 = 0;
                }
                else
                {
                    fishing.score2 += score;
                }
                
                fishing.score2_txt.text = fishing.score2.ToString();
            }
            

        }
    }

}
