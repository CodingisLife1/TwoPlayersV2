using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tournament : MonoBehaviour
{
    [SerializeField] private int gameIndex;
    [SerializeField] private GameObject[] gamesForOne;
    [SerializeField] private GameObject[] games;
    [SerializeField] private List<int> list = new List<int>();
    [SerializeField] private int scoreRed;
    [SerializeField] private int scoreBlue;
    [SerializeField] private TextMeshProUGUI scoreRed_txt;
    [SerializeField] private TextMeshProUGUI scoreBlue_txt;
    [SerializeField] private GameObject[] back_buttons;
    public bool inTournament;
    public bool twoPlayers;
    [SerializeField] private Button OnePlayer;
    [SerializeField] private Button TwoPlayers;

    [SerializeField] private TankGame tankGame;
    [SerializeField] private PingPong pingPong;
    [SerializeField] private Racing racing;
    [SerializeField] private TicTacToe ttc;
    [SerializeField] private Shooter shooter;
    [SerializeField] private Fishing fishing;
    [SerializeField] private CRAdditional cr;
    [SerializeField] private RPS rps;
    [SerializeField] private ChooseMode airHockey;
    [SerializeField] private Painting painting;

    [SerializeField] private GameObject redWinPanel;
    [SerializeField] private GameObject blueWinPanel;

    [SerializeField] private Image[] progress;
    [SerializeField] private Color redColor;
    [SerializeField] private Color blueColor;

    [SerializeField] private GameObject menu;

    [SerializeField] private GameObject[] allJustPlay;
    [SerializeField] private GameObject[] allHomeButtons;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameIndex == 7)
        {
            EndTournament();
            gameIndex = 0;
        }
    }

    public void ChooseGameForOne()
    {
        inTournament = true;
        twoPlayers = false;
        OnePlayer.gameObject.SetActive(true);
        TwoPlayers.gameObject.SetActive(false);

        foreach (var item in back_buttons)
        {
            item.SetActive(false);
        }


        OnePlayer.onClick.RemoveAllListeners();

        int r = Random.Range(0, 8);

            for(int i = 0; i < 6; i++)
            {

                if (list.Count == 0)
                {
                    gamesForOne[r].SetActive(true);
                    list.Add(r);
                    break;
                }

                if (r != list[i] && i == list.Count - 1)
                {
                    gamesForOne[r].SetActive(true);
                    list.Add(r);
                    break;
                }

                if (r == list[i])
                {
                    r = Random.Range(0, 9);
                    i = -1;
                }       
            }

        if (r == 0)
        {
            OnePlayer.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            OnePlayer.onClick.AddListener(() => menu.SetActive(false));
            OnePlayer.onClick.AddListener(() => games[r].SetActive(true));
            OnePlayer.onClick.AddListener(() => tankGame.ChooseMode(false));
            OnePlayer.onClick.AddListener(() => OnePlayer.gameObject.SetActive(false));
        }
        else if (r == 1)
        {
            OnePlayer.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            OnePlayer.onClick.AddListener(() => menu.SetActive(false));
            OnePlayer.onClick.AddListener(() => games[r].SetActive(true));
            OnePlayer.onClick.AddListener(() => pingPong.ChooseMode(false));
            OnePlayer.onClick.AddListener(() => pingPong.StartNewGame());
            OnePlayer.onClick.AddListener(() => pingPong.NewAIStart());
            OnePlayer.onClick.AddListener(() => OnePlayer.gameObject.SetActive(false));
        }
        else if (r == 2)
        {
            OnePlayer.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            OnePlayer.onClick.AddListener(() => menu.SetActive(false));
            OnePlayer.onClick.AddListener(() => games[r].SetActive(true));
            OnePlayer.onClick.AddListener(() => racing.ChooseMode(false));
            OnePlayer.onClick.AddListener(() => OnePlayer.gameObject.SetActive(false));
        }
        else if (r == 3)
        {
            OnePlayer.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            OnePlayer.onClick.AddListener(() => menu.SetActive(false));
            OnePlayer.onClick.AddListener(() => games[r].SetActive(true));
            OnePlayer.onClick.AddListener(() => ttc.ChooseMode(false));
            OnePlayer.onClick.AddListener(() => OnePlayer.gameObject.SetActive(false));
        }
        else if (r == 4)
        {
            OnePlayer.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            OnePlayer.onClick.AddListener(() => menu.SetActive(false));
            OnePlayer.onClick.AddListener(() => games[r].SetActive(true));
            OnePlayer.onClick.AddListener(() => shooter.ChooseMode(false));
            OnePlayer.onClick.AddListener(() => shooter.NewStart());
            OnePlayer.onClick.AddListener(() => shooter.NewAIStart());
            OnePlayer.onClick.AddListener(() => OnePlayer.gameObject.SetActive(false));
        }
        else if (r == 5)
        {
            OnePlayer.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            OnePlayer.onClick.AddListener(() => menu.SetActive(false));
            OnePlayer.onClick.AddListener(() => games[r].SetActive(true));
            OnePlayer.onClick.AddListener(() => fishing.ChooseMode(false));
            OnePlayer.onClick.AddListener(() => fishing.NewStart());
            OnePlayer.onClick.AddListener(() => OnePlayer.gameObject.SetActive(false));
        }
        else if (r == 6)
        {
            OnePlayer.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            OnePlayer.onClick.AddListener(() => menu.SetActive(false));
            OnePlayer.onClick.AddListener(() => games[r].SetActive(true));
            OnePlayer.onClick.AddListener(() => rps.ChooseMode(false));
            OnePlayer.onClick.AddListener(() => OnePlayer.gameObject.SetActive(false));
        }
        else if (r == 7)
        {
            OnePlayer.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            OnePlayer.onClick.AddListener(() => menu.SetActive(false));
            OnePlayer.onClick.AddListener(() => games[r].SetActive(true));
            OnePlayer.onClick.AddListener(() => painting.ChooseMode(false));
            OnePlayer.onClick.AddListener(() => painting.NewStart());
            OnePlayer.onClick.AddListener(() => painting.NewAIStart());
            OnePlayer.onClick.AddListener(() => OnePlayer.gameObject.SetActive(false));
        }
        
    }

    public void ChooseGameForTwo()
    {
        inTournament = true;
        twoPlayers = true;

        OnePlayer.gameObject.SetActive(false);
        TwoPlayers.gameObject.SetActive(true);

        foreach (var item in back_buttons)
        {
            item.SetActive(false);
        }

        TwoPlayers.onClick.RemoveAllListeners();

        int r = Random.Range(0, 8);

        for (int i = 0; i < 6; i++)
        {
            if (list.Count == 0)
            {
                gamesForOne[r].SetActive(true);
                list.Add(r);
                break;
            }

            if (r != list[i] && i == list.Count - 1)
            {
                gamesForOne[r].SetActive(true);
                list.Add(r);
                break;
            }

            if (r == list[i])
            {
                r = Random.Range(0, 9);
                i = -1;
            }
        }

        if (r == 0)
        {
            TwoPlayers.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            TwoPlayers.onClick.AddListener(() => menu.SetActive(false));
            TwoPlayers.onClick.AddListener(() => games[r].SetActive(true));
            TwoPlayers.onClick.AddListener(() => tankGame.ChooseMode(true));
            TwoPlayers.onClick.AddListener(() => TwoPlayers.gameObject.SetActive(false));
        }
        else if (r == 1)
        {
            TwoPlayers.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            TwoPlayers.onClick.AddListener(() => menu.SetActive(false));
            TwoPlayers.onClick.AddListener(() => games[r].SetActive(true));
            TwoPlayers.onClick.AddListener(() => pingPong.ChooseMode(true));
            TwoPlayers.onClick.AddListener(() => pingPong.StartNewGame());
            TwoPlayers.onClick.AddListener(() => TwoPlayers.gameObject.SetActive(false));
        }
        else if (r == 2)
        {
            TwoPlayers.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            TwoPlayers.onClick.AddListener(() => menu.SetActive(false));
            TwoPlayers.onClick.AddListener(() => games[r].SetActive(true));
            TwoPlayers.onClick.AddListener(() => racing.ChooseMode(true));
            TwoPlayers.onClick.AddListener(() => TwoPlayers.gameObject.SetActive(false));
        }
        else if (r == 3)
        {
            TwoPlayers.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            TwoPlayers.onClick.AddListener(() => menu.SetActive(false));
            TwoPlayers.onClick.AddListener(() => games[r].SetActive(true));
            TwoPlayers.onClick.AddListener(() => ttc.ChooseMode(true));
            TwoPlayers.onClick.AddListener(() => TwoPlayers.gameObject.SetActive(false));
        }
        else if (r == 4)
        {
            TwoPlayers.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            TwoPlayers.onClick.AddListener(() => menu.SetActive(false));
            TwoPlayers.onClick.AddListener(() => games[r].SetActive(true));
            TwoPlayers.onClick.AddListener(() => shooter.ChooseMode(true));
            TwoPlayers.onClick.AddListener(() => shooter.NewStart());
            TwoPlayers.onClick.AddListener(() => TwoPlayers.gameObject.SetActive(false));
        }
        else if (r == 5)
        {
            TwoPlayers.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            TwoPlayers.onClick.AddListener(() => menu.SetActive(false));
            TwoPlayers.onClick.AddListener(() => games[r].SetActive(true));
            TwoPlayers.onClick.AddListener(() => fishing.ChooseMode(true));
            TwoPlayers.onClick.AddListener(() => fishing.NewStart());
            TwoPlayers.onClick.AddListener(() => TwoPlayers.gameObject.SetActive(false));
        }
        else if (r == 6)
        {
            TwoPlayers.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            TwoPlayers.onClick.AddListener(() => menu.SetActive(false));
            TwoPlayers.onClick.AddListener(() => games[r].SetActive(true));
            TwoPlayers.onClick.AddListener(() => rps.ChooseMode(true));
            TwoPlayers.onClick.AddListener(() => TwoPlayers.gameObject.SetActive(false));
        }
        else if (r == 7)
        {
            TwoPlayers.onClick.AddListener(() => gamesForOne[r].SetActive(false));
            TwoPlayers.onClick.AddListener(() => menu.SetActive(false));
            TwoPlayers.onClick.AddListener(() => games[r].SetActive(true));
            TwoPlayers.onClick.AddListener(() => painting.ChooseMode(true));
            TwoPlayers.onClick.AddListener(() => painting.NewStart());
            TwoPlayers.onClick.AddListener(() => TwoPlayers.gameObject.SetActive(false));
        }
    }


    public void EndTournament()
    {
        if (scoreRed > scoreBlue)
        {
            StartCoroutine(WinPanel(true));
        }
        else
        {
            StartCoroutine(WinPanel(false));
        }

        CloseTournament();
    }

    public void CloseTournament()
    {
        list.Clear();
        scoreRed = 0;
        scoreRed_txt.text = "0";
        scoreBlue_txt.text = "0";
        scoreBlue = 0;
        inTournament = false;
        gameIndex = 0;
        for (int i = 0; i < progress.Length; i++)
        {
            progress[i].color = Color.white;
        }

        foreach (var item in back_buttons)
        {
            item.SetActive(true);
        }

        OnePlayer.gameObject.SetActive(false);
        TwoPlayers.gameObject.SetActive(false);

        for (int i = 0; i < allJustPlay.Length; i++)
        {
        	allJustPlay[i].SetActive(true);
        }

        for (int i = 0; i < allHomeButtons.Length; i++)
        {
            allHomeButtons[i].SetActive(true);
        }
    }

    public void PlusScore(bool red)
    {
        
        if (red)
        {
            scoreRed++;
            scoreRed_txt.text = scoreRed.ToString();
            progress[gameIndex].color = redColor;
        }
        else
        {
            scoreBlue++;
            scoreBlue_txt.text = scoreBlue.ToString();
            progress[gameIndex].color = blueColor;
        }
        gameIndex++;

    }

    IEnumerator WinPanel(bool red)
    {
        if (red)
        {
            redWinPanel.SetActive(true);
            yield return new WaitForSeconds(3);
            redWinPanel.SetActive(false);
        }
        else
        {
            blueWinPanel.SetActive(true);
            yield return new WaitForSeconds(3);
            blueWinPanel.SetActive(false);
        }
    }


    public void OffHomeButtons()
    {
        for (int i = 0; i < allHomeButtons.Length; i++)
        {
            allHomeButtons[i].SetActive(false);
        }
    }
}
