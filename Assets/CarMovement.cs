using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Vector2 input;

    public Rigidbody rg;
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float steerSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetInputs(Vector2 input)
    {
        this.input = input;
    }

    public float GetSpeed()
    {
        return forwardMoveSpeed;
    }

    void FixedUpdate()
    {
        float speed = input.y > 0 ? forwardMoveSpeed : backwardMoveSpeed;
        if (input.y == 0) speed = 0;
        rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);
        // Steer
        float rotation = input.x * steerSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0, Space.World);
    }
}
