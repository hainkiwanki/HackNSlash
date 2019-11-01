using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Item m_item;
    public Image m_image;

    [SerializeField] Image m_border;

    public RectTransform m_rectTransform;
    public Rect m_rect;

    public void Init(Item _item, Vector2 _itemSize, Vector3 _itemPosition)
    {
        m_item = _item;
        m_rectTransform = GetComponent<RectTransform>();
        m_rect = m_rectTransform.rect;

        SetSprite(_item.m_icon);
        SetImageSize(_itemSize);
        SetImagePosition(_itemPosition);
    }

    public void SetSprite(Sprite _sprite)
    {
        m_image.sprite = _sprite;
        m_image.preserveAspect = true;
    }

    public void SetImageSize(Vector2 _size)
    {
        m_rectTransform.sizeDelta = _size;
    }

    public void SetImagePosition(Vector3 _pos)
    {
        m_rectTransform.anchoredPosition = _pos.ToVector2();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging item");
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
