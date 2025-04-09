using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;            // Vitesse de déplacement
    public float rotationSpeed = 200f;  // Vitesse de rotation

    private Rigidbody rb;
    private Animator animator;
    private Vector3 inputDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Debug.Log("PlayerMovement script started");
    }

    void Update()
    {
        float inputZ = 0f;
        float inputX = 0f;

        if (Input.GetKey(KeyCode.Z)) inputZ = -1f;  // Avancer
        if (Input.GetKey(KeyCode.S)) inputZ = 1f;   // Reculer
        if (Input.GetKey(KeyCode.Q)) inputX = -1f;
        if (Input.GetKey(KeyCode.D)) inputX = 1f;

        // Stocke l'input dans un vecteur
        inputDirection = new Vector3(0, 0, inputZ).normalized;

        // Tourner avec Q/D
        if (inputX != 0)
        {
            float rotation = inputX * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotation);
        }

        // Gérer l'animation
        bool isWalking = inputDirection.magnitude > 0.1f;

        animator.SetBool("isWalking", isWalking);
    }

    void FixedUpdate()
    {
        // Avancer dans la direction où regarde le personnage
        if (inputDirection.magnitude > 0.1f)
        {
            Vector3 move = transform.TransformDirection(inputDirection) * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }
    }
}

