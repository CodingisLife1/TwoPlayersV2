using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    [SerializeField] private GameObject[] items;

    [SerializeField] private Transform bottomPoint1;
    [SerializeField] private Transform bottomPoint2;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject player2_button;
    [SerializeField] private ShooterAI ai;
    public TextMeshProUGUI score1_txt;
    public TextMeshProUGUI score2_txt;
    public int score1;
    public int score2;

    private List<GameObject> objects = new List<GameObject>();
    private bool moveUp1;
    private bool moveUp2;
    private bool mode;

    public GameObject redWin;
    public GameObject blueWin;
    public GameObject menu;
    public TotalScore totalScore;
    public GameObject gamePanel;
    public Tournament tournament;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayers();
    }

    public void MovePlayers()
    {
        if (moveUp1 && player1.position.y < topRight.y - player1.localScale.y)
        {
            player1.Translate(player1.up * moveSpeed * Time.deltaTime);
        }
        
        if (player1.position.y >= topRight.y - player1.localScale.y)
        {
            moveUp1 = false;
        }
        
        if (!moveUp1 && player1.position.y > bottomLeft.y + player1.localScale.y)
        {
            player1.Translate(player1.up * -moveSpeed * Time.deltaTime);
        }
        
        if (player1.position.y <= bottomLeft.y + player1.localScale.y)
        {
            moveUp1 = true;
        }

        if (moveUp2 && player2.position.y < topRight.y - player2.localScale.y)
        {
            player2.Translate(player2.up * moveSpeed * Time.deltaTime);
        }
        
        if (player2.position.y >= topRight.y - player2.localScale.y)
        {
            moveUp2 = false;
        }
        
        if (!moveUp2 && player2.position.y > bottomLeft.y + player2.localScale.y)
        {
            player2.Translate(player2.up * -moveSpeed * Time.deltaTime);
        }
        
        if (player2.position.y <= bottomLeft.y + player2.localScale.y)
        {
            moveUp2 = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot(true);
        }

        if (mode)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Shoot(false);
            }
        }
        
    }

    IEnumerator SpawnItems()
    {
        while (true)
        {
            Vector3 pos = new Vector3(Random.Range(bottomPoint1.position.x, bottomPoint2.position.x), bottomPoint1.position.y, bottomPoint1.position.z);
            GameObject obj = Instantiate(items[Random.Range(0, items.Length - 1)], pos, Quaternion.identity);
            objects.Add(obj);
            obj.GetComponent<Rigidbody2D>().AddForce(obj.transform.up * Random.Range(630, 700));
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            
        }
    }

    public void Shoot(bool player)
    {
        if (player)
        {
            GameObject bul = Instantiate(bullet, spawnPoint1.position, Quaternion.identity);
            bul.GetComponent<Rigidbody2D>().AddForce(spawnPoint1.right * bulletSpeed);
            bul.tag = "Bullet1";
            Destroy(bul, 3);
        }
        else
        {
            GameObject bul = Instantiate(bullet, spawnPoint2.position, Quaternion.Euler(0, 0, 180));
            bul.GetComponent<Rigidbody2D>().AddForce(spawnPoint2.right * -bulletSpeed);
            bul.tag = "Bullet2";
            Destroy(bul, 3);
        }
    }

    public IEnumerator BackToMenu()
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
        redWin.SetActive(false);
        blueWin.SetActive(false);
        score1 = 0;
        score2 = 0;
        score1_txt.text = "0";
        score2_txt.text = "0";

        foreach (var item in objects)
        {
            Destroy(item);
        }

        foreach (var item in ai.objects)
        {
            Destroy(item);
        }

        objects.Clear();
        ai.objects.Clear();
    }

    public void Home()
    {
        menu.SetActive(true);
        gamePanel.SetActive(false);
        redWin.SetActive(false);
        blueWin.SetActive(false);
        score1 = 0;
        score2 = 0;
        score1_txt.text = "0";
        score2_txt.text = "0";

        foreach (var item in objects)
        {
            Destroy(item);
        }

        foreach (var item in ai.objects)
        {
            Destroy(item);
        }

        objects.Clear();
        ai.objects.Clear();
    }

    public void Win()
    {
        StartCoroutine(BackToMenu());
    }

    public void NewStart()
    {
        moveUp1 = false;
        moveUp2 = true;
        StartCoroutine(SpawnItems());
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

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

    public void ChooseMode(bool twoPlayers)
    {
        if (twoPlayers)
        {
            mode = true;
            player2.gameObject.GetComponent<ShooterAI>().enabled = false;
            player2_button.SetActive(true);
        }
        else
        {
            mode = false;
            player2_button.SetActive(false);
            player2.gameObject.GetComponent<ShooterAI>().enabled = true;
        }
    }

    public void NewAIStart()
    {
        StartCoroutine(ai.AIShoot());
    }
}
