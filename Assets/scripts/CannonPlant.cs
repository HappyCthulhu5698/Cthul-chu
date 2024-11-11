using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVelocity : MonoBehaviour
{
    // Velocity to add to the player
    public Vector2 addedVelocity = new Vector2(0f, 10f);

    // This method is called when the object collides with another object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object collided with the player
        if (other.gameObject.CompareTag("Player") && !Input.GetKeyDown("Keycode.S"))
        {
            // Get the Rigidbody2D component from the player
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();

            // Check if the player has a Rigidbody2D component
            if (playerRb != null)
            {
                // Add the specified velocity to the player
                playerRb.velocity += addedVelocity;
            }
        }
    }
}