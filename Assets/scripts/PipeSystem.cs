using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PipeSystem : MonoBehaviour
{
    public List<Transform> waypoints;
    public float travelSpeed = 5f;
    public float cornerPauseDuration = 0.1f;
    public float entranceGuideDuration = 0.1f;
    public float pipeWidth = 0.5f;
    public Color pipeColor = Color.magenta;
    public GameObject pipeSegmentPrefab;
    
    private bool isMoving;
    
    private void Start()
    {
        GeneratePipeVisuals();
    }
    
    private void GeneratePipeVisuals()
    {
        // Create parent object
        var parent = new GameObject("Visuals");
        parent.transform.SetParent(transform);
        
        for (var i = 0; i < waypoints.Count - 1; i++)
        {
            CreatePipeSegment(waypoints[i].position, waypoints[i + 1].position, parent, i);
        }
    }

    private void CreatePipeSegment(Vector2 start, Vector2 end, GameObject parent, int index)
    {
        var segment = Instantiate(pipeSegmentPrefab, parent.transform, true);
        segment.name = $"Segment {index + 1}";
        
        // Calculate position and scale
        var middlePoint = (start + end) / 2;
        var distance = Vector2.Distance(start, end) + pipeWidth;
        var direction = (end - start).normalized;

        // Set position and scale
        segment.transform.position = middlePoint;
        segment.transform.localScale = new Vector3(distance, pipeWidth, 0);

        // Set rotation
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        segment.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Set color
        var segmentRenderer = segment.GetComponent<SpriteRenderer>();
        segmentRenderer.material.color = pipeColor;
        segmentRenderer.sortingOrder = -1;
    }

    public IEnumerator MoveThroughPipe(GameObject player, bool startFromEnd, Vector2 entryPosition)
    {
        if (isMoving) yield break;
        isMoving = true;
        
        var playerMovement = player.GetComponent<PlayerMovement>();
        var rb = player.GetComponent<Rigidbody2D>();
       
        // Disable player controls
        playerMovement.DisableControls();

        // Set starting index and position
        var startIndex = startFromEnd ? waypoints.Count - 1 : 0;
        Vector2 pipeEntrance = waypoints[startIndex].position;

        // Guide player to pipe entrance
        yield return StartCoroutine(GuideToEntrance(rb, entryPosition, pipeEntrance));

        // Move player through pipe
        if (startFromEnd)
        {
            for (var i = waypoints.Count - 1; i > 0; i--)
            {
                yield return StartCoroutine(MoveToNextWaypoint(player, waypoints[i], waypoints[i-1]));
            }
        }
        else
        {
            for (var i = 0; i < waypoints.Count - 1; i++)
            {
                yield return StartCoroutine(MoveToNextWaypoint(player, waypoints[i], waypoints[i+1]));
            }
        }

        // Enable player controls
        playerMovement.EnableControls();
        isMoving = false;
    }
    
    private IEnumerator GuideToEntrance(Rigidbody2D rb, Vector2 startPosition, Vector2 pipeEntrance)
    {
        var startTime = Time.time;

        while (Time.time - startTime < entranceGuideDuration)
        {
            var t = (Time.time - startTime) / entranceGuideDuration;
            rb.MovePosition(Vector2.Lerp(startPosition, pipeEntrance, t));
            yield return new WaitForFixedUpdate();
        }

        // Ensure the player is exactly at the pipe entrance
        rb.MovePosition(pipeEntrance);
    }

    private IEnumerator MoveToNextWaypoint(GameObject player, Transform start, Transform end)
    {
        var journeyLength = Vector2.Distance(start.position, end.position);
        var startTime = Time.time;

        while (player.transform.position != end.position)
        {
            var distanceCovered = (Time.time - startTime) * travelSpeed;
            var fractionOfJourney = distanceCovered / journeyLength;
            player.transform.position = Vector2.Lerp(start.position, end.position, fractionOfJourney);
            yield return null;
        }
        
        var rb = player.GetComponent<Rigidbody2D>();
        var wasUsingGravity = rb.gravityScale != 0;
        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        // Pause at the corner
        yield return new WaitForSeconds(cornerPauseDuration);
        
        if (wasUsingGravity) rb.gravityScale = originalGravity;
    }
}