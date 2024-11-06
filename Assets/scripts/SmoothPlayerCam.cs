using System.Collections;
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
        //turnCoroutine = StartCoroutine(FlipYLerp());

        LeanTween.rotateY(gameObject, DetermineEndRotation(), flipYRotationTime).setEaseInOutSine();
    }
    
    private IEnumerator FlipYLerp()
    {
        var startRotation = transform.localEulerAngles.y;
        var endRotation = DetermineEndRotation();

        var elapsedTime = 0f;
        while (elapsedTime < flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;
            
            var rotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / flipYRotationTime);
            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;
        return isFacingRight ? 0f : 180f;
    }
}
