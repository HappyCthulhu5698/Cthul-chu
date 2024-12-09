using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTile : MonoBehaviour
{
    public GameObject particleSystem;
    private bool wallEnabled = true;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (wallEnabled && Input.GetKey(KeyCode.J))
        {
            gameObject.GetComponent<TilemapRenderer>().enabled = false;
            gameObject.GetComponent<TilemapCollider2D>().enabled = false;
            particleSystem.GetComponent<ParticleSystem>().Play();
            ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
            em.enabled = true;
        }
        if (Input.GetKey(KeyCode.J))
        {
            wallEnabled = false;
        }
    }
}
