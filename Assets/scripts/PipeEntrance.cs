using UnityEngine;

public class PipeEntrance : MonoBehaviour
{
    public PipeSystem pipeSystem;
    public bool isEndEntrance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        
        Vector2 entryPosition = collision.transform.position;
        StartCoroutine(pipeSystem.MoveThroughPipe(collision.gameObject, isEndEntrance, entryPosition));
    }
}