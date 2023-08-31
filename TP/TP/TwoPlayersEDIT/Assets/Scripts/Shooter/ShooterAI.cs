using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform spawnPoint2; 
    public List<GameObject> objects = new List<GameObject>();
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AIShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 2.5f));

            GameObject bul = Instantiate(bullet, spawnPoint2.position, Quaternion.Euler(0, 0, 180));
            objects.Add(bul);
            bul.GetComponent<Rigidbody2D>().AddForce(spawnPoint2.right * -bulletSpeed);
            bul.tag = "Bullet2";
        }
            
    }


}
