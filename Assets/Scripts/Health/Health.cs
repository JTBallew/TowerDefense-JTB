using System;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public event System.Action<int, int> OnHealthChanged;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI towersText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public bool IsDead()
    {
        return currentHealth > 0;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            Debug.Log($"Current Health: {currentHealth}");
        }
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            GameManager.Instance.gameOver = true;
            gameOverScreen.SetActive(true);
            gameOverText.text = "Game Over!";
            towersText.text = $"Towers Built: {GameManager.Instance.towersBuilt}";
            enemiesText.text = $"Enemies Defeated: {GameManager.Instance.enemiesDefeated}";
        }
    }
}
