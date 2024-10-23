using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlayerWithInertia : MonoBehaviour
{
    private bool isDragging = false; // To check if the player is being dragged
    private Vector3 offset; // To store the offset between the mouse and the player
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        // When the mouse button is pressed on the player
        isDragging = true;

        // Calculate the offset between the player's position and the mouse position
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
        // When the mouse button is released
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // If the player is being dragged, calculate the target position
            Vector3 targetPosition = GetMouseWorldPosition() + offset;

            // Calculate the force needed to move the player towards the target position
            Vector3 force = (targetPosition - transform.position) * 10f; // Adjust the multiplier for speed

            // Apply the force to the Rigidbody2D
            rb.velocity = force;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Convert the mouse position from screen space to world space
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z; // Set the z position to the player's z position
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}