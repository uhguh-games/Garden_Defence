using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] private float lerpSpeed;

    public void SetMaxHealth(float health) 
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        // float smoothValue = Mathf.Lerp(slider.value, health, Time.deltaTime * lerpSpeed);
        slider.value = health;
    }
}
