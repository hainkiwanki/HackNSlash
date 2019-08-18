using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private Image m_image;
    private RectTransform m_rectTransform;
    private Rect m_rect;

    public void Init(Sprite _sprite, Vector2 _size)
    {
        Init(_sprite, _size.x, _size.y);
    }

    public void Init(Sprite _sprite, float _width, float _height)
    {
        m_image = gameObject.GetComponent<Image>();
        m_rectTransform = gameObject.GetComponent<RectTransform>();
        m_rect = m_rectTransform.rect;

        SetSprite(_sprite);
        SetImageSize(_width, _height);
    }

    public void SetSprite(Sprite _sprite)
    {
        m_image.sprite = _sprite;
        m_image.preserveAspect = true;
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
