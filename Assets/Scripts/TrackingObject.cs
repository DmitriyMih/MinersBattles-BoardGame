using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingObject : MonoBehaviour
{
    
    public void OnMouseDown()
    {
        CameraController.cameraController.followTransform = this.transform;
    }
}
