using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UrinePointer : MonoBehaviour,IDragHandler
{
    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private RectTransform plane;

    [SerializeField]
    private float pointerOffset = 25;

    [SerializeField]
    private int MaxCollect = 5;

    private int CurrentCollected = 0;

    public Sprite[] FillSprites;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        CurrentCollected = 0;
        _image.sprite = FillSprites[0];
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        var rt = transform.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(plane, data.position, data.pressEventCamera, out globalMousePos))
        {
            var clamped = Mathf.Clamp(globalMousePos.x, 0+pointerOffset, Screen.width- pointerOffset);
            rt.position = new Vector2(clamped, rt.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentCollected++;
        Destroy(collision.gameObject);

        if(CurrentCollected >= MaxCollect)
        {
            TaskManager.Inst.CompleteCurrentTask();
            _image.sprite = FillSprites[0];
        }

        _image.sprite = FillSprites[CurrentCollected%FillSprites.Length];
    }
}
