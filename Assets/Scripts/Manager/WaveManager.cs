using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;

[System.Serializable]
public struct SpawnData
{
    public GameObject EnemyToSpawn;
    public float TimeBeforeSpawn;
    public Transform SpawnPoint;
    public Transform EndPoint;
}

[System.Serializable]
public struct WaveData
{
    public float TimeBeforeWave;
    public List<SpawnData> EnemyData;
}

public class WaveManager : MonoBehaviour
{
    public List<WaveData> LevelWaveData;

    [SerializeField] private GameObject gameWinScreen;
    [SerializeField] private TextMeshProUGUI gameWinText;
    [SerializeField] private TextMeshProUGUI towersText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    private int wavesPassed;
    private int enemiesSpawned;
    private bool gameWon;

    void Start()
    {
        wavesPassed = 0;
        enemiesSpawned = 0;
        StartLevel();
    }

    private void Update()
    {
        if ((GameManager.Instance.enemiesDefeated + GameManager.Instance.enemiesFailed) > enemiesSpawned)
        {
            GameManager.Instance.enemiesDefeated = enemiesSpawned - GameManager.Instance.enemiesFailed;
        }
        if (wavesPassed == LevelWaveData.Count && (GameManager.Instance.enemiesDefeated + GameManager.Instance.enemiesFailed) == enemiesSpawned && !gameWon)
        {
            gameWon = true;
            GameManager.Instance.gameOver = true;
            gameWinScreen.SetActive(true);
            gameWinText.text = "You Win!";
            towersText.text = $"Towers Built: {GameManager.Instance.towersBuilt}";
            enemiesText.text = $"Enemies Defeated: {GameManager.Instance.enemiesDefeated}";
        }
    }

    public void StartLevel()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        foreach (WaveData currentWave in LevelWaveData)
        {
            yield return new WaitForSeconds(currentWave.TimeBeforeWave);
            foreach (SpawnData currentEnemyToSpawn in currentWave.EnemyData)
            {
                yield return new WaitForSeconds(currentEnemyToSpawn.TimeBeforeSpawn);
                SpawnEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
                enemiesSpawned++;
            }
            wavesPassed++;
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialized(endPoint);
    }
}
