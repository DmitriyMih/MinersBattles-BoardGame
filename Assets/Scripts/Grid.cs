using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Vector2Int currentCoordinat;
    public GlobalGridController globalGridController;
    public float scale = 0.8f;
    
    public Vector2Int gridSize = new Vector2Int();

    public Building[,] grid;
    //private Building flyingBuilding;
    private Camera mainCamera;

    public Color currentPersonColor;
    
    public Transform gridRotator;
    public float gridRotation;

    public Renderer currentRender;
    public Material defaultMaterial;

    public void Awake()
    {
        grid = new Building[gridSize.x, gridSize.y];
        mainCamera = Camera.main;

        gridRotator.eulerAngles = new Vector3(0f, transform.rotation.y, 0);
    }

    public void Start()
    {
        globalGridController = GlobalGridController.globalGridController;
        //  grid scale
        //globalGridController.maxGridSize.x = VoxelTilePlacerWfc.tileMapSizeX * (scale * 8);
        //globalGridController.maxGridSize.y = VoxelTilePlacerWfc.tileMapSizeY * (scale * 8);

        //CameraController.cameraController.ClampInitialization(globalGridController.minGridSize + Vector2.one * scale, globalGridController.maxGridSize + Vector2.one * scale * 2);
    }

    //public void StartPlacingBuildings(Building buildingPrefab)
    //{
    //    if (flyingBuilding != null)
    //        Destroy(flyingBuilding.gameObject);

    //    flyingBuilding = Instantiate(buildingPrefab);
    //}

    //public void Update()
    //{
    //    if (flyingBuilding != null)
    //    {
    //        var groundPlane = new Plane(Vector3.up, Vector3.zero);
    //        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

    //        zoneOutline.OutlineWidth = 10;
    //        if (groundPlane.Raycast(ray, out float position))
    //        {
    //            Vector3 worldPosition = ray.GetPoint(position);
    //            Debug.DrawRay(ray.direction, worldPosition, Color.red);

    //            //Debug.Log("Pre x = " + x);
    //            //x = Mathf.RoundToInt(x);
    //            //x *= scale;
    //            //x -= scale / 2;
    //            //Debug.Log("Past x = " + x);
    //            float x = Mathf.RoundToInt(worldPosition.x / scale) * scale - (scale / 2);
    //            float z = Mathf.RoundToInt(worldPosition.z / scale) * scale - (scale / 2);

    //            bool available = true;

    //            if (x < globalGridController.minGridSize.x || x > globalGridController.maxGridSize.x + globalGridController.minGridSize.x + scale - flyingBuilding.size.x)
    //                available = false;
    //            if (z < globalGridController.minGridSize.y || z > globalGridController.maxGridSize.y + globalGridController.minGridSize.y + scale - flyingBuilding.size.y)
    //                available = false;


    //            debugCoordinatSphere = new Vector3(x, 3f, z);
    //            Debug.Log(x + " | " + worldPosition.x + " | " + z + " | " + worldPosition.z);

    //            //flyingBuilding.transform.position = worldPosition;
    //            flyingBuilding.SetTransparent(available);
    //            flyingBuilding.transform.position = new Vector3(x, worldPosition.y, z);

    //            if (Input.GetKeyDown(KeyCode.F) && available)
    //            {
    //                flyingBuilding.SetNormalCOlor();
    //                flyingBuilding = null;

    //                EnabledOutline();
    //            }
    //        }
    //    }
    //    //else
    //    //    zoneOutline.OutlineWidth = 0;
    //}
    //public void EnabledOutline()
    //{
    //    zoneOutline.OutlineColor = currentPersonColor;
    //    zoneOutline.OutlineWidth = 10f;
    //}

    public void ApplyDefaultMaterial()
    {
        currentRender.material = defaultMaterial;
    }

    public void ApplyNewMaterial(Material newMaterial)
    {
        currentRender.material = newMaterial;
    }

    public Transform gridPoint;
    public float borderScale = 6.4f;
    public void OnDrawGizmos()
    {   
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gridRotator.position, Vector3.one * 6.4f);
    }

    public void OnDrawGizmosSelected()
    {
        if (gridPoint == null)
            return;
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if ((x + y) % 2 == 0)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.yellow;
                Gizmos.DrawCube(gridPoint.position + new Vector3(x, 0, y) * scale, new Vector3(1, 1f, 1) * scale);
            }
        }
    }
}
