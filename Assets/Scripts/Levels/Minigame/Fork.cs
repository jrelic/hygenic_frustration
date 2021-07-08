using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fork : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private RectTransform plane;

    [SerializeField]
    private float pointerOffsetX = 100;

    [SerializeField]
    private float pointerOffsetY = 50;

    [SerializeField]
    private int MaxCollect = 15;

    private int CurrentCollected = 0;

    private GameObject foodOnFork;

    private Vector3 originalFoodPosition;

    private void OnEnable()
    {
        CurrentCollected = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            plane = data.pointerEnter.transform as RectTransform;

        var rt = transform.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(plane, data.position, data.pressEventCamera, out globalMousePos))
        {
            var clampedX = Mathf.Clamp(globalMousePos.x, 0 + pointerOffsetX, Screen.width - pointerOffsetX);
            var clampedY = Mathf.Clamp(globalMousePos.y, 0 - pointerOffsetY, Screen.height + pointerOffsetY);
            rt.position = new Vector2(clampedX, clampedY);

            if(foodOnFork != null)
            {
                foodOnFork.transform.position = rt.position;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(foodOnFork != null)
        {
            if(collision.gameObject.tag == "Patient")
            {
                foodOnFork.SetActive(false);
                foodOnFork.transform.position = originalFoodPosition;
                CurrentCollected++;
                foodOnFork = null;
            }
        }
        else if(collision.gameObject.tag == "Food")
        {
            foodOnFork = collision.gameObject;
            originalFoodPosition = collision.gameObject.transform.position;
        }


        if (CurrentCollected >= MaxCollect)
        {
            TaskManager.Inst.CompleteCurrentTask();
        }
    }
}
