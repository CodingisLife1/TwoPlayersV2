using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
    [SerializeField] private CarController controller;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Init init;
    [SerializeField] private bool red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (init.mobile)
        {
            joystick.gameObject.SetActive(true);
        }
        else
        {
            joystick.gameObject.SetActive(false);
        }
        Vector2 inputVector = Vector2.zero;
        if (init.mobile)
        {
            inputVector.x = joystick.Horizontal;
            inputVector.y = joystick.Vertical;
        }
        else
        {
            if (red)
            {
                inputVector.x = Input.GetAxis("HorizontalRed");
                inputVector.y = Input.GetAxis("VerticalRed");
            }
            else
            {
                inputVector.x = Input.GetAxis("HorizontalBlue");
                inputVector.y = Input.GetAxis("VerticalBlue");
            }
            
        }
        

        controller.SetInputVector(inputVector);
    }
}
