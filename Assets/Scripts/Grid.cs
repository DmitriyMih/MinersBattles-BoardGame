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

    public bool isSetPlace = false;
    public void Awake()
    {
        grid = new Building[gridSize.x, gridSize.y];
        mainCamera = Camera.main;

        gridRotator.eulerAngles = new Vector3(0f, transform.rotation.y, 0);
    }

    public void Start()
    {
        globalGridController = GlobalGridController.globalGridController;
    }

     
    public void ApplyDefaultMaterial()
    {
        if (isSetPlace == true)
            return;

        currentRender.material = defaultMaterial;
    }

    public void ApplyNewMaterial(Material newMaterial)
    {
        if (isSetPlace == true)
            return;

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
