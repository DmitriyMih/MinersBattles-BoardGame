using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer mainRender;

    public float scale = 0.125f;
    public Vector2Int size = Vector2Int.one;

    public BuildingsType currentBuildingType;
    public enum BuildingsType
    {
        miniTower,
        bigTower,
        conveyor,
        sawmill
    }

    public void SetTransparent(bool available)
    {
        if (available)
            mainRender.material.color = Color.yellow;
        else
            mainRender.material.color = Color.red;
    }

    public List<Grid> GrantingTerritorialZone(int minX, int minY, int maxX, int maxY, Grid[,] map)
    {
        List<Grid> currentTerritorial = new List<Grid>();

        switch (currentBuildingType)
        {
            case BuildingsType.miniTower:
                //  centr
                currentTerritorial.Add(map[minX, minY]);
                //  left
                if (minX > 0)
                    currentTerritorial.Add(map[minX - 1, minY]);
                //  right
                if (minX < VoxelTilePlacerWfc.tileMapSizeX - 1)
                    currentTerritorial.Add(map[minX + 1, minY]);
                //  bottom
                if (minY > 0)
                    currentTerritorial.Add(map[minX, minY - 1]);
                //  top
                if (minY < VoxelTilePlacerWfc.tileMapSizeY - 1)
                    currentTerritorial.Add(map[minX, minY + 1]);
                break;

            case BuildingsType.bigTower:
                //  centr
                currentTerritorial.Add(map[minX, minY]);
                //  left
                if (minX > 0)
                {
                    currentTerritorial.Add(map[minX - 1, minY]);
                    if (minX > 1)
                        currentTerritorial.Add(map[minX - 2, minY]);

                }
                //  right
                if (minX < VoxelTilePlacerWfc.tileMapSizeX - 1)
                {
                    currentTerritorial.Add(map[minX + 1, minY]);
                    if (minX < VoxelTilePlacerWfc.tileMapSizeX - 2)
                        currentTerritorial.Add(map[minX + 2, minY]);
                }
                //  bottom
                if (minY > 0)
                {
                    currentTerritorial.Add(map[minX, minY - 1]);
                    if (minY > 1)
                        currentTerritorial.Add(map[minX, minY - 2]);
                }
                //  top
                if (minY < VoxelTilePlacerWfc.tileMapSizeY - 1)
                {
                    currentTerritorial.Add(map[minX, minY + 1]);
                    if (minY < VoxelTilePlacerWfc.tileMapSizeY - 2)
                        currentTerritorial.Add(map[minX, minY + 2]);
                }
                //-  title   -//  internal corners
                //  left bottom
                if (minX > 0 && minY > 0)
                    currentTerritorial.Add(map[minX - 1, minY - 1]);
                //  left top
                if (minX > 0 && minY < VoxelTilePlacerWfc.tileMapSizeY - 1)
                    currentTerritorial.Add(map[minX - 1, minY + 1]);
                //  right bottom
                if (minX < VoxelTilePlacerWfc.tileMapSizeX - 1 && minY > 0)
                    currentTerritorial.Add(map[minX + 1, minY - 1]);
                //  right top
                if (minX < VoxelTilePlacerWfc.tileMapSizeX - 1 && minY < VoxelTilePlacerWfc.tileMapSizeY - 1)
                    currentTerritorial.Add(map[minX + 1, minY + 1]);

                break;

        }
        return currentTerritorial;
    }

    public void SetNormalCOlor()
    {
        mainRender.material.color = Color.white;
    }

    public void OnDrawGizmos()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if ((x + y) % 2 == 0)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.yellow;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y) * scale, new Vector3(1, 1f, 1) * scale);
            }
        }
    }

}
