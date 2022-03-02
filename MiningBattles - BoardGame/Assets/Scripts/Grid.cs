using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public float scale = 0.8f;
    public Vector2Int gridSize = new Vector2Int();

    public Building[,] grid;
    private Building flyingBuilding;
    private Camera mainCamera;

    public Color currentPersonColor;
    public Outline outline;

    public Transform gridRotator;
    public float gridRotation;
    public void Awake()
    {
        grid = new Building[gridSize.x, gridSize.y];

        mainCamera = Camera.main;

        outline.enabled = false;
        //Vector3 newRotation = new Vector3(0f, -gridRotation, 0);
        gridRotator.eulerAngles = new Vector3(0f, transform.rotation.y, 0); //Quaternion.EulerAngles(0f, gridRotation, 0f);//Quaternion.Lerp(gridRotation);
    }

    public void StartPlacingBuildings(Building buildingPrefab)
    {
        if (flyingBuilding != null)
            Destroy(flyingBuilding);

        flyingBuilding = Instantiate(buildingPrefab);
    }

    public void Update()
    {
        if(flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                Debug.DrawRay(ray.direction, worldPosition, Color.red);

                bool available = true;

                //float x = worldPosition.x % scale;// - scale /2;
                //float z = worldPosition.z % scale;// - scale / 2;
                //Debug.Log(x + " | " + worldPosition.x   + " | " + z + " | " + worldPosition.z);

                flyingBuilding.transform.position = worldPosition;
                flyingBuilding.SetTransparent(available);
                //flyingBuilding.transform.position = new Vector3(x, 0, z);

                if (Input.GetKeyDown(KeyCode.F) && available)
                {
                    flyingBuilding.SetNormalCOlor();
                    flyingBuilding = null;

                    EnabledOutline();
                }
            }


        }
    }

    public void EnabledOutline()
    {
        outline.enabled = true;
        outline.OutlineColor = currentPersonColor;
        outline.OutlineWidth = 10f;
    }

    public Transform gridPoint;
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
