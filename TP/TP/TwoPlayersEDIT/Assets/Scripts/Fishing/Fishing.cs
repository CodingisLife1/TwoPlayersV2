using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    [Header("Players")]
    [SerializeField] private LineRenderer hook1;
    [SerializeField] private LineRenderer hook2;
    [SerializeField] private Transform hookStartPos1;
    [SerializeField] private Transform hookStartPos2;
    [SerializeField] private Transform hookStartPoint1;
    [SerializeField] private Transform hookStartPoint2;
    [SerializeField] private Transform hookEndPoint1;
    [SerializeField] private Transform hookEndPoint2;
    
    public TextMeshProUGUI score1_txt;
    public TextMeshProUGUI score2_txt;
    public int score1;
    public int score2;
    public bool isCatched1;
    public bool isCatched2;

    public GameObject redWin;
    public GameObject blueWin;
    public GameObject menu;
    public TotalScore totalScore;
    public GameObject gamePanel;

    public Tournament tournament;

    public bool mode;
    private bool canHook1;
    private bool canHook2;

    [Header("Fish")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] fish;


    private List<GameObject> objects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hook1.SetPosition(0, hookStartPoint1.position);
        hook2.SetPosition(0, hookStartPoint2.position);
        hook1.SetPosition(1, hookEndPoint1.position);
        hook2.SetPosition(1, hookEndPoint2.position);

        if (canHook1 && hookEndPoint1.position.y > -3.3f && !isCatched1)
        {
            hookEndPoint1.position = new Vector3(hookEndPoint1.position.x, hookEndPoint1.position.y - 0.1f, hookEndPoint1.position.z);
        }
        else if (!canHook1 && hookEndPoint1.position.y < hookStartPos1.position.y)
        {
            hookEndPoint1.position = new Vector3(hookEndPoint1.position.x, hookEndPoint1.position.y + 0.1f, hookEndPoint1.position.z);
        }

        if (mode)
        {
            if (canHook2 && hookEndPoint2.position.y > -3.3f && !isCatched2)
            {
                hookEndPoint2.position = new Vector3(hookEndPoint2.position.x, hookEndPoint2.position.y - 0.1f, hookEndPoint2.position.z);
            }
            else if (!canHook2 && hookEndPoint2.position.y < hookStartPos2.position.y)
            {
                hookEndPoint2.position = new Vector3(hookEndPoint2.position.x, hookEndPoint2.position.y + 0.1f, hookEndPoint2.position.z);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                Hook(false);
            }

            if (Input.GetKeyUp(KeyCode.M))
            {
                FinishHook(false);
            }
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            Hook(true);
        }       

        if (Input.GetKeyUp(KeyCode.Z))
        {
            FinishHook(true);
        }       
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            int rand = Random.Range(0, spawnPoints.Length);
            GameObject f = Instantiate(fish[Random.Range(0, fish.Length)], spawnPoints[rand].position, Quaternion.identity);
            if (rand >= 3)
            {
                f.GetComponent<Fish>().moveSpeed *= -1;
            }

            if (rand < 3)
            {
                f.transform.localScale = new Vector3(f.transform.localScale.x * -1, f.transform.localScale.y, f.transform.localScale.z);
            }

            objects.Add(f);

            yield return new WaitForSeconds(1f);
        }
        
    }

    public void Hook(bool player)
    {
        if (player)
        {
            
            canHook1 = true;
            
        }
        else
        {
            canHook2 = true;
        }
    }

    public void FinishHook(bool player)
    {
        if (player)
        {
            canHook1 = false;
        }
        else
        {
            canHook2 = false;
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
        StopAllCoroutines();

        foreach (var item in objects)
        {
            Destroy(item);
        }

        objects.Clear();
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
        StopAllCoroutines();

        foreach (var item in objects)
        {
            Destroy(item);
        }

        objects.Clear();
    }

    public void Win()
    {
        StartCoroutine(BackToMenu());
    }

    public void NewStart()
    {
        StartCoroutine(Spawn());
        if (!mode)
        {
            StartCoroutine(FishingAI());
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

    public void ChooseMode(bool twoPlayers)
    {
        if (twoPlayers)
        {
            mode = true;
        }
        else
        {
            mode = false;
        }
    }

    IEnumerator FishingAI()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 2));

        float t = Random.Range(0.5f, 2);

        while (true)
        {
            if (hookEndPoint2.position.y > -3.3f && !isCatched2)
            {
                hookEndPoint2.position = new Vector3(hookEndPoint2.position.x, hookEndPoint2.position.y - 0.1f, hookEndPoint2.position.z);
                yield return new WaitForSeconds(0.01f);
                t -= 0.01f;
                if (t <= 0)
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        yield return new WaitForSeconds(Random.Range(0.5f, 1));

        while (hookEndPoint2.position.y < hookStartPos2.position.y)
        {
            hookEndPoint2.position = new Vector3(hookEndPoint2.position.x, hookEndPoint2.position.y + 0.1f, hookEndPoint2.position.z);
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(FishingAI());
        
    }
}
