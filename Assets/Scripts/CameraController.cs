using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController cameraController;
    
    public Transform player;
    public Transform followTransform;

    public float minClamp = 5f;
    public float mapClampX = 30f;
    public float mapClampZ = 30f;
    public float mouseScroll = 1.5f;
    public float mouseRotation = 5f;

    public Transform cameraTransform;
    public float normalSpeed;
    public float fastSpeed;

    public float movementSpeed;
    public float movementTime;

    public float rotationAmount;
    public Vector3 zoomAmount;

    public float minZoom; 
    public float maxZoom;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    [Header("Clamp settings")]
    public bool clampInitialization = false;
    public Vector2 minClampZone;
    public Vector2 maxClampZone;

    public void Awake()
    {
        cameraController = this;
    }

    public void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    public void ClampInitialization(Vector2 minZone, Vector2 maxZone)
    {
        minClampZone = minZone;
        maxClampZone = maxZone;

        clampInitialization = true;
    }

    public void Update()
    {
        if (clampInitialization == false)
            return;

        if (followTransform != null)
        {
            HandleMouseRotationInput();
            transform.position = followTransform.position;
            
            //  zoom + rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime);
        }
        else
        {
            HandleMouseMoveInput();
            HandleMouseRotationInput();
            HandleMovementInput();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(player!= null)
            followTransform = player;
        }
    }

    public void HandleMouseMoveInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    }

    public void HandleMouseRotationInput()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount * mouseScroll;

            newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom);
            newZoom.z = -newZoom.y;
        }

        if(Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / mouseRotation));
        }
    }

        public void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        { 
            newPosition += (transform.right * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
            newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom);
            // = Mathf.Clamp(newZoom.y, -maxZoom, -minZoom);
            newZoom.z = -newZoom.y;
        }

        if (Input.GetKey(KeyCode.T))
        {
            newZoom -= zoomAmount;
            newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom);
            // = Mathf.Clamp(newZoom.y, -maxZoom, -minZoom);
            newZoom.z = -newZoom.y;
        }

        newPosition = new Vector3(Mathf.Clamp(newPosition.x, minClampZone.x, maxClampZone.x), newPosition.y, Mathf.Clamp(newPosition.z, minClampZone.y, maxClampZone.y));
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime);
    }
}
