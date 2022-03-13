using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [Header("Settings")]
    public float scale = 0.8f;
    public Building currentBuilding;

    public bool isMoveActive = false;
    public Conveyor nextConveyorPieces;
    //public Conveyor lastConveyorPieces;

    [Header("State settings")]
    public List<GameObject> conveyorPrefabs = new List<GameObject>();
    public ConveyorState currentConveyorState;
    public enum ConveyorState
    {
        single,
        front,
        back,
        middle
    }

    public void Awake()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, GlobalGridController.globalGridController.lastConveyorRotation, 0f);
    }

    public void Enabled()
    {
        isMoveActive = true;
    }

    public void Disabled()
    {
        isMoveActive = false;
    }

    public void DebugColoring()
    {
        Color randomColor = Random.ColorHSV();
        foreach (var render in currentBuilding.conveyorsRenders)
        {
            render.material.color = randomColor;
        }
    }

    public void Update()
    {
        if (isMoveActive == false)
            return;

        //  software
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90f, 0f);
            GlobalGridController.globalGridController.lastConveyorRotation = transform.eulerAngles.y;
        }
        //  againstif
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90f, 0f);
            GlobalGridController.globalGridController.lastConveyorRotation = transform.eulerAngles.y;
        }


    }

    [ContextMenu("Check state")]
    public void CheckState()
    {

        if (nextConveyorPieces.transform.eulerAngles.y != transform.eulerAngles.y)
            return;


        //if (nextConveyorPieces == null && lastConveyorPieces == null)
        //{ 
        //    currentConveyorState = ConveyorState.single;
        //    ApplyState(currentConveyorState);
        //    return; 
        //}

        //if (currentConveyorState == ConveyorState.single)
        //{
        //    currentConveyorState = ConveyorState.front;
        //    //
        //    switch (nextConveyorPieces.currentConveyorState)
        //    {
        //        case ConveyorState.single:
        //            nextConveyorPieces.currentConveyorState = ConveyorState.back;
        //            break;

        //        case ConveyorState.front:
        //            nextConveyorPieces.currentConveyorState = ConveyorState.middle;
        //            break;
        //    }
        //}

        //if (currentConveyorState == ConveyorState.back)
        //{
        //    currentConveyorState = ConveyorState.middle;
        //    //
        //    switch (nextConveyorPieces.currentConveyorState)
        //    {
        //        case ConveyorState.single:
        //            nextConveyorPieces.currentConveyorState = ConveyorState.back;
        //            break;

        //        case ConveyorState.front:
        //            nextConveyorPieces.currentConveyorState = ConveyorState.middle;
        //            break;
        //    }
        //}

        ApplyState(currentConveyorState);
    }

    public void ApplyState(ConveyorState conveyorState)
    {
        foreach(var prefab in conveyorPrefabs)
            prefab.SetActive(false);
        
        switch(conveyorState)
        {
            case ConveyorState.single:
                conveyorPrefabs[0].SetActive(true);
                break;

            case ConveyorState.front:
                conveyorPrefabs[1].SetActive(true);
                break;

            case ConveyorState.back:
                conveyorPrefabs[2].SetActive(true);
                break;

            case ConveyorState.middle:
                conveyorPrefabs[3].SetActive(true);
                break;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Conveyor" && other.transform.gameObject != this)
        {
            nextConveyorPieces = other.transform.GetComponent<Conveyor>();
            
            nextConveyorPieces.DebugColoring();
  
  //other.GetComponent<Conveyor>().DebugColoring();
            //nextConveyorPieces.lastConveyorPieces = this;
            //Debug.Log(nextConveyorPieces.lastConveyorPieces);

            //CheckState();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Conveyor" && other.gameObject == nextConveyorPieces.gameObject)
        {
            //nextConveyorPieces.lastConveyorPieces = null;
            //nextConveyorPieces.currentBuilding.SetNormalCOlor();
            //nextConveyorPieces = null;
            Debug.Log("Del");
            
            //nextConveyorPieces.CheckState();
            //CheckState();
        }
    }
}
