using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonPlant : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Velocity to add to the player
    public Vector2 addedVelocity = new Vector2(0f, 10f);
    // Position to teleport the player to
    [SerializeField] public Transform teleportPosition;
    public float waitTime = 5f;
    public Sprite sprite1;
    public Sprite sprite2;

    Rigidbody2D playerRb;
    SpriteRenderer playerSR;
    PlayerMovement playerMV;
    void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite
        spriteRenderer.sprite = sprite1;

    }
    // This method is called when the object collides with another object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object collided with the player
        if (other.gameObject.CompareTag("Player") && !Input.GetKeyDown(KeyCode.S))
        {
            spriteRenderer.sprite = sprite2;
            playerMV = other.GetComponent<PlayerMovement>();
            // Get the Rigidbody2D component from the player
            playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            // Get the player's SpriteRenderer component
            playerSR = other.gameObject.GetComponent<SpriteRenderer>();
            playerSR.enabled = false;
            playerMV.DisableControls();
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("Go", waitTime);
            other.transform.position = teleportPosition.transform.position;
            playerRb.velocity = Vector2.zero;
        }
    }

    void Go()
    {
        spriteRenderer.sprite = sprite1;
        playerMV.EnableControls();
        playerRb.velocity = addedVelocity;
        playerSR.enabled = true;
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}