using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [HideInInspector]
    public Item m_item;

    [SerializeField] Image m_border;

    private Image m_image;
    private RectTransform m_rectTransform;
    private Rect m_rect;

    public void Init(Item _item, Vector2 _size)
    {
        Init(_item, _size.x, _size.y);
    }

    public void Init(Item _item, float _width, float _height)
    {
        m_item = _item;
        m_image = gameObject.GetComponent<Image>();
        m_rectTransform = gameObject.GetComponent<RectTransform>();
        m_rect = m_rectTransform.rect;
        //m_border.gameObject.SetActive(false);

        SetSprite(_item.m_icon);
        SetImageSize(_width, _height);
    }

    public void SetSprite(Sprite _sprite)
    {
        m_image.sprite = _sprite;
        m_image.preserveAspect = false;
    }

    public void SetImageSize(float _w, float _h)
    {
        m_rectTransform.sizeDelta = new Vector2(_w, _h);
    }

    public void SetImagePosition(Vector3 _pos)
    {
        m_rectTransform.anchoredPosition = _pos.ToVector2();
    }
}
