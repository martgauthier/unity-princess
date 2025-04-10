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
	private Vector2 input;
    void Start()
    {
        //Physics.gravity = new Vector3(0, -30f, 0);
        rg.drag = 5f;           // Freinage progressif (ajuste entre 1 et 5)
        rg.angularDrag = 3f;    // Évite la rotation infinie
    }
    void Update() // Get keyboard inputs
    {
        //inputY = Input.GetAxis("Vertical");
        //inputX = Input.GetAxis("Horizontal");
        //Debug.Log(inputX + "," + inputY);
    }
	public void SetInputs(Vector2 input)
    {
        this.input = input;
    }

    void FixedUpdate() // Apply physics here
    {
        // Accelerate
        float speed = input.y > 0 ? -forwardMoveSpeed : -backwardMoveSpeed; // inverted axis
        if (input.y == 0) speed = 0;
        rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);

		// Steer (rotation uniquement sur Y, physiquement)
        float rotation = input.x * steerSpeed * Time.fixedDeltaTime;
        Quaternion turnOffset = Quaternion.Euler(0f, rotation, 0f);
        rg.MoveRotation(rg.rotation * turnOffset);

        // Stabilisation pour rester à plat (empêche les inclinaisons sur X/Z)
        Vector3 euler = rg.rotation.eulerAngles;
        rg.MoveRotation(Quaternion.Euler(0f, euler.y, 0f));

    }
    
}


