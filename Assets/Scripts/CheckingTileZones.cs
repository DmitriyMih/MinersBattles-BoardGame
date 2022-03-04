using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingTileZones : MonoBehaviour
{
    public static CheckingTileZones checkingTileZones;

    public List<Grid> grids = new List<Grid>();
    public void Awake()
    {
        checkingTileZones = this;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.GetComponent<Grid>())
        {
            if (grids.Count > 0)
            {
                grids[0].ApplyDefaultMaterial();
                grids.Remove(grids[0]);
            }

            Grid currentGrids = other.transform.parent.GetComponent<Grid>();
            grids.Add(currentGrids);
            currentGrids.ApplyNewMaterial(GlobalGridController.globalGridController.playerZoneColor[0]);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (grids.Count > 0)
            grids[0].ApplyNewMaterial(GlobalGridController.globalGridController.playerZoneColor[0]);
        else
        {
            if (other.transform.parent.GetComponent<Grid>())
            {
                Grid currentGrids = other.transform.parent.GetComponent<Grid>();
                grids.Add(currentGrids);
                currentGrids.ApplyNewMaterial(GlobalGridController.globalGridController.playerZoneColor[0]);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
    //    if (other.transform.parent.GetComponent<Grid>())
    //    {
    //        if (grids.Count > 1)
    //        {
    //            grids[0].ApplyDefaultMaterial();
    //            grids.Remove(grids[0]);
    //        }

    //        Grid currentGrids = other.transform.parent.GetComponent<Grid>();
    //        grids.Remove(currentGrids);
    //    }
    }
}
