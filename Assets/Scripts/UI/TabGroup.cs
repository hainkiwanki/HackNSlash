using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField]
    private List<TabButton> m_tabButtons;

    public Sprite m_tabIdle;
    public Sprite m_tabHovered;
    public Sprite m_tabActive;

    public TabButton m_selectedTab;
    public List<GameObject> m_objToSwap;

    private void Start()
    {
        m_selectedTab.m_background.sprite = m_tabActive;
    }

    public void Subscribe(TabButton _button)
    {
        if (m_tabButtons == null)
            m_tabButtons = new List<TabButton>();

        m_tabButtons.Add(_button);
    }

    public void OnTabEnter(TabButton _button)
    {
        ResetTabs();
        if(m_selectedTab == null || m_selectedTab != _button)
            _button.m_background.sprite = m_tabHovered;
    }

    public void OnTabExit(TabButton _button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton _button)
    {
        m_selectedTab = _button;
        ResetTabs();
        _button.m_background.sprite = m_tabActive;
        int index = _button.transform.GetSiblingIndex();
        for (int i = 0; i < m_objToSwap.Count; i++)
        {
            m_objToSwap[i].SetActive(i == index);
        }
    }

    public void ResetTabs()
    {
        int cnt = m_tabButtons.Count;
        for (int i = 0; i < cnt; i++)
        {
            if (m_selectedTab != null && m_selectedTab == m_tabButtons[i])
                continue;
            m_tabButtons[i].m_background.sprite = m_tabIdle;
        }
    }
}
