using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    // [SerializeField] Image clockPointer;
    RectTransform rectTransform;
    float pointerSpeed;
    [SerializeField] float shortDuration;
    [SerializeField] float longDuration;
    TimeManager timeManager;
    [SerializeField] private Image morningSlice;
    [SerializeField] private Image daySlice;
    [SerializeField] private Image eveningSlice;
    [SerializeField] private Image nightSlice;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color originalColor;

    private List<Image> sliceList = new List<Image>();

    void Start() 
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        // rectTransform = clockPointer.GetComponent<RectTransform>();

        sliceList.Add(morningSlice);
        sliceList.Add(daySlice);
        sliceList.Add(eveningSlice);
        sliceList.Add(nightSlice);
    }
    void Update() 
    {
        if (!timeManager.timeIsUp) 
        {
            if (timeManager.currentState == TimeState.Morning) 
            {
                // pointerSpeed = shortDuration;
                ChangeColor(morningSlice);
            } 
            else if (timeManager.currentState == TimeState.Day) 
            {
                // pointerSpeed = longDuration;
                ChangeColor(daySlice);
            } 
            else if (timeManager.currentState == TimeState.Evening) 
            {
                ChangeColor(eveningSlice);
            } 
            else if (timeManager.currentState == TimeState.Night) 
            {
                ChangeColor(nightSlice);
            }

            // rectTransform.Rotate(new Vector3(0, 0, -45) * Time.deltaTime * pointerSpeed);
        }
    }

    void ChangeColor(Image activeSlice) 
    {
        activeSlice.color = activeColor;

        foreach (Image slice in sliceList) 
        {
            if (slice != activeSlice) 
            {
                slice.color = originalColor;
            }
        }
    }
}

