using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] Image tooltip;
    [SerializeField] GameObject tooltipParent;
    private FadeImage fadeImage;

    void Start() 
    {
        HideTooltip();
        fadeImage = GetComponent<FadeImage>();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        /*
        Invoke("ShowTooltip", 0.4f);
        fadeImage.Fade(tooltip, false);

        Invoke("HideTooltip", 2f);

        */

        // Spawn tooltip bubble at clicked position
        // Fill tooltip text with the information associated with the clicked object (scriptable object)
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        Invoke("ShowTooltip", 0.4f);
        fadeImage.Fade(tooltip, false);
        // Spawn tooltip bubble at clicked position
        // Fill tooltip text with the information associated with the clicked object (scriptable object)
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        Invoke("HideTooltip", 1f);
        fadeImage.Fade(tooltip, false);

    }

    public void OnDrag(PointerEventData data)
    {
       
   

        // hide bubble and clear text
    }

    public void OnPointerUp(PointerEventData eventData) 
    {

    }

    private void HideTooltip() 
    {
        tooltipParent.SetActive(false);
    }
    private void ShowTooltip() 
    {
        tooltipParent.SetActive(true);
    }
}
