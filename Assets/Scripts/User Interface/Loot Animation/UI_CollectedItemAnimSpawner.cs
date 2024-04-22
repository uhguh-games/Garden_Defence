using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_CollectedItemAnimSpawner : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public IEnumerator SpawnUICollectableItems(Canvas canvas, Collectable loot, UI_ItemSlot ui_itemSlot)
    {
        Vector3 screenPosition = cam.WorldToScreenPoint(loot.transform.position);
        Vector2 canvasPositionStart;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPosition,
            canvas.worldCamera,
            out canvasPositionStart
        );

        RectTransform targetSlotRectTransform = ui_itemSlot.GetComponent<RectTransform>();
        Vector3 targetScreenPosition = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, targetSlotRectTransform.position);
        Vector2 canvasPositionEnd;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            targetScreenPosition,
            canvas.worldCamera,
            out canvasPositionEnd
        );

        for (int i = 0; i < loot.Value; i++)
        {
            var collectableItem = Instantiate(loot.UI_CollectableItem, canvas.transform, false);
            RectTransform startRecttransform = collectableItem.GetComponent<RectTransform>();
            startRecttransform.anchoredPosition = canvasPositionStart;

            float timeToMove = 0.25f;
            Vector3 startPos = startRecttransform.anchoredPosition;
            Vector3 endPos = canvasPositionEnd;
            float startTime = Time.time;

            while (true)
            {
                float timeSinceStarted = Time.time - startTime;
                float percentageComplete = timeSinceStarted / timeToMove;

                startRecttransform.anchoredPosition = Vector3.Lerp(startPos, endPos, percentageComplete);

                if (percentageComplete >= 1.0f)
                    break;

                yield return new WaitForEndOfFrame();
            }
            
            Destroy(collectableItem);
            ui_itemSlot.PlayPulseAnimation();
            yield return new WaitForSeconds(.5f);
        }
    }
}
