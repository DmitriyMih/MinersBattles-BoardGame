using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMove : MonoBehaviour
{
    [Header("Settings")]
    public GlobalGridController globalGridController;
    public float currentSideOfTheWorld = 0;
    public bool isActive = false;

    [Header("Move settings")]
    public Vector2 minClampZone;
    public Vector2 maxClampZone;

    public Vector3 newPosition;
    public Quaternion newRotation;

    public float movementTime = 0.5f;
    public float scale = 0.8f;

    [Header("Spawn settings")]
    public Transform spawnPoint;

    public void Start()
    {
        globalGridController = GlobalGridController.globalGridController;
        isActive = true;

        minClampZone = globalGridController.minGridSize;
        maxClampZone = globalGridController.maxGridSize;

        float x = Mathf.RoundToInt(transform.position.x / scale) * scale - (scale / 2);
        float z = Mathf.RoundToInt(transform.position.z / scale) * scale - (scale / 2);

        Debug.Log(x + " | " + transform.position.x + " | " + z + " | " + transform.position.z); 
        transform.position = new Vector3(Mathf.Clamp(x, minClampZone.x, maxClampZone.x), transform.position.y, Mathf.Clamp(z, minClampZone.y, maxClampZone.y));
    }

    public void Update()
    {
        if (isActive == false)
            return;

        minClampZone = globalGridController.minGridSize;
        maxClampZone = globalGridController.maxGridSize;

        currentSideOfTheWorld = CameraController.cameraController.currentSideOfTheWorld;

        float horizontal = 0;
        float vertical = 0;

        if (Input.GetKeyUp(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0f, currentSideOfTheWorld, 0f);
            vertical = scale;
            //newPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + scale);
            ///*transform.localPosition = */new Vector3(transform.localPosition.x, transform.position.y, transform.localPosition.z + scale);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0f, currentSideOfTheWorld + 180, 0f);
            vertical = -scale;
            //newPosition = new Vector3(transform.localPosition.x, transform.localPosition.y ,transform.localPosition.z - scale);
            ///*transform.localPosition =*/ new Vector3(transform.localPosition.x, transform.position.y, transform.localPosition.z - scale);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0f, currentSideOfTheWorld + 270f, 0f);
            horizontal = -scale;
            //newPosition = new Vector3(transform.localPosition.x - scale, transform.localPosition.y, transform.localPosition.x);
            ///*transform.localPosition = */new Vector3(transform.localPosition.x - scale, transform.position.y, transform.localPosition.z);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0f, currentSideOfTheWorld + 90f, 0f);
            horizontal = scale;
            //newPosition = new Vector3(transform.localPosition.x + scale, transform.localPosition.y, transform.localPosition.x);
            ///*transform.localPosition = */new Vector3(transform.localPosition.x + scale, transform.position.y, transform.localPosition.z);
        }

        switch (Mathf.RoundToInt(currentSideOfTheWorld / 90))
        {
            case 0:
                //Debug.Log("0");
                newPosition = new Vector3(transform.localPosition.x + horizontal, transform.localPosition.y, transform.localPosition.z + vertical);
                break;

            case 1:
                //Debug.Log("1");
                newPosition = new Vector3(transform.localPosition.x + vertical, transform.localPosition.y, transform.localPosition.z - horizontal);
                break;

            case 2:
                //Debug.Log("2");
                newPosition = new Vector3(transform.localPosition.x - horizontal, transform.localPosition.y, transform.localPosition.z - vertical);
                break;

            case 3:
                //Debug.Log("3");
                newPosition = new Vector3(transform.localPosition.x - vertical, transform.localPosition.y, transform.localPosition.z + horizontal);
                break;

            case 4:
                //Debug.Log("4"); 
                newPosition = new Vector3(transform.localPosition.x + horizontal, transform.localPosition.y, transform.localPosition.z + vertical);
                break;
        }

        //Debug.Log("Transform = " + transform.position + " | " + "Local = " + newPosition);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, movementTime);
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, minClampZone.x, maxClampZone.x), transform.localPosition.y, Mathf.Clamp(transform.position.z, minClampZone.y, maxClampZone.y));

        int currentGlobalGridX = Mathf.RoundToInt(transform.position.x / (scale * 8)) - 1;
        int currentGlobalGridY = Mathf.RoundToInt(transform.position.z / (scale * 8)) - 1;

        
    }

}
