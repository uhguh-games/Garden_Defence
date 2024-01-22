using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 origin;
    Vector3 difference;
    Vector3 resetCamera;
    bool drag;

    private void Start() 
    {
        resetCamera = Camera.main.transform.position;
    }

    private void LateUpdate() 
    {
        if (Input.GetMouseButton(0)) 
        {
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;

            if (!drag) 
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            } 
        } 
        else 
        {
            drag = false;
        }

        if (drag) 
        {
            Camera.main.transform.position = origin - difference;
            
            // Implement constraints
            
            
        }

        if (Input.GetMouseButton(1)) 
        {
            Camera.main.transform.position = resetCamera;
        }
    }
}
