using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAI : MonoBehaviour
{
    public Vector2 dir;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    public IEnumerator AIStart()
    {
        while (true)
        {
            
            dir = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));

            if (dir.x == 0 && dir.y == 0)
            {
                dir = new Vector2(0, 1);
            }

            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir = new Vector2(dir.x * -1, dir.y * -1);
    }

}
