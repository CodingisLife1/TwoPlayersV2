using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score1_txt;
    [SerializeField] private TextMeshProUGUI score2_txt;
    private int score1;
    private int score2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlusPoint(bool red)
    {
        if (red)
        {
            score1++;
            score1_txt.text = score1.ToString();
        }
        else
        {
            score2++;
            score2_txt.text = score2.ToString();
        }
    }
}
