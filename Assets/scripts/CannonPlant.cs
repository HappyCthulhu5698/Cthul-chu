using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerVelocity : MonoBehaviour
{
    // Velocity to add to the player
    public Vector2 addedVelocity = new Vector2(0f, 10f);
    // Time in seconds for the cooldown
    public float cooldownTime = 5f;
    // Flag to check if cooldown is active
    private bool isCooldown = false;
    // Flag to check if cooldown is active
    public float cooldownTime2 = 5f;
    // Time in seconds for the cooldown
    private bool isCooldown2 = false;
    // Position to teleport the player to
    public Vector2 teleportPosition;

    // This method is called when the object collides with another object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object collided with the player
        if (other.gameObject.CompareTag("Player") && !Input.GetKeyDown(KeyCode.S))
        {

            Invoke("go", 1f);
            //USE THIS

            // Get the Rigidbody2D component from the player
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            // Get the player's SpriteRenderer component
            SpriteRenderer playerSR = other.gameObject.GetComponent<SpriteRenderer>();

            // Check if the player has a Rigidbody2D component
            if (playerRb != null)
            {
                StartCoroutine(Cooldown2());
                if (isCooldown2)
                {
                    // Hold the player in place by setting velocity to zero
                    playerRb.velocity = Vector2.zero;
                    // Make the player invisible
                    playerSR.enabled = false;

                    // Teleport the player to the specified position
                    other.transform.position = teleportPosition;
                }
                if (!isCooldown2 && !isCooldown)
                {
                    // Add the specified velocity to the player
                    playerRb.velocity = addedVelocity;
                    StartCoroutine(Cooldown());
                    playerSR.enabled = true;
                }
            }
        }
    }
    // Coroutine to handle the cooldown
    private IEnumerator Cooldown()
    {
        // Set the cooldown flag to true
        isCooldown = true;

        // Wait for the specified cooldown time
        yield return new WaitForSeconds(cooldownTime);

        // Set the cooldown flag back to false
        isCooldown = false;
    }
    private IEnumerator Cooldown2()
    {
        // Set the cooldown flag to true
        isCooldown2 = true;

        // Wait for the specified cooldown time
        yield return new WaitForSeconds(cooldownTime2);

        // Set the cooldown flag back to false
        isCooldown2 = false;
    }
}