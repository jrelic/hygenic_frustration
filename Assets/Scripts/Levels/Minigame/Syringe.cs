using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Syringe : MonoBehaviour, IDragHandler
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

    public Sprite[] FillSprites;

    [SerializeField]
    private Image _image;

    private void Awake()
    {
        _image.sprite = FillSprites[0];
    }

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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentCollected++;

        if (CurrentCollected >= MaxCollect)
        {
            TaskManager.Inst.CompleteCurrentTask();
            _image.sprite = FillSprites[0];
        }

        _image.sprite = FillSprites[CurrentCollected % FillSprites.Length];
    }
}
