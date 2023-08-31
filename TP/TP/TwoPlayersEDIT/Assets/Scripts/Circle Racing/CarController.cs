using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float accelerationFactor;
    [SerializeField] private float turnFactor;
    [SerializeField] private float driftFactor;

    private float accelerationInput;
    private float rotationAngle;
    private float steeringInput;

    public float maxSpeed;
    private float velocityVsUp;

    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        EngineForce();
        Steering();
        OrtagonalVelocity();
    }

    private void EngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (accelerationInput == 0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3, Time.fixedDeltaTime * 3);
        }
        else
        {
            rb.drag = 0;
        }

        Vector2 forceVector = transform.up * accelerationInput * accelerationFactor;
        rb.AddForce(forceVector, ForceMode2D.Force);
    }

    private void Steering()
    {
        float minSpeed = rb.velocity.magnitude / 8;
        minSpeed = Mathf.Clamp01(minSpeed);
        rotationAngle -= steeringInput * turnFactor * minSpeed;
        rb.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    private void OrtagonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
}
