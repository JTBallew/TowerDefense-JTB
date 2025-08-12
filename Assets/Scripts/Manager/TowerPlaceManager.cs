using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class TowerPlaceManager : MonoBehaviour
{
    public Camera MainCamera;
    public LayerMask TileLayer;
    public InputAction PlaceTowerAction;
    public bool isPlacingTower = false;

    [SerializeField] private float placementHeightOffset = 0.2f;
    [SerializeField] private GameObject prefabPreview;
    [SerializeField] private GameObject cancelButton;
    private GameObject currentTowerPrefabToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacementPosition;
    private List<Vector3> placedTowers = new List<Vector3>();
    private bool isTileSelected = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isPlacingTower)
        {
            Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, TileLayer))
            {
                towerPlacementPosition = hitInfo.transform.position + Vector3.up * placementHeightOffset;
                towerPreview.transform.position = towerPlacementPosition;
                towerPreview.SetActive(true);
                isTileSelected = true;
            }
            else
            {
                towerPreview.SetActive(false);
                isTileSelected = false;
            }
        }
    }

    private void OnEnable()
    {
        PlaceTowerAction.Enable();
        PlaceTowerAction.performed += OnPlaceTower;
    }

    private void OnDisable()
    {
        PlaceTowerAction.performed -= OnPlaceTower;
        PlaceTowerAction.Disable();
    }

    public void StartPlacingTower (GameObject towerPrefab)
    {
        if (MoneyManager.Instance.currentMoney < towerPrefab.GetComponent<Tower>().towerCost)
        {
            Debug.Log("Not enough Money");
            return;
        }
        if (currentTowerPrefabToSpawn != towerPrefab && !GameManager.Instance.gameOver)
        {
            isPlacingTower = true;
            currentTowerPrefabToSpawn = towerPrefab;
            cancelButton.SetActive(true);
            if (towerPreview != null)
            {
                Destroy(towerPreview);
            }
            towerPreview = Instantiate(prefabPreview);
        }
    }

    private void OnPlaceTower(InputAction.CallbackContext context)
    {
        if (isPlacingTower && isTileSelected && !placedTowers.Contains(towerPlacementPosition))
        {
            isPlacingTower = false;
            if (towerPlacementPosition != null && !placedTowers.Contains(towerPlacementPosition))
            {
                placedTowers.Add(towerPlacementPosition);
            }
            Instantiate(currentTowerPrefabToSpawn, towerPlacementPosition, Quaternion.identity);
            Destroy(towerPreview);
            MoneyManager.Instance.SpendMoney(currentTowerPrefabToSpawn.GetComponent<Tower>().towerCost);
            currentTowerPrefabToSpawn = null;
            cancelButton.SetActive(false);
            GameManager.Instance.towersBuilt++;
        }
    }

    public void CancelBuilding()
    {
        isPlacingTower = false;
        currentTowerPrefabToSpawn = null;
        Destroy(towerPreview);
        cancelButton.SetActive(false);
    }
}
