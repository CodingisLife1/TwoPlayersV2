using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
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
            circleRacing.checkpoint1 = true;
        }
        else
        {
            circleRacing.checkpoint2 = true;
        }
    }
}
