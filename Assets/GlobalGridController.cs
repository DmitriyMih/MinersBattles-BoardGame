using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGridController : MonoBehaviour
{
    [Header("Settings")]
    public static GlobalGridController globalGridController;
    public Grid[,] gridMap;

    [Header("Grid border settings")]
    public float scale = 0.8f;
    public Vector2 minGridSize;
    public Vector2 maxGridSize;

    [Header("View settings")]
    public float globalScale = 0.8f;
    public Vector2Int gridSize = new Vector2Int();

    public Transform gridPoint;

    [Header("Building settings")]
    private Building flyingBuilding;
    private Camera mainCamera;

    [Header("Outline zone")]
    public GameObject currentOutlineGameobject;
    public Grid currentGrid;
    public Material[] playerZoneColor;

    public LayerMask groundLayer;
    public List<Grid> activeZones = new List<Grid>();

    [ContextMenu("Enter grid")]
    public void EnterLogGrid()
    {
        foreach(var obj in gridMap)
        {
            Debug.Log(obj + " | " + obj.currentCoordinat);
        }
    }
    
    public void Awake()
    {
        globalGridController = this;

        mainCamera = Camera.main;
    }

    public Transform debugSpher;
    public void Start()
    {
        //  grid scale
        maxGridSize.x = VoxelTilePlacerWfc.tileMapSizeX * (scale * 8);
        maxGridSize.y = VoxelTilePlacerWfc.tileMapSizeY * (scale * 8);

        CameraController.cameraController.ClampInitialization(globalGridController.minGridSize + Vector2.one * scale, globalGridController.maxGridSize + Vector2.one * scale * 2);
    }

    public void StartPlacingBuildings(Building buildingPrefab)
    {
        if (flyingBuilding != null)
            Destroy(flyingBuilding.gameObject);

        flyingBuilding = Instantiate(buildingPrefab);
    }

    public void Update()
    {
        if (flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            
            Ray buildingRay = new Ray(flyingBuilding.transform.position, -Vector3.up);
            RaycastHit hit;

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                
                Debug.DrawRay(ray.direction, worldPosition, Color.red);

                float x = Mathf.RoundToInt(worldPosition.x / scale) * scale - (scale / 2);
                float z = Mathf.RoundToInt(worldPosition.z / scale) * scale - (scale / 2);

                bool available = true;

                if (x < globalGridController.minGridSize.x || x > globalGridController.maxGridSize.x + globalGridController.minGridSize.x + scale - flyingBuilding.size.x)
                    available = false;
                if (z < globalGridController.minGridSize.y || z > globalGridController.maxGridSize.y + globalGridController.minGridSize.y + scale - flyingBuilding.size.y)
                    available = false;
                //Debug.Log(x + " | " + worldPosition.x + " | " + z + " | " + worldPosition.z);

                int currentGlobalGridX = Mathf.RoundToInt(worldPosition.x / (scale * 8)) - 1;
                int currentGlobalGridY = Mathf.RoundToInt(worldPosition.z / (scale * 8)) - 1;

                TurningOffAllGridZones(); 
                List<Grid> applyGrids = new List<Grid>();

                if ((currentGlobalGridX >= 0 && currentGlobalGridY >= 0) && (currentGlobalGridX < VoxelTilePlacerWfc.tileMapSizeX && currentGlobalGridY < VoxelTilePlacerWfc.tileMapSizeY))
                {
                    //  painting

                    applyGrids.Add(gridMap[currentGlobalGridX, currentGlobalGridY]);
                    if (currentGlobalGridX > 0)
                        applyGrids.Add(gridMap[currentGlobalGridX - 1, currentGlobalGridY]);
                    if (currentGlobalGridX < VoxelTilePlacerWfc.tileMapSizeX - 1)
                        applyGrids.Add(gridMap[currentGlobalGridX + 1, currentGlobalGridY]);
                    if (currentGlobalGridY > 0)
                        applyGrids.Add(gridMap[currentGlobalGridX, currentGlobalGridY - 1]);
                    if (currentGlobalGridY < VoxelTilePlacerWfc.tileMapSizeY - 1)
                        applyGrids.Add(gridMap[currentGlobalGridX, currentGlobalGridY + 1]);
                    
                    foreach(var appGrid in applyGrids)
                    {
                        appGrid.ApplyNewMaterial(playerZoneColor[0]);
                    }

                    activeZones.AddRange(applyGrids);
                
                }

                Debug.Log(currentGlobalGridX + " | " + worldPosition.x + " | " + currentGlobalGridY + " | " + worldPosition.z);

                flyingBuilding.SetTransparent(available);
                
                Vector3 newPosition = new Vector3(x, worldPosition.y, z);
                flyingBuilding.transform.position = newPosition;
                debugSpher.position = newPosition;

                if (Input.GetKeyDown(KeyCode.F) && available)
                {
                    flyingBuilding.SetNormalCOlor();
                    flyingBuilding = null;

                    foreach (var appGrid in applyGrids)
                    {
                        appGrid.isSetPlace = true;
                    }

                    applyGrids.Clear();
                }
            }
        }
    }

    public void TurningOffAllGridZones()
    {
        foreach (var activeObj in activeZones)
        {
            //Debug.Log(obj + " | " + obj.currentCoordinat);
            activeObj.ApplyDefaultMaterial();
        }
    }

    public void OnDrawGizmos()
    {
        if (gridPoint == null)
            return;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if ((x + y) % 2 == 0)
                    Gizmos.color = Color.black;
                else
                    Gizmos.color = Color.white;
                Gizmos.DrawWireCube(gridPoint.position + new Vector3(x, 0, y) * globalScale, new Vector3(1, 1f, 1) * globalScale);
            }
        }
    }
}
