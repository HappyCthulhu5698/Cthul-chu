using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public bool pipesEnabled;

    public static GameManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = GameObject.Find("GameManager").GetComponent<GameManager>();

            return _instance;
        }
    }
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
