using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public float moveSpeed = 0.1f;
    public float cameraZ = -10f;
    Vector2 position;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = new Vector3(player.position.x, player.position.y, cameraZ);
        position = Vector2.Lerp(transform.position, playerPosition, moveSpeed);

    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(position.x, position.y, cameraZ) ;
        
    }
}
