using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Health playerHealth;
    public int towersBuilt;
    public int enemiesDefeated;
    public int enemiesFailed;
    public bool gameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        playerHealth = GetComponent<Health>();
        towersBuilt = 0;
        enemiesDefeated = 0;
        enemiesFailed = 0;
        gameOver = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
