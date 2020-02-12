using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct LayoutSlot
{
    public int x;
    public int y;

    public LayoutSlot(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}

public class Item : MonoBehaviour
{
    public enum EItemLayout
    {
        ONEBYONE,
        ONEBYTWO,
        ONEBYTHREE,
        TWOBYTWO,
        TWOBYTHREE
    }

    protected string m_name;
    protected string m_description;
    protected string m_guid;
    protected uint m_id;
    protected int m_currentAmount;
    protected int m_maxStackAmount;
    protected bool m_isStackable;
    protected Sprite m_icon;
    protected LayoutSlot[] m_slotLayout;
    protected EItemLayout m_layout;

    public LayoutSlot[] SlotLayout => m_slotLayout;
    public bool CanStack => m_isStackable;
    public string GUID => m_guid;
    public uint ID => m_id;
    public int StackAmount => m_currentAmount;
    public bool IsStackFull => m_currentAmount == m_maxStackAmount;
    public int MaxStack => m_maxStackAmount;


    public Item(uint _id, string _name, string _desc, int _currentAmt = 0, bool _isStackable = false, int _maxStackAmount = 1, Sprite _icon = null, EItemLayout _layout = EItemLayout.TWOBYTWO)
    {
        m_id = _id;
        m_name = _name;
        m_description = _desc;
        m_layout = _layout;
        m_currentAmount = _currentAmt;
        m_isStackable = _isStackable;
        m_maxStackAmount = _maxStackAmount;
        m_icon = _icon;

        Init();
    }
    public Item(Item _otherItem, int _amt)
    {
        m_id = _otherItem.m_id;
        m_name = _otherItem.m_name;
        m_description = _otherItem.m_description;
        m_layout = _otherItem.m_layout;
        m_currentAmount = _amt;
        m_isStackable = _otherItem.m_isStackable;
        m_maxStackAmount = _otherItem.m_maxStackAmount;
        m_icon = _otherItem.m_icon;

        Init();
    }

    private void Init()
    {
        Guid g = Guid.NewGuid();
        m_guid = g.ToString();

        switch (m_layout)
        {
            case EItemLayout.ONEBYONE:
                m_slotLayout = new LayoutSlot[1] { new LayoutSlot(0, 0) };
                break;
            case EItemLayout.ONEBYTWO:
                m_slotLayout = new LayoutSlot[2] { new LayoutSlot(0, 0), new LayoutSlot(0, 1) };
                break;
            case EItemLayout.ONEBYTHREE:
                m_slotLayout = new LayoutSlot[3] { new LayoutSlot(0, 0), new LayoutSlot(0, 1), new LayoutSlot(0, 2) };
                break;
            case EItemLayout.TWOBYTWO:
                m_slotLayout = new LayoutSlot[4] { new LayoutSlot(0, 0), new LayoutSlot(1, 0), new LayoutSlot(0, 1), new LayoutSlot(1, 1) };
                break;
            case EItemLayout.TWOBYTHREE:
                m_slotLayout = new LayoutSlot[6] { new LayoutSlot(0, 0), new LayoutSlot(1, 0), new LayoutSlot(0, 1), new LayoutSlot(1, 1), new LayoutSlot(0, 2), new LayoutSlot(1, 2) };
                break;
            default:
                m_slotLayout = new LayoutSlot[0];
                break;
        }
    }

    public void AddToStack(ref int _amt)
    {
        if(m_currentAmount < m_maxStackAmount)
        {
            int maxToAdd = m_currentAmount - m_maxStackAmount;
            m_currentAmount += Math.Min(maxToAdd, _amt);
            _amt -= Math.Min(maxToAdd, _amt);
        }
    }

    public Item SplitStack(int _takeaway)
    {
        // Create new item with stack taken away
        Item newItem = new Item(this, _takeaway);
        // Take away amount from current item
        m_currentAmount -= _takeaway;
        return newItem;
    }
}