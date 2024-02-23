using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] float zoomChange;
    [SerializeField] float smoothChange;
    [SerializeField] float minSize, maxSize;
    private Camera cam;

    private void Start() 
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate() 
    {
        if (Input.mouseScrollDelta.y > 0) 
        {
            cam.orthographicSize -= zoomChange * Time.deltaTime * smoothChange;
        } 
        else if (Input.mouseScrollDelta.y < 0) 
        {
            cam.orthographicSize += zoomChange * Time.deltaTime * smoothChange;
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);

    }
}
