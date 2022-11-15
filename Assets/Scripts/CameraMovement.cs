using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public float moveSpeed = 0.1f;
    public float cameraZ = -10f;
    Vector2 position;

    void Update()
    {
        // Updates the vector containing the new position for the camera to move to
        Vector3 playerPosition = new Vector3(player.position.x, player.position.y, cameraZ);
        position = Vector2.Lerp(transform.position, playerPosition, moveSpeed);

    }

    private void FixedUpdate()
    {
        // Updates the position of the camera
        transform.position = new Vector3(position.x, position.y, cameraZ) ;
    }
}
