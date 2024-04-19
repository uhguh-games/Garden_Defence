using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    /*
    [SerializeField] Transform cameraPivot;
    [SerializeField] float speed;
    [SerializeField] float rotationAngle = 90f;
    bool isRotating;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotating)
        {
            RotateCameraClockwise();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotating)
        {
            RotateCameraCounterClockwise();
        }
    }

    void RotateCameraClockwise()
    {
        if (isRotating) return;

        isRotating = true;
        float targetAngle = Mathf.Repeat(transform.eulerAngles.y + rotationAngle, 360f);

        StartCoroutine(RotateClockwiseCoroutine(targetAngle));
    }

    void RotateCameraCounterClockwise()
    {
        if (isRotating) return;

        isRotating = true;
        float targetAngle = Mathf.Repeat(transform.eulerAngles.y -rotationAngle, 360f);

        StartCoroutine(RotateCounterClockwiseCoroutine(targetAngle));
    }

    IEnumerator RotateClockwiseCoroutine(float targetAngle)
    {
        while (Mathf.Abs(transform.eulerAngles.y - targetAngle) > 1.0f)
        {
            float step = 90f * Time.deltaTime * speed;
            transform.RotateAround(cameraPivot.position, Vector3.up, step);
            yield return null;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);

        isRotating = false;
    }

    IEnumerator RotateCounterClockwiseCoroutine(float targetAngle)
    {
        while (Mathf.Abs(transform.eulerAngles.y - targetAngle) > 1.0f)
        {
            float step = -90f * Time.deltaTime * speed;
            transform.RotateAround(cameraPivot.position, Vector3.up, step);
            yield return null;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);

        isRotating = false;
    }

    */
}