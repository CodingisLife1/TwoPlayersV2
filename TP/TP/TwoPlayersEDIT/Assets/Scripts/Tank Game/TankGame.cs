using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TankGame : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    [SerializeField] private Transform tank1;
    [SerializeField] private Transform tank2;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed1;
    [SerializeField] private float rotSpeed2;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform startPos1;
    [SerializeField] private Transform startPos2;
    [SerializeField] private Animator tank1_Anim;
    [SerializeField] private Animator tank2_Anim;

    [SerializeField] private GameObject redWin;
    [SerializeField] private GameObject blueWin;
    [SerializeField] private GameObject menu;
    [SerializeField] private TotalScore totalScore;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Tournament tournament;
    [SerializeField] private GameObject player2_Button;

    public TextMeshProUGUI scoreRed_txt;
    public TextMeshProUGUI scoreBlue_txt;

    private int scoreRed;
    private int scoreBlue;

    private bool canRot1;
    private bool canRot2;
    private bool canMove1;
    private bool canMove2;
    public bool mode;

    // Start is called before the first frame update
    void Start()
    {
        canRot1 = true;
        canRot2 = true;

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

    // Update is called once per frame
    void Update()
    {
        if (canRot1)
        {
            tank1.Rotate(Vector3.forward * rotSpeed1 * Time.deltaTime);
        }
        if (canRot2)
        {
            tank2.Rotate(Vector3.forward * rotSpeed2 * Time.deltaTime);
        }


        if (canMove1)
        {
            tank1.Translate(tank1.right * moveSpeed * Time.deltaTime, Space.World);
        }
        if (canMove2)
        {
            tank2.Translate(tank2.right * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            MovePlayer(true);
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            ReverseRotation(true);
        }

        if (mode)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                MovePlayer(false);
            }

            if (Input.GetKeyUp(KeyCode.M))
            {
                ReverseRotation(false);
            }
        }
        

        
        

    }

    public void MovePlayer(bool tank)
    {
        
        if (tank)
        {
            canMove1 = true;
            canRot1 = false;
            GameObject bul = Instantiate(bullet, spawnPoint1.position, Quaternion.identity);
            bul.GetComponent<TankBullet>().direction = tank1.right;
        }
        else
        {
            canMove2 = true;
            canRot2 = false;
            GameObject bul = Instantiate(bullet, spawnPoint2.position, Quaternion.identity);
            bul.GetComponent<TankBullet>().direction = tank2.right;
        }

    }

    public void ReverseRotation(bool tank)
    {
        if (tank)
        {
            canMove1 = false;
            canRot1 = true;
            rotSpeed1 *= -1;
        }
        else
        {
            canMove2 = false;
            canRot2 = true;
            rotSpeed2 *= -1;
        }
        
    }

    public void PlusPoint(bool tank)
    {
        if (!tank)
        {
            scoreRed += 1;
            scoreRed_txt.text = scoreRed.ToString();
            tank2.position = startPos2.position;
            tank2_Anim.SetTrigger("Hit");
            StartCoroutine(Invisible(false));
        }
        else
        {
            scoreBlue += 1;
            scoreBlue_txt.text = scoreBlue.ToString();
            tank1.position = startPos1.position;
            tank1_Anim.SetTrigger("Hit");
            StartCoroutine(Invisible(true));
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
    

    IEnumerator Invisible(bool player)
    {
        if (player)
        {
            tank1.GetChild(0).tag = "Untagged";
            yield return new WaitForSeconds(3);
            tank1.GetChild(0).tag = "Tank1";
        }
        else
        {
            tank2.GetChild(0).tag = "Untagged";
            yield return new WaitForSeconds(3);
            tank2.GetChild(0).tag = "Tank2";

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
        tank1.position = startPos1.position;
        tank2.position = startPos2.position;
        tank1.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        tank2.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        tank1.GetChild(0).tag = "Tank1";
        tank2.GetChild(0).tag = "Tank2";
        StopAllCoroutines();
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
            StartCoroutine(AIPlay());
        }
    }

    IEnumerator AIPlay()
    {
        while (true)
        {
            canRot2 = true;
            yield return new WaitForSeconds(Random.Range(1, 2));
            int r = Random.Range(0, 101);
            canRot2 = false;

            if (r < 50)
            {
                GameObject bul = Instantiate(bullet, spawnPoint2.position, Quaternion.identity);
                bul.GetComponent<TankBullet>().direction = tank2.right;
                canMove2 = true;
                yield return new WaitForSeconds(Random.Range(1, 2));
                canMove2 = false;
            }
            else if (r >= 50)
            {
                float angle = Mathf.Atan2(tank1.position.y - tank2.position.y, tank1.position.x - tank2.position.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                tank2.rotation = Quaternion.RotateTowards(tank2.rotation, targetRotation, 120 * Time.deltaTime);
                yield return new WaitForSeconds(0.5f);

                GameObject bul = Instantiate(bullet, spawnPoint2.position, Quaternion.identity);
                bul.GetComponent<TankBullet>().direction = tank2.right;
                canMove2 = true;
                yield return new WaitForSeconds(Random.Range(0, 1));
                canMove2 = false;
            }
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
        tank1.position = startPos1.position;
        tank2.position = startPos2.position;
        tank1.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        tank2.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        tank1.GetChild(0).tag = "Tank1";
        tank2.GetChild(0).tag = "Tank2";
        StopAllCoroutines();
    }
}
