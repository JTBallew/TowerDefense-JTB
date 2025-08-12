using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int currentMoney;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int startingMoney;

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

        currentMoney = startingMoney;
    }

    void Start()
    {
        UpdateText();
    }

    public void GainMoney(int money)
    {
        currentMoney += money;
        UpdateText();
    }

    public void SpendMoney(int money)
    {
        currentMoney -= money;
        UpdateText();
    }

    private void UpdateText()
    {
        moneyText.text = "Money: $" + currentMoney;
    }
}
