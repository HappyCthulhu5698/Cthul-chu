using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothPlayerCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    
    [Header("Flip Rotation Stats")]
    [SerializeField] private float flipYRotationTime = 0.5f;
    
    private Coroutine turnCoroutine;
    private PlayerMovement playerMovement;
    private bool isFacingRight;

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        isFacingRight = playerMovement.facingRight;
    }

    private void Update()
    {
        transform.position = player.position;
    }

    public void CallTurn()
    {
        turnCoroutine = StartCoroutine(FlipYLerp());
    }
    
    private IEnumerator FlipYLerp()
    {
        isFacingRight = !isFacingRight;
        var startRotation = transform.localEulerAngles.y;
        var endRotation = isFacingRight ? 0f : 180f;

        var elapsedTime = 0f;
        
        while (elapsedTime < flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;
            
            var t = elapsedTime / flipYRotationTime;
            var rotation = Mathf.Lerp(startRotation, endRotation, t);
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            yield return null;
        }
    }
}
