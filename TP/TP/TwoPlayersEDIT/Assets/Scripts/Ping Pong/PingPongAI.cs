using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform ball;
    private bool moveToBall;

    // Start is called before the first frame update
    public IEnumerator StartAI()
    {
        moveToBall = true;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 3.5f));
            moveToBall = !moveToBall;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToBall)
        {
            player.position = Vector3.Lerp(player.position, new Vector3(player.position.x, ball.position.y, player.position.z), 3 * Time.deltaTime);
        }
        
    }
}
