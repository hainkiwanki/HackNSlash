using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemAdded(Item _itemAdded);
    public OnItemAdded onItemAdded;

    private Item[,] m_items;
    protected int m_rows;
    protected int m_cols;
    protected Transform m_parentObj;

    public Inventory(int _rows, int _cols)
    {
        m_rows = _rows;
        m_cols = _cols;
        m_items = new Item[_rows, _cols];
    }

    public bool AddItem(Item _item, bool _skipStack = false)
    {
        bool canFit = GetAvailableSlots(_item.SlotLayout, out List<LayoutSlot> slotsUsed);

        if (!canFit)
            return false;

        if(_item.CanStack)
        {
            int amtToStackAway = _item.StackAmount;
            if (!_skipStack)
            {
                var similarItems = GetSimilarItems(_item, true);
                for (int i = 0; i < similarItems.Count; i++)
                {
                    similarItems[i].AddToStack(ref amtToStackAway);
                    if (amtToStackAway == 0)
                        break;
                }
            }

            // If there are still items left => have to create new stacks
            while (amtToStackAway != 0)
            {
                int amtTaking = Math.Min(_item.MaxStack, amtToStackAway);
                Item newItem = _item.SplitStack(amtTaking);
                AddToInventory(slotsUsed, newItem);
                return AddItem(_item, true);
            }
        }
        else
        {
            // Add item
            AddToInventory(slotsUsed, _item);
        }
        return true;
    }

    private bool GetAvailableSlots(LayoutSlot[] _itemLayoutSlots, out List<LayoutSlot> _availableSlots)
    {
        int slotAmt = _itemLayoutSlots.Length;
        _availableSlots = new List<LayoutSlot>();

        for (int r = 0; r < m_rows; r++)
        {
            for (int c = 0; c < m_cols; c++)
            {
                for (int s = 0; s < slotAmt; s++)
                {
                    LayoutSlot slot = _itemLayoutSlots[s];
                    int row = r + slot.y;
                    int col = c + slot.x;
                    // If it doesn't fit
                    if(row > m_rows || col > m_cols || m_items[col, row] != null)
                    {
                        // Next column check
                        c = m_cols;
                        _availableSlots.Clear();
                        break;
                    }
                    // If it does fit
                    _availableSlots.Add(slot);
                    if(_availableSlots.Count == slotAmt)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Returns a list of items similar to the given item.
    /// </summary>
    /// <param name="_item">Item that will be compared to</param>
    /// <param name="_checkStack">If true, it will check if item is stackable and stuff can be added to it</param>
    /// <returns></returns>
    private List<Item> GetSimilarItems(Item _item, bool _checkStack = false)
    {
        List<Item> items = new List<Item>();

        for (int row = 0; row < m_rows; row++)
        {
            for (int col = 0; col < m_cols; col++)
            {
                var currItem = m_items[col, row];
                // If item is not already in list and it's the same item ID
                if (!items.Contains(currItem) && currItem.ID == _item.ID)
                {
                    if ((!_checkStack) || (_checkStack && !currItem.IsStackFull && currItem.CanStack))
                    {
                        items.Add(currItem);
                    }
                }
            }
        }

        return items;
    }

    private void AddToInventory(List<LayoutSlot> _slotsToTake, Item _item)
    {
        // Add cache data
        for (int i = 0; i < _slotsToTake.Count; i++)
        {
            LayoutSlot slot = _slotsToTake[i];
            m_items[slot.x, slot.y] = _item;
        }
        onItemAdded.Invoke(_item);
    }
}
