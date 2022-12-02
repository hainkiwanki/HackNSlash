using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Slot
{
    public int x;
    public int y;
    public Item itemRef;

    public Slot(int _x = -1, int _y = -1, Item _itemRef = null)
    {
        x = _x;
        y = _y;
        itemRef = _itemRef;
    }
}

public class Inventory : Singleton<Inventory>
{
    private const int C_INVENTORY_COLUMNS = 9;
    private const int C_INVENTORY_ROWS = 6;
    private const float C_SLOT_SIZE = 25.0f;

    [SerializeField] Transform m_itemGridLayer;
    [SerializeField] ItemUI m_itemUIPrefab;


    private Item[,] m_inventorySlots;

    protected override void _OnAwake()
    {
        base._OnAwake();

        m_inventorySlots = new Item[C_INVENTORY_COLUMNS, C_INVENTORY_ROWS];
    }

    public void AddItem(Item _item)
    {
        List<Slot> slotResult = new List<Slot>();
        bool canFit = CheckInventorySlots(_item.GetGridLayout(), ref slotResult);
        if(canFit)
        {
            AddToInventory(slotResult, _item);
        }
        else
        {
            Debug.Log("Does not fit in inventory");
            // Logger.LogError("Could not fit item in inventory", Color.red);
        }
    }

    private bool CheckInventorySlots(Slot[] _itemGridLayout, ref List<Slot> _outSlots)
    {
        for(int row = 0; row < C_INVENTORY_ROWS; row++) // y
        {
            for (int column = 0; column < C_INVENTORY_COLUMNS; column++) // x
            {
                for (int i = 0; i < _itemGridLayout.Length; i++)
                {
                    Slot slot = _itemGridLayout[i];
                    int x = column + slot.x;
                    int y = row + slot.y;
                    if (x >= C_INVENTORY_COLUMNS || y >= C_INVENTORY_ROWS || m_inventorySlots[x, y] != null)
                    {
                        break;
                    }

                    _outSlots.Add(new Slot(x, y));
                }
                if (_outSlots.Count == _itemGridLayout.Length)
                    return true;
                else
                    _outSlots.Clear();
            }
            if (_outSlots.Count == _itemGridLayout.Length)
                return true;
            else
                _outSlots.Clear();
        }

        return false;
    }

    private void AddToInventory(List<Slot> _slots, Item _item)
    {
        // Code wise
        foreach(var slot in _slots)
        {
            m_inventorySlots[slot.x, slot.y] = _item;
        }
        // GUI wise
        ItemUI itemUI = Instantiate(m_itemUIPrefab, m_itemGridLayer);
        var width = ((_slots[_slots.Count - 1].x - _slots[0].x) + 1) * C_SLOT_SIZE;
        var height = ((_slots[_slots.Count - 1].y - _slots[0].y) + 1) * C_SLOT_SIZE;
        itemUI.Init(_item, new Vector2(width, height), new Vector3(_slots[0].x * C_SLOT_SIZE, -_slots[0].y * C_SLOT_SIZE));
    }
}
