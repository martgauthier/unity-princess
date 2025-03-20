using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.Q)) moveX = -1f;  // Q → Gauche
        if (Input.GetKey(KeyCode.D)) moveX = 1f;   // D → Droite
        if (Input.GetKey(KeyCode.Z)) moveZ = 1f;   // Z → Avancer
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;  // S → Reculer

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized * speed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
}

