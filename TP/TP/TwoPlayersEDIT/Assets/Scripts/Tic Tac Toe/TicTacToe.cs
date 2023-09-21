using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    [SerializeField] private Init initSDK;
    [SerializeField] private GameObject crossSymbol;
    [SerializeField] private GameObject zeroSymbol;
    [SerializeField] private Transform[] symbolPositions;
    [SerializeField] private TextMeshProUGUI turn_txt;
    [SerializeField] private GameObject[] buttons;

    [SerializeField] private GameObject redWin;
    [SerializeField] private GameObject blueWin;
    [SerializeField] private GameObject drawPanel;
    [SerializeField] private GameObject menu;
    [SerializeField] private TotalScore totalScore;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Tournament tournament;
    [SerializeField] private GameObject parentSymbols;
    private List<GameObject> existSymbols = new List<GameObject>();

    private bool mode;
    private bool playerTurn; // true = X, false = O
    private bool checkWin;
    private char[,] symbols = new char[3, 3];
    

    // Start is called before the first frame update
    void Start()
    {
        playerTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode)
        {
            if (playerTurn)
            {
                turn_txt.color = Color.red;
            }
            else
            {
                turn_txt.color = Color.blue;
            }
        }
        else
        {
            if (playerTurn)
            {
                turn_txt.color = Color.red;
            }
            else
            {
                turn_txt.color = Color.blue;
                AITurn();
            }

        }

        
        
    }

    public void Button00()
    {
            if (symbols[0, 0] != 'X' && symbols[0, 0] != 'O' && checkWin == false)
            {
                if (playerTurn)
                {
                    symbols[0, 0] = 'X';
                    GameObject g = Instantiate(crossSymbol, new Vector3(symbolPositions[0].position.x, symbolPositions[0].position.y, symbolPositions[0].position.z), Quaternion.identity);
                    existSymbols.Add(g);
                    g.transform.parent = parentSymbols.transform;
                    playerTurn = false; // î÷åðåäü ñëåäóþùåãî èãðîêà
                }
                else
                {
                    symbols[0, 0] = 'O';
                    GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[0].position.x, symbolPositions[0].position.y, symbolPositions[0].position.z), Quaternion.identity);
                    existSymbols.Add(g);
                    g.transform.parent = parentSymbols.transform;
                    playerTurn = true; // î÷åðåäü ñëåäóþùåãî èãðîêà
                }

                // Ïðîâåðêà íà ïîáåäó êðåñòèêà
                if ((symbols[0, 0] == 'X' && symbols[0, 1] == 'X' && symbols[0, 2] == 'X') || (symbols[0, 0] == 'X' && symbols[1, 0] == 'X' && symbols[2, 0] == 'X') || (symbols[0, 0] == 'X' && symbols[1, 1] == 'X' && symbols[2, 2] == 'X'))
                {
                    checkWin = true;
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

                // Ïðîâåðêà íà ïîáåäó íîëèêà
                if ((symbols[0, 0] == 'O' && symbols[0, 1] == 'O' && symbols[0, 2] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 0] == 'O' && symbols[2, 0] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 1] == 'O' && symbols[2, 2] == 'O'))
                {
                    checkWin = true;
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

        if (existSymbols.Count == 9 && checkWin == false)
        {
            drawPanel.SetActive(true);
            StartCoroutine(BackToMenu());
        }

    }

    public void Button01()
    {
        if (symbols[0, 1] != 'X' && symbols[0, 1] != 'O' && checkWin == false)
        {
            if (playerTurn)
            {
                symbols[0, 1] = 'X';
                GameObject g = Instantiate(crossSymbol, new Vector3(symbolPositions[1].position.x, symbolPositions[1].position.y, symbolPositions[1].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = false; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }
            else
            {
                symbols[0, 1] = 'O';
                GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[1].position.x, symbolPositions[1].position.y, symbolPositions[1].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = true; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }

            // Ïðîâåðêà íà ïîáåäó êðåñòèêà
            if ((symbols[0, 0] == 'X' && symbols[0, 1] == 'X' && symbols[0, 2] == 'X') || (symbols[0, 1] == 'X' && symbols[1, 1] == 'X' && symbols[2, 1] == 'X'))
            {
                checkWin = true;
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

            // Ïðîâåðêà íà ïîáåäó íîëèêà
            if ((symbols[0, 0] == 'O' && symbols[0, 1] == 'O' && symbols[0, 2] == 'O') || (symbols[0, 1] == 'O' && symbols[1, 1] == 'O' && symbols[2, 1] == 'O'))
            {
                checkWin = true;
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

        if (existSymbols.Count == 9 && checkWin == false)
        {
            drawPanel.SetActive(true);
            StartCoroutine(BackToMenu());
        }

    }

    public void Button02()
    {
        if (symbols[0, 2] != 'X' && symbols[0, 2] != 'O' && checkWin == false)
        {
            if (playerTurn)
            {
                symbols[0, 2] = 'X';
                GameObject g = Instantiate(crossSymbol, new Vector3(symbolPositions[2].position.x, symbolPositions[2].position.y, symbolPositions[2].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = false; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }
            else
            {
                symbols[0, 2] = 'O';
                GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[2].position.x, symbolPositions[2].position.y, symbolPositions[2].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = true; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }

            // Ïðîâåðêà íà ïîáåäó êðåñòèêà
            if ((symbols[0, 0] == 'X' && symbols[0, 1] == 'X' && symbols[0, 2] == 'X') || (symbols[0, 2] == 'X' && symbols[1, 2] == 'X' && symbols[2, 2] == 'X') || (symbols[0, 2] == 'X' && symbols[1, 1] == 'X' && symbols[2, 0] == 'X'))
            {
                checkWin = true;
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

            // Ïðîâåðêà íà ïîáåäó íîëèêà
            if ((symbols[0, 0] == 'O' && symbols[0, 1] == 'O' && symbols[0, 2] == 'O') || (symbols[0, 2] == 'O' && symbols[1, 2] == 'O' && symbols[2, 2] == 'O') || (symbols[0, 2] == 'O' && symbols[1, 1] == 'O' && symbols[2, 0] == 'O'))
            {
                checkWin = true;
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

        if (existSymbols.Count == 9 && checkWin == false)
        {
            drawPanel.SetActive(true);
            StartCoroutine(BackToMenu());
        }

    }

    public void Button10()
    {
        if (symbols[1, 0] != 'X' && symbols[1, 0] != 'O' && checkWin == false)
        {
            if (playerTurn)
            {
                symbols[1, 0] = 'X';
                GameObject g = Instantiate(crossSymbol, new Vector3(symbolPositions[3].position.x, symbolPositions[3].position.y, symbolPositions[3].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = false; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }
            else
            {
                symbols[1, 0] = 'O';
                GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[3].position.x, symbolPositions[3].position.y, symbolPositions[3].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = true; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }

            // Ïðîâåðêà íà ïîáåäó êðåñòèêà
            if ((symbols[1, 0] == 'X' && symbols[1, 1] == 'X' && symbols[1, 2] == 'X') || (symbols[0, 0] == 'X' && symbols[1, 0] == 'X' && symbols[2, 0] == 'X'))
            {
                checkWin = true;
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

            // Ïðîâåðêà íà ïîáåäó íîëèêà
            if ((symbols[1, 0] == 'O' && symbols[1, 1] == 'O' && symbols[1, 2] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 0] == 'O' && symbols[2, 0] == 'O'))
            {
                checkWin = true;
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

        if (existSymbols.Count == 9 && checkWin == false)
        {
            drawPanel.SetActive(true);
            StartCoroutine(BackToMenu());
        }

    }

    public void Button11()
    {
        if (symbols[1, 1] != 'X' && symbols[1, 1] != 'O' && checkWin == false)
        {
            if (playerTurn)
            {
                symbols[1, 1] = 'X';
                GameObject g = Instantiate(crossSymbol, new Vector3(symbolPositions[4].position.x, symbolPositions[4].position.y, symbolPositions[4].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = false; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }
            else
            {
                symbols[1, 1] = 'O';
                GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[4].position.x, symbolPositions[4].position.y, symbolPositions[4].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = true; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }

            // Ïðîâåðêà íà ïîáåäó êðåñòèêà
            if ((symbols[1, 0] == 'X' && symbols[1, 1] == 'X' && symbols[1, 2] == 'X') || (symbols[0, 1] == 'X' && symbols[1, 1] == 'X' && symbols[2, 1] == 'X') || (symbols[0, 0] == 'X' && symbols[1, 1] == 'X' && symbols[2, 2] == 'X') || (symbols[0, 2] == 'X' && symbols[1, 1] == 'X' && symbols[2, 0] == 'X'))
            {
                checkWin = true;
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

            // Ïðîâåðêà íà ïîáåäó íîëèêà
            if ((symbols[1, 0] == 'O' && symbols[1, 1] == 'O' && symbols[1, 2] == 'O') || (symbols[0, 1] == 'O' && symbols[1, 1] == 'O' && symbols[2, 1] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 1] == 'O' && symbols[2, 2] == 'O') || (symbols[0, 2] == 'O' && symbols[1, 1] == 'O' && symbols[2, 0] == 'O'))
            {
                checkWin = true;
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

        if (existSymbols.Count == 9 && checkWin == false)
        {
            drawPanel.SetActive(true);
            StartCoroutine(BackToMenu());
        }

    }

    public void Button12()
    {
        if (symbols[1, 2] != 'X' && symbols[1, 2] != 'O' && checkWin == false)
        {
            if (playerTurn)
            {
                symbols[1, 2] = 'X';
                GameObject g = Instantiate(crossSymbol, new Vector3(symbolPositions[5].position.x, symbolPositions[5].position.y, symbolPositions[5].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = false; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }
            else
            {
                symbols[1, 2] = 'O';
                GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[5].position.x, symbolPositions[5].position.y, symbolPositions[5].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = true; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }

            // Ïðîâåðêà íà ïîáåäó êðåñòèêà
            if ((symbols[0, 2] == 'X' && symbols[1, 2] == 'X' && symbols[2, 2] == 'X') || (symbols[1, 0] == 'X' && symbols[1, 1] == 'X' && symbols[1, 2] == 'X'))
            {
                checkWin = true;
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

            // Ïðîâåðêà íà ïîáåäó íîëèêà
            if ((symbols[0, 2] == 'O' && symbols[1, 2] == 'O' && symbols[2, 2] == 'O') || (symbols[1, 0] == 'O' && symbols[1, 1] == 'O' && symbols[1, 2] == 'O'))
            {
                checkWin = true;
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

        if (existSymbols.Count == 9 && checkWin == false)
        {
            drawPanel.SetActive(true);
            StartCoroutine(BackToMenu());
        }

    }

    public void Button20()
    {
        if (symbols[2, 0] != 'X' && symbols[2, 0] != 'O' && checkWin == false)
        {
            if (playerTurn)
            {
                symbols[2, 0] = 'X';
                GameObject g = Instantiate(crossSymbol, new Vector3(symbolPositions[6].position.x, symbolPositions[6].position.y, symbolPositions[6].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = false; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }
            else
            {
                symbols[2, 0] = 'O';
                GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[6].position.x, symbolPositions[6].position.y, symbolPositions[6].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = true; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }

            // Ïðîâåðêà íà ïîáåäó êðåñòèêà
            if ((symbols[0, 0] == 'X' && symbols[1, 0] == 'X' && symbols[2, 0] == 'X') || (symbols[2, 0] == 'X' && symbols[2, 1] == 'X' && symbols[2, 2] == 'X') || (symbols[2, 0] == 'X' && symbols[1, 1] == 'X' && symbols[0, 2] == 'X'))
            {
                checkWin = true;
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

            // Ïðîâåðêà íà ïîáåäó íîëèêà
            if ((symbols[0, 0] == 'O' && symbols[1, 0] == 'O' && symbols[2, 0] == 'O') || (symbols[2, 0] == 'O' && symbols[2, 1] == 'O' && symbols[2, 2] == 'O') || (symbols[2, 0] == 'O' && symbols[1, 1] == 'O' && symbols[0, 2] == 'O'))
            {
                checkWin = true;
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

        if (existSymbols.Count == 9 && checkWin == false)
        {
            drawPanel.SetActive(true);
            StartCoroutine(BackToMenu());
        }

    }

    public void Button21()
    {
        if (symbols[2, 1] != 'X' && symbols[2, 1] != 'O' && checkWin == false)
        {
            if (playerTurn)
            {
                symbols[2, 1] = 'X';
                GameObject g = Instantiate(crossSymbol, new Vector3(symbolPositions[7].position.x, symbolPositions[7].position.y, symbolPositions[7].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = false; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }
            else
            {
                symbols[2, 1] = 'O';
                GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[7].position.x, symbolPositions[7].position.y, symbolPositions[7].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = true; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }

            // Ïðîâåðêà íà ïîáåäó êðåñòèêà
            if ((symbols[0, 1] == 'X' && symbols[1, 1] == 'X' && symbols[2, 1] == 'X') || (symbols[2, 0] == 'X' && symbols[2, 1] == 'X' && symbols[2, 2] == 'X'))
            {
                checkWin = true;
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

            // Ïðîâåðêà íà ïîáåäó íîëèêà
            if ((symbols[0, 1] == 'O' && symbols[1, 1] == 'O' && symbols[2, 1] == 'O') || (symbols[2, 0] == 'O' && symbols[2, 1] == 'O' && symbols[2, 2] == 'O'))
            {
                checkWin = true;
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

        if (existSymbols.Count == 9 && checkWin == false)
        {
            drawPanel.SetActive(true);
            StartCoroutine(BackToMenu());
        }

    }

    public void Button22()
    {
        if (symbols[2, 2] != 'X' && symbols[2, 2] != 'O' && checkWin == false)
        {
            if (playerTurn)
            {
                symbols[2, 2] = 'X';
                GameObject g = Instantiate(crossSymbol, new Vector3(symbolPositions[8].position.x, symbolPositions[8].position.y, symbolPositions[8].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = false; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }
            else
            {
                symbols[2, 2] = 'O';
                GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[8].position.x, symbolPositions[8].position.y, symbolPositions[8].position.z), Quaternion.identity);
                existSymbols.Add(g);
                g.transform.parent = parentSymbols.transform;
                playerTurn = true; // î÷åðåäü ñëåäóþùåãî èãðîêà
            }

            // Ïðîâåðêà íà ïîáåäó êðåñòèêà
            if ((symbols[0, 2] == 'X' && symbols[1, 2] == 'X' && symbols[2, 2] == 'X') || (symbols[2, 0] == 'X' && symbols[2, 1] == 'X' && symbols[2, 2] == 'X') || (symbols[0, 0] == 'X' && symbols[1, 1] == 'X' && symbols[2, 2] == 'X'))
            {
                checkWin = true;
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

            // Ïðîâåðêà íà ïîáåäó íîëèêà
            if ((symbols[0, 2] == 'O' && symbols[1, 2] == 'O' && symbols[2, 2] == 'O') || (symbols[2, 0] == 'O' && symbols[2, 1] == 'O' && symbols[2, 2] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 1] == 'O' && symbols[2, 2] == 'O'))
            {
                checkWin = true;
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

        if (existSymbols.Count == 9 && checkWin == false)
        {
            drawPanel.SetActive(true);
            StartCoroutine(BackToMenu());
        }

    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2);
        menu.SetActive(true);
        gamePanel.SetActive(false);
        redWin.SetActive(false);
        blueWin.SetActive(false);
        drawPanel.SetActive(false);
        playerTurn = false;
        checkWin = false;

        symbols[0, 0] = ' ';
        symbols[0, 1] = ' ';
        symbols[0, 2] = ' ';
        symbols[1, 0] = ' ';
        symbols[1, 1] = ' ';
        symbols[1, 2] = ' ';
        symbols[2, 0] = ' ';
        symbols[2, 1] = ' ';
        symbols[2, 2] = ' ';
        foreach (var item in existSymbols)
        {
            Destroy(item);
        }

        existSymbols.Clear();
    }

    public void Home()
    {
        menu.SetActive(true);
        gamePanel.SetActive(false);
        redWin.SetActive(false);
        blueWin.SetActive(false);
        drawPanel.SetActive(false);
        playerTurn = false;
        checkWin = false;

        symbols[0, 0] = ' ';
        symbols[0, 1] = ' ';
        symbols[0, 2] = ' ';
        symbols[1, 0] = ' ';
        symbols[1, 1] = ' ';
        symbols[1, 2] = ' ';
        symbols[2, 0] = ' ';
        symbols[2, 1] = ' ';
        symbols[2, 2] = ' ';
        foreach (var item in existSymbols)
        {
            Destroy(item);
        }

        existSymbols.Clear();
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

        if (!initSDK.mobile)
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

    public void AITurn()
    {
        if (!checkWin)
        {
            int chooseRow = Random.Range(0, 3);
            int chooseColumn = Random.Range(0, 3);

            while (symbols[chooseRow, chooseColumn] == 'X' || symbols[chooseRow, chooseColumn] == 'O')
            {
                chooseRow = Random.Range(0, 3);
                chooseColumn = Random.Range(0, 3);
            }

            int numCell = 0;

            if (chooseRow == 0 && chooseColumn == 0)
            {
                numCell = 0;
            }
            else if (chooseRow == 0 && chooseColumn == 1)
            {
                numCell = 1;
            }
            else if (chooseRow == 0 && chooseColumn == 2)
            {
                numCell = 2;
            }
            else if (chooseRow == 1 && chooseColumn == 0)
            {
                numCell = 3;
            }
            else if (chooseRow == 1 && chooseColumn == 1)
            {
                numCell = 4;
            }
            else if (chooseRow == 1 && chooseColumn == 2)
            {
                numCell = 5;
            }
            else if (chooseRow == 2 && chooseColumn == 0)
            {
                numCell = 6;
            }
            else if (chooseRow == 2 && chooseColumn == 1)
            {
                numCell = 7;
            }
            else if (chooseRow == 2 && chooseColumn == 2)
            {
                numCell = 8;
            }

            symbols[chooseRow, chooseColumn] = 'O';

            GameObject g = Instantiate(zeroSymbol, new Vector3(symbolPositions[numCell].position.x, symbolPositions[numCell].position.y, symbolPositions[numCell].position.z), Quaternion.identity);
            existSymbols.Add(g);
            g.transform.parent = parentSymbols.transform;
            playerTurn = true;


            if (chooseRow == 0 && chooseColumn == 0)
            {
                if ((symbols[0, 0] == 'O' && symbols[0, 1] == 'O' && symbols[0, 2] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 0] == 'O' && symbols[2, 0] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 1] == 'O' && symbols[2, 2] == 'O'))
                {
                    checkWin = true;
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
            else if (chooseRow == 0 && chooseColumn == 1)
            {
                if ((symbols[0, 0] == 'O' && symbols[0, 1] == 'O' && symbols[0, 2] == 'O') || (symbols[0, 1] == 'O' && symbols[1, 1] == 'O' && symbols[2, 1] == 'O'))
                {
                    checkWin = true;
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
            else if (chooseRow == 0 && chooseColumn == 2)
            {
                if ((symbols[0, 0] == 'O' && symbols[0, 1] == 'O' && symbols[0, 2] == 'O') || (symbols[0, 2] == 'O' && symbols[1, 2] == 'O' && symbols[2, 2] == 'O') || (symbols[0, 2] == 'O' && symbols[1, 1] == 'O' && symbols[2, 0] == 'O'))
                {
                    checkWin = true;
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
            else if (chooseRow == 1 && chooseColumn == 0)
            {
                if ((symbols[1, 0] == 'O' && symbols[1, 1] == 'O' && symbols[1, 2] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 0] == 'O' && symbols[2, 0] == 'O'))
                {
                    checkWin = true;
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
            else if (chooseRow == 1 && chooseColumn == 1)
            {
                if ((symbols[1, 0] == 'O' && symbols[1, 1] == 'O' && symbols[1, 2] == 'O') || (symbols[0, 1] == 'O' && symbols[1, 1] == 'O' && symbols[2, 1] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 1] == 'O' && symbols[2, 2] == 'O') || (symbols[0, 2] == 'O' && symbols[1, 1] == 'O' && symbols[2, 0] == 'O'))
                {
                    checkWin = true;
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
            else if (chooseRow == 1 && chooseColumn == 2)
            {
                if ((symbols[0, 2] == 'O' && symbols[1, 2] == 'O' && symbols[2, 2] == 'O') || (symbols[1, 0] == 'O' && symbols[1, 1] == 'O' && symbols[1, 2] == 'O'))
                {
                    checkWin = true;
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
            else if (chooseRow == 2 && chooseColumn == 0)
            {
                if ((symbols[0, 0] == 'O' && symbols[1, 0] == 'O' && symbols[2, 0] == 'O') || (symbols[2, 0] == 'O' && symbols[2, 1] == 'O' && symbols[2, 2] == 'O') || (symbols[2, 0] == 'O' && symbols[1, 1] == 'O' && symbols[0, 2] == 'O'))
                {
                    checkWin = true;
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
            else if (chooseRow == 2 && chooseColumn == 1)
            {
                if ((symbols[0, 1] == 'O' && symbols[1, 1] == 'O' && symbols[2, 1] == 'O') || (symbols[2, 0] == 'O' && symbols[2, 1] == 'O' && symbols[2, 2] == 'O'))
                {
                    checkWin = true;
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
            else if (chooseRow == 2 && chooseColumn == 2)
            {
                if ((symbols[0, 2] == 'O' && symbols[1, 2] == 'O' && symbols[2, 2] == 'O') || (symbols[2, 0] == 'O' && symbols[2, 1] == 'O' && symbols[2, 2] == 'O') || (symbols[0, 0] == 'O' && symbols[1, 1] == 'O' && symbols[2, 2] == 'O'))
                {
                    checkWin = true;
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

            if (existSymbols.Count == 9 && checkWin == false)
            {
                drawPanel.SetActive(true);
                StartCoroutine(BackToMenu());
            }
        }
        

    }

}
