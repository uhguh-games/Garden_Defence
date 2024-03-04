using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] Image tooltip;
    [SerializeField] GameObject tooltipParent;
    private FadeImage fadeImage;

    // Spawn tooltip bubble at clicked position
    // Fill tooltip text with the information associated with the clicked object (scriptable object)
    // Create a separate script for the tooltip and simply call the actions from there

    void Start() 
    {
        fadeImage = GetComponent<FadeImage>();
        HideTooltip();
    }
    private void HideTooltip() 
    {
        tooltipParent.SetActive(false);
        fadeImage.Fade(tooltip, true);
    }
    private void ShowTooltip() 
    {
        tooltipParent.SetActive(true);
        fadeImage.Fade(tooltip, false);
    }
}
