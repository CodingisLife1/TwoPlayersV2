using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    bool wasJustClicked = true;
    bool canMove;
    Vector2 playerSize;
    [SerializeField] private Init init;
    [SerializeField] private bool red;
    [SerializeField] private Collider2D coll;

    Rigidbody2D rb;

    public Transform BoundaryHolder;

    Boundary playerBoundary;

	// Use this for initialization
	void Start () {
        playerSize = GetComponent<SpriteRenderer>().bounds.extents;
        rb = GetComponent<Rigidbody2D>();

        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);
        if (init.mobile)
        {
            coll.sharedMaterial.bounciness = 1;
        }
        else
        {
            coll.sharedMaterial.bounciness = 1;
            //coll.sharedMaterial.bounciness = 3;
        }
        

    }
	
	// Update is called once per frame
	void Update () {
        if (init.mobile)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (wasJustClicked)
                {
                    wasJustClicked = false;

                    if ((mousePos.x >= transform.position.x && mousePos.x < transform.position.x + playerSize.x ||
                    mousePos.x <= transform.position.x && mousePos.x > transform.position.x - playerSize.x) &&
                    (mousePos.y >= transform.position.y && mousePos.y < transform.position.y + playerSize.y ||
                    mousePos.y <= transform.position.y && mousePos.y > transform.position.y - playerSize.y))
                    {
                        canMove = true;
                    }
                    else
                    {
                        canMove = false;
                    }
                }

                if (canMove)
                {
                    Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left,
                                                                      playerBoundary.Right),
                                                          Mathf.Clamp(mousePos.y, playerBoundary.Down,
                                                                      playerBoundary.Up));
                    rb.MovePosition(clampedMousePos);
                }
            }
            else
            {
                wasJustClicked = true;
            }
        }
        else
        {
            float x = 0;
            float y = 0;
            if (red)
            {
                x = Input.GetAxis("HorizontalRed");
                y = Input.GetAxis("VerticalRed");
            }
            else
            {
                x = Input.GetAxis("HorizontalBlue");
                y = Input.GetAxis("VerticalBlue");
            }

            rb.MovePosition(new Vector3(transform.position.x + x / 2.5f, transform.position.y + y / 2.5f, 0));
        }
		
	}
}