using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LootCollectionUI : MonoBehaviour
{
    [SerializeField] private GameObject goldStackPrefab; //Access parent asset
    //[SerializeField] private TextMeshProUGUI counter;

    [SerializeField] private float coinScale = 0.5f;
    [SerializeField] private float coinScaleDuration = 0.3f;
    [SerializeField] private float coinMoveDuration = 0.5f;
    [SerializeField] private float coinMoveDelay = 0.5f;
    [SerializeField] private float coinDelayMultiplier = 0.5f;

    [SerializeField] private Transform destination;
    //[SerializeField] private Transform parentPostition;


    [SerializeField] private int coinNumber;



    void Start()
    {
        if (coinNumber == 0)
            coinNumber = 10;

    }
 

    public void RewardGoldStack(int noCoin, Vector3 startPosition)
    {


        var delay = 0f;

        GameObject goldStackParent = Instantiate(goldStackPrefab, transform.position, Quaternion.identity);

        goldStackParent.SetActive(true);

        for (int i = 0; i < goldStackParent.transform.childCount; i++)
        {
          

            RectTransform coinRectTransform = goldStackParent.transform.GetChild(i).GetComponent<RectTransform>();

            //convert 3D transform to screen position
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(startPosition);

            //convert screen position to canvas position
            RectTransformUtility.ScreenPointToLocalPointInRectangle( goldStackParent.transform.parent.GetComponent<RectTransform>(), screenPosition, null, out Vector2 canvasPosition);

            // Set the position of the RectTransform
            coinRectTransform.anchoredPosition = canvasPosition;

            coinRectTransform.DOScale(endValue: coinScale, duration: coinScaleDuration).SetEase(Ease.OutBack);
            coinRectTransform.DOMove(destination.position, duration: coinMoveDuration).SetDelay(delay + coinMoveDelay).SetEase(Ease.OutBack);
            coinRectTransform.DOScale(endValue: 0f, duration: 0f).SetDelay(delay + 1f).SetEase(Ease.OutBack);

            delay += coinDelayMultiplier;
        }
    }







}
