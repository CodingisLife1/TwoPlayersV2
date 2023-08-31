using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    [SerializeField] private Color newColor;
    [SerializeField] private Painting painting;
    [SerializeField] private bool red;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Box"))
        {

            Color collisionColor = collision.GetComponent<SpriteRenderer>().color;

            if (collisionColor == newColor)
            {
                
                return;
            }
            else if (collisionColor != newColor && collisionColor != Color.white)
            {
                if (red)
                {
                    painting.scoreRed += 3;
                    painting.scoreBlue -= 3;
                }
                else
                {
                    painting.scoreRed -= 3;
                    painting.scoreBlue += 3;
                }
            }
            else
            {
                if (red)
                {
                    painting.scoreRed += 3;
                }
                else
                {
                    painting.scoreBlue += 3;
                }
            }
            painting.scoreRed_txt.text = painting.scoreRed.ToString();
            painting.scoreBlue_txt.text = painting.scoreBlue.ToString();

            collision.GetComponent<SpriteRenderer>().color = newColor;
        }
    }
}
