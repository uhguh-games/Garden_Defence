using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] Image clockPointer;
    RectTransform rectTransform;
    float pointerSpeed;
    [SerializeField] float shortDuration;
    [SerializeField] float longDuration;
    TimeManager timeManager;

    void Start() 
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        rectTransform = clockPointer.GetComponent<RectTransform>();
    }
    void Update() 
    {
        if (timeManager.currentState == TimeState.Morning || timeManager.currentState == TimeState.Evening) 
        {
            pointerSpeed = shortDuration;
        } 
        else if (timeManager.currentState == TimeState.Day || timeManager.currentState == TimeState.Night) 
        {
            pointerSpeed = longDuration;
        } 

        rectTransform.Rotate(new Vector3(0, 0, -45) * Time.deltaTime * pointerSpeed);
    }
}

