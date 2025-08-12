using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public Camera MainCamera;
    public LayerMask TowerLayer;
    public InputAction ClickTowerAction;

    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private TextMeshProUGUI damageText;
    private int upgradeCost;
    private TowerPlaceManager placeManager;
    private Tower towerClicked;

    private void Awake()
    {
        placeManager = GetComponent<TowerPlaceManager>();
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        ClickTowerAction.Enable();
        ClickTowerAction.performed += CheckTower;
    }

    private void OnDisable()
    {
        ClickTowerAction.performed -= CheckTower;
        ClickTowerAction.Disable();
    }

    private void CheckTower(InputAction.CallbackContext context)
    {
        if (!placeManager.isPlacingTower && !GameManager.Instance.gameOver)
        {
            Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, TowerLayer))
            {
                upgradePanel.SetActive(true);
                towerClicked = hitInfo.transform.gameObject.GetComponent<Tower>();
                upgradeCost = towerClicked.towerCost * 2;
                UpdateText();
                if (towerClicked.isUpgraded)
                {
                    upgradeButton.SetActive(false);
                }
                else
                {
                    upgradeButton.SetActive(true);
                }
            }
        }
    }

    private void UpdateText()
    {
        upgradeText.text = "Upgrade Cost: $" + upgradeCost;
        cooldownText.text = "Cooldown: " + towerClicked.fireCooldown + "s";
        damageText.text = "Damage: " + towerClicked.damage;
    }

    public void UpgradeTower()
    {
        if (MoneyManager.Instance.currentMoney < upgradeCost)
        {
            return;
        }
        MoneyManager.Instance.SpendMoney(upgradeCost);
        towerClicked.isUpgraded = true;
        towerClicked.fireCooldown /= 2;
        towerClicked.damage *= 2;
        UpdateText();
        upgradeButton.SetActive(false);
    }

    public void CloseMenu()
    {
        upgradePanel.SetActive(false);
    }
}
