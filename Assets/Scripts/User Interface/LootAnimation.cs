using UnityEngine;
using DG.Tweening;

public class LootAnimation : MonoBehaviour
{
    [Header("Animation Configs")]

    [SerializeField] private float lootScale = 0.5f;
    [SerializeField] private float scaleDuration = 0.3f;
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float moveDelay = 0.5f;
    [SerializeField] private float lootDelayMultiplier = 0.5f;
    [SerializeField] private int amountOfLoot;

    [Space]

    [SerializeField] private Transform targetPosition;
    // [SerializeField] private float speed = 1000f;
    [SerializeField] private GameObject mainCanvas;
    private RectTransform canvasRect;
    [SerializeField] GameObject lootStackParent;
    void Awake() 
    {
        mainCanvas = GameObject.Find("Main Canvas");
    }

    public void Initialize(Transform target)
    {
        targetPosition = target;
        // Get the main canvas rect
        Canvas canvas = FindObjectOfType<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();

        // Move the object to the target position
        // MoveToTargetPosition();
        AnimateLoot(10);
    }
/*
    void MoveToTargetPosition()
    {
        canvasRect = mainCanvas.GetComponent<RectTransform>();
        targetPosition = GameObject.Find("lootTarget").transform;
        // Calculate the position on the canvas
        Vector2 targetPosOnCanvas = GetCanvasPosition(targetPosition.position);

        // Move the object to the target position
        transform.DOMove(targetPosOnCanvas, speed).SetEase(Ease.OutQuad);
    }

    */

    Vector2 GetCanvasPosition(Vector3 worldPosition)
    {
        // Convert world position to viewport position
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);

        // Convert viewport position to canvas position
        Vector2 canvasPosition = new Vector2(
            ((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f))
        );

        return canvasPosition;
    }

    public void AnimateLoot(int lootAmount) 
    {
        var delay = 0f;

        for (int i = 0; i < lootStackParent.transform.childCount; i++)
        {
            lootStackParent.transform.GetChild(i).DOScale(endValue: lootScale, duration: scaleDuration).SetEase(Ease.OutBack);

            lootStackParent.transform.GetChild(i).GetComponent<RectTransform>().DOMove(targetPosition.transform.position, duration: moveDuration).SetDelay(delay + moveDelay).SetEase(Ease.OutBack);

            lootStackParent.transform.GetChild(i).DOScale(endValue: 0f, duration: 0f).SetDelay(delay + 1f).SetEase(Ease.OutBack);

            delay += lootDelayMultiplier;
        }
    }
}
