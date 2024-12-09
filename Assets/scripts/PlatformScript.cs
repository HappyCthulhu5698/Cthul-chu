using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformScript : MonoBehaviour
{
    public TilemapCollider2D tcollider;
    private GameObject player;
    private bool playerInside = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        { 
            tcollider.isTrigger = true;
        }
        else if(!playerInside)
        {
            tcollider.isTrigger = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            playerInside = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            playerInside = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            playerInside = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            playerInside = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        {
            if(collision.gameObject == player)
            {
                playerInside = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            playerInside = false;
        }
    }
}
