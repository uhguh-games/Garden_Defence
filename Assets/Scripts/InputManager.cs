using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    CameraControls cameraControls;
    public Vector2 movementInput;

    private void OnEnable() 
    {
        if (cameraControls == null) 
        {
            cameraControls = new CameraControls();

            cameraControls.RotateCamera.Rotation.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        cameraControls.Enable();
    }

    private void OnDisable() 
    {
        cameraControls.Disable();
    }
}
