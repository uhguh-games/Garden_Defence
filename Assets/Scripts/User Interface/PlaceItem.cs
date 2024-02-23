using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IBeginDragHandler, IPointerUpHandler
{
    [SerializeField] Image tooltip;
    [SerializeField] GameObject tooltipParent;
    private FadeImage fadeImage;
    private TowerSpawner towerSpawner;

    void Start() 
    {
        towerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();
        HideTooltip();
        fadeImage = GetComponent<FadeImage>();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Invoke("ShowTooltip", 0.4f);
        fadeImage.Fade(tooltip, false);
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        print ("Mouse Down");

        towerSpawner.PreviewTower();
    
        Invoke("ShowTooltip", 0.4f);
        fadeImage.Fade(tooltip, false);

        // Spawn tooltip bubble at clicked position
        // Fill tooltip text with the information associated with the clicked object (scriptable object)
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        Invoke("HideTooltip", 1f);
        fadeImage.Fade(tooltip, true);
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        print ("Mouse Up");
        towerSpawner.PlaceTower();

        Invoke("HideTooltip", 1f);
        fadeImage.Fade(tooltip, true);
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
