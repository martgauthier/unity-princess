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
        //Physics.gravity = new Vector3(0, -30f, 0);
        rg.centerOfMass = new Vector3(0, -1f, 0.8f); // abaisse le centre de masse
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
        
       //// Adhérence au sol (anti flottement sur bosses ou descentes)
       //RaycastHit hit;
       //if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
       //{
       //    rg.AddForce(-Vector3.up * 20f); // colle légèrement au sol
       //}
       //// Stabilisation douce pour éviter les basculements excessifs
       //Vector3 velocityLocal = transform.InverseTransformDirection(rg.velocity);
       //velocityLocal.y = 0;
       //rg.velocity = transform.TransformDirection(velocityLocal);

// Appliquer une force anti-basculement
        //Vector3 predictedUp = Quaternion.AngleAxis(
        //    rg.angularVelocity.magnitude * Mathf.Rad2Deg * Time.fixedDeltaTime, rg.angularVelocity) * transform.up;
//
        //Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
        //rg.AddTorque(torqueVector * 100f); // ajuste ce facteur pour plus ou moins de stabilité

    }
    
}


