using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioTestSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }



    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(slider.value);
    }
    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(slider.value);
    }
}
