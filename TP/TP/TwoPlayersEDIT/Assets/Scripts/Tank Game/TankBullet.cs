using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    public Vector3 direction;
    [SerializeField] private TankGame tankGame;
    [SerializeField] private GameObject explosionAnim;

    // Start is called before the first frame update
    void Start()
    {
        tankGame = GameObject.FindGameObjectWithTag("Tank Game").GetComponent<TankGame>();
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tank1"))
        {
            tankGame.PlusPoint(true);
            GameObject exp = Instantiate(explosionAnim, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(exp, 0.25f);
        }
        else if (collision.CompareTag("Tank2"))
        {
            tankGame.PlusPoint(false);
            GameObject exp = Instantiate(explosionAnim, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(exp, 0.25f);
        }

        if (collision.CompareTag("Box"))
        {
            GameObject exp = Instantiate(explosionAnim, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(exp, 0.25f);
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
