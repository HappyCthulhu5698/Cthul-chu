using UnityEngine;

public class PipeConsumable : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        GameManager.Instance.pipesEnabled = true;
        Destroy(gameObject);
    }
}
