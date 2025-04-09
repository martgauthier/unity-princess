using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class VehiculeController : MonoBehaviour {
    public Rigidbody rg;
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float steerSpeed;
    private float inputX;
    private float inputY;
    void Start()
    {
        rg.drag = 2f;           // Freinage progressif (ajuste entre 1 et 5)
        rg.angularDrag = 2f;    // Évite la rotation infinie
    }
    void Update() // Get keyboard inputs
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        Debug.Log(inputX + "," + inputY);
    }

    void FixedUpdate() // Apply physics here
    {
        // Accelerate
        float speed = inputY > 0 ? -forwardMoveSpeed : -backwardMoveSpeed; // inverted axis
        if (inputY == 0) speed = 0;
        rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);
        // Steer
        float rotation = inputX * steerSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0, Space.World);
    }
}


