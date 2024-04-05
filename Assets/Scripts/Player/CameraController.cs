using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private Transform player;
    private Vector3 offset;

    private void Awake()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        offset = transform.position - player.position;
    }
    void LateUpdate()
    {
        // Vérifier si le joueur existe
        if (player != null)
        {
            // Positionner la caméra à la position du joueur avec l'offset
            transform.position = player.position + offset;

            // Regarder toujours vers le joueur
            transform.LookAt(player);
        }
    }
}
