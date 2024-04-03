using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LootCollectionUI : MonoBehaviour
{
    [SerializeField] private GameObject goldStackParent; //Access parent asset
    [SerializeField] private TextMeshProUGUI counter;

    [SerializeField] private float coinScale = 0.5f;
    [SerializeField] private float coinScaleDuration = 0.3f;
    [SerializeField] private float coinMoveDuration = 0.5f;
    [SerializeField] private float coinMoveDelay = 0.5f;
    [SerializeField] private float coinDelayMultiplier = 0.5f;

    [SerializeField] private Transform destination;
    //[SerializeField] private Transform parentPostition;

    [SerializeField] private Vector3[] initialPosition;
    [SerializeField] private Quaternion[] initialRotation;
    [SerializeField] private int coinNumber;

 

    void Start()
    {
        if (coinNumber == 0)
            coinNumber = 10;
        

        initialPosition = new Vector3[coinNumber];
        initialRotation = new Quaternion[coinNumber];

        for (int i = 0; i < goldStackParent.transform.childCount; i++)
        {
            initialPosition[i] = goldStackParent.transform.GetChild(i).position;
            initialRotation[i] = goldStackParent.transform.GetChild(i).rotation;
        }
    }

    public void Reset(Transform lootSpawnPosition, Vector3 lootSpawnPoint)
    {
        

        for(int i = 0; i < goldStackParent.transform.childCount; i++)
        {
            goldStackParent.transform.GetChild(i).position = lootSpawnPosition.transform.position;
            goldStackParent.transform.GetChild(i).rotation = initialRotation[i];

            lootSpawnPosition.transform.position = lootSpawnPoint;
        }
    }

    public void RewardGoldStack(int noCoin)
    {
        

        var delay = 0f;

        goldStackParent.SetActive(true);
        for (int i = 0; i < goldStackParent.transform.childCount; i++)
        {
            goldStackParent.transform.GetChild(i).DOScale(endValue: coinScale, duration: coinScaleDuration).SetEase(Ease.OutBack);

            goldStackParent.transform.GetChild(i).GetComponent<RectTransform>().DOMove(destination.transform.position, duration: coinMoveDuration).SetDelay(delay + coinMoveDelay).SetEase(Ease.OutBack);

            goldStackParent.transform.GetChild(i).DOScale(endValue: 0f, duration: 0f).SetDelay(delay + 1f).SetEase(Ease.OutBack);

            delay += coinDelayMultiplier;
        }
    }





    

}
