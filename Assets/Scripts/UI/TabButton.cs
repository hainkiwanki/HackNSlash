using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TabGroup m_tabGroup;

    [HideInInspector]
    public Image m_background;

    public void OnPointerClick(PointerEventData eventData)
    {
        m_tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        m_background = GetComponent<Image>();
        m_tabGroup.Subscribe(this);
    }
}
