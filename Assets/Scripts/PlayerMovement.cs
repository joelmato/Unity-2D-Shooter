using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Sets the movement value along the x-axis
        movement.y = Input.GetAxisRaw("Vertical"); // Sets the movement value along the y-axis

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Gets the position of the mouse as a worldspace point
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * movementSpeed * Time.fixedDeltaTime); // Moves the player object

        Vector2 lookDirection = mousePos - rb.position; // Gets the direction of the mouse relative to the player
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f; // Gets the angle between the mouse and the player
        rb.rotation = angle; // Updates the angle of the player in order to point it towards the mouse
    }
}
