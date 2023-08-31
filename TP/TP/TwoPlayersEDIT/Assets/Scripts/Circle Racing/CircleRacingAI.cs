using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRacingAI : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
	[SerializeField] private Finish finish;
    [SerializeField] private Transform[] points;
    private int nextPosindex;
    [SerializeField] private Transform nextPos;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        rb.velocity = Vector2.zero;
    }

    public void Move()
    {
        if (Vector2.Distance(transform.position, nextPos.position) < 0.1f)
        {
            nextPosindex++;
            if (nextPosindex >= points.Length)
            {
                nextPosindex = 0;
            }

            TakeTurn();
            nextPos = points[nextPosindex];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
        }
    }

    public void TakeTurn()
    {
        Vector3 curRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        curRotation.z += nextPos.eulerAngles.z;
        transform.eulerAngles = curRotation;
    }

    void OnTrifferEnter2D(Collider2D other)
    {
    	if (other.CompareTag("Finish"))
    	{
    		Debug.Log("BOT");
    		other.GetComponent<Finish>().BotFinish();
    	}
    }
}
