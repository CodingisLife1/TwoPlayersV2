using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlayerPosition : MonoBehaviour
{
	[SerializeField] private Camera cam;
	[SerializeField] private bool left;
    // Start is called before the first frame update
    void Start()
    {
        var leftBottom = (Vector2)cam.ScreenToWorldPoint(new Vector3(0,0,cam.nearClipPlane));
        var leftTop = (Vector2)cam.ScreenToWorldPoint(new Vector3(0,cam.pixelHeight,cam.nearClipPlane));
        var rightBottom = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight,cam.nearClipPlane));
        var rightTop = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,0,cam.nearClipPlane));

        if (left)
        {
        	transform.position = new Vector3(leftBottom.x + 1f, 0, 0);
        }
        else
        {
        	transform.position = new Vector3(rightBottom.x - 1f, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
