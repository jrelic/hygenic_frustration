using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WashingSponge : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private Washing _washing;

    [SerializeField]
    private RectTransform plane;

    public float CurrentWashTime;

    public float WashTime = 0.5f;

    public Vector3 LastPosition = Vector3.zero;

    public Vector3 OriginalPosition = Vector3.zero;

    [SerializeField]
    private float pointerOffsetX = 100;

    [SerializeField]
    private float pointerOffsetY = 50;

    private void Awake()
    {
        OriginalPosition = transform.position;
    }

    private void OnEnable()
    {
        CurrentWashTime = WashTime;
        transform.position = OriginalPosition;
        _washing.Refill();
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        CurrentWashTime -= Time.deltaTime;
        if (collision.gameObject.tag == "Patient" && CurrentWashTime <= 0)
        {
            if(LastPosition == Vector3.zero || Vector3.Distance(LastPosition, transform.position) > 2)
            {
                _washing.PerformWash();
                LastPosition = transform.position;
                CurrentWashTime = WashTime;
            }
        }
        else if(collision.gameObject.tag == "Shampo")
        {
            _washing.Refill();
        }
    }
}
