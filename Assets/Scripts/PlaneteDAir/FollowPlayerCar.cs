using UnityEngine;

public class FollowPlayerCar: MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(-10f, 8f, 0); // derrière et un peu en hauteur

    void LateUpdate()
    {
        // Calculer la position relative au joueur, en tenant compte de sa rotation
        transform.position = player.position + player.rotation * offset;

        // Toujours regarder vers le joueur
        transform.LookAt(player);
    }
}