using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonPlant : MonoBehaviour
{
    // Velocity to add to the player
    public Vector2 addedVelocity = new Vector2(0f, 10f);
    // Position to teleport the player to
    public Vector2 teleportPosition;
    public float waitTime = 5f;

    Rigidbody2D playerRb;
    SpriteRenderer playerSR;
    PlayerMovement playerMV;

    // This method is called when the object collides with another object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object collided with the player
        if (other.gameObject.CompareTag("Player") && !Input.GetKeyDown(KeyCode.S))
        {
            print("hit");
            playerMV = other.GetComponent<PlayerMovement>();
            // Get the Rigidbody2D component from the player
            playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            // Get the player's SpriteRenderer component
            playerSR = other.gameObject.GetComponent<SpriteRenderer>();
            playerSR.enabled = false;
            playerMV.DisableControls();
            Invoke("Go", waitTime);
            other.transform.position = teleportPosition;
        }
    }

    void Go()
    {
        playerMV.EnableControls();
        print("launch");
        playerRb.velocity = addedVelocity;
        playerSR.enabled = true;
    }
    void Freeze()
    {
        playerRb.velocity = new Vector2(0f, 0f);
    }
}