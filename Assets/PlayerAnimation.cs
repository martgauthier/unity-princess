using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;  // Référence à l'Animator
    private Rigidbody rb;
    private float speedThreshold = 0.1f; // Seuil pour détecter le mouvement

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Vérifier si une touche de déplacement est appuyée
        bool isMoving = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D);

        // Activer ou désactiver l'animation en fonction du mouvement
        animator.SetBool("isWalking", isMoving);
    }
}
