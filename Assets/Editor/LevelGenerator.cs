using UnityEngine;
using UnityEditor;

public class LevelGenerator : EditorWindow
{
    private int sectionSpacing = 10;
    private int gridLength = 10;
    private int gridWidth = 10;
    private GameObject tilePrefab;
    private Transform gridParent;
    private GameObject[,] gridTiles;

    [MenuItem("Tools/Level Generator")]
    public static void ShowWindow()
    {
        GetWindow<LevelGenerator>("Level Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        gridLength = EditorGUILayout.IntField("Grid Length", gridLength);
        gridWidth = EditorGUILayout.IntField("Grid Width", gridWidth);
        GUILayout.EndHorizontal();
        GUILayout.Space(sectionSpacing);

        GUILayout.Label("Tile Prefab", EditorStyles.boldLabel);
        tilePrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", tilePrefab, typeof(GameObject), false);
        GUILayout.Space(sectionSpacing);

        GUILayout.Label("Grid Parent", EditorStyles.boldLabel);
        gridParent = (Transform)EditorGUILayout.ObjectField("Parent", gridParent, typeof(Transform), true);
        GUILayout.Space(sectionSpacing);

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }

        if (GUILayout.Button("Delete Grid"))
        {
            DeleteGrid();
        }
    }

    private void GenerateGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile Prefab is not assigned!");
            return;
        }
        gridTiles = new GameObject[gridLength, gridWidth];
        for (int x = 0; x < gridLength; x++)
        {
            for (int z = 0; z < gridWidth; z++)
            {
                Vector3 position = new Vector3(x, 0, z);
                gridTiles[x,z] = (GameObject) PrefabUtility.InstantiatePrefab(tilePrefab, gridParent);
                gridTiles[x,z].transform.position = position;
            }
        }
    }

    private void DeleteGrid()
    {
        for (int x = 0; x < gridLength; x++)
        {
            for (int z = 0; z < gridWidth; z++)
            {
                DestroyImmediate(gridTiles[x, z]);
            }
        }
    }
}
