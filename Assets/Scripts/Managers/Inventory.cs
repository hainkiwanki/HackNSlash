using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Slot
{
    public int x;
    public int y;
    public bool isFree;

    public Slot(int _x = 0, int _y = 0)
    {
        x = _x;
        y = _y;
        isFree = true;
    }
}

public struct GridLayout
{
    public int[,] slotLayout;
    public float width;
    public float height;

    public Vector2 ImageSize => new Vector2(width, height);

    public GridLayout(int[,] _slots, float _w, float _h)
    {
        slotLayout = _slots;
        width = _w;
        height = _h;
    }
}

public class Inventory : Singleton<Inventory>
{
    [SerializeField] Transform m_itemGridLayer;
    [SerializeField] ItemUI m_itemUIPrefab;

    private const int INVENTORY_COLUMNS = 20;
    private const int INVENTORY_ROWS = 6;
    private const float SLOT_SIZE = 20.0f;
    private List<Item> m_itemsOwned;

    private Dictionary<EItemSize, GridLayout> m_itemGridLayouts;
    private List<Slot> m_inventorySlots;

    protected override void _OnAwake()
    {
        base._OnAwake();

        m_itemsOwned = new List<Item>();
        m_itemGridLayouts = new Dictionary<EItemSize, GridLayout>
        {
            { EItemSize.OneByOne,   new GridLayout( new int[1,2]{ { 0, 0 } }, 
                                    SLOT_SIZE, SLOT_SIZE) },
            { EItemSize.OneByTwo,   new GridLayout( new int[2,2]{ { 0, 0 }, { 0, 1 } }, 
                                    SLOT_SIZE, 2 * SLOT_SIZE) },
            { EItemSize.OneByThree, new GridLayout( new int[3,2]{ { 0, 0 }, { 0, 1 }, { 0, 2 } }, 
                                    SLOT_SIZE, 3 * SLOT_SIZE) },
            { EItemSize.TwoByTwo,   new GridLayout( new int[4,2]{ { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } }, 
                                    2 * SLOT_SIZE, 2 * SLOT_SIZE) },
            { EItemSize.TwoByThree, new GridLayout( new int[6,2]{ { 0, 0 }, { 0, 1 }, { 0, 2 }, { 1, 0 }, { 1, 1 }, { 1, 2 } }, 
                                    2 * SLOT_SIZE, 3 * SLOT_SIZE) },
        };
        m_inventorySlots = new List<Slot>();
        for(int i = 0; i < INVENTORY_ROWS; i++)
        {
            for(int j = 0; j < INVENTORY_COLUMNS; j++)
            {
                m_inventorySlots.Add(new Slot(j, i));
            }
        }
    }

    public void AddItem(Item _item)
    {
        var slots = m_itemGridLayouts[_item.m_itemSize].slotLayout;
        for (int i = 0; i < INVENTORY_ROWS; i++)
        {
            for (int j = 0; j < INVENTORY_COLUMNS; j++)
            {
                var gridSlots = CheckIfFit(j, i, slots);
                if (gridSlots.Count <= 0)
                    continue;
                else
                {
                    AddItem(gridSlots, _item);
                    return;
                }
            }
        }
    }

    private void AddItem(List<int> _slotIndices, Item _item)
    {
        var slotAmt = _slotIndices.Count;
        for (int i = 0; i < slotAmt; i++)
        {
            var slot = m_inventorySlots[_slotIndices[i]];
            slot.isFree = false;
            m_inventorySlots[_slotIndices[i]] = slot;
        }
        var firstSlot = m_inventorySlots[_slotIndices[0]];
        m_itemsOwned.Add(_item);
        var item = Instantiate(m_itemUIPrefab, m_itemGridLayer);
        item.Init(_item, m_itemGridLayouts[_item.m_itemSize].ImageSize);
        item.SetImagePosition(new Vector3(SLOT_SIZE * firstSlot.x, -SLOT_SIZE * firstSlot.y));
    }

    private List<int> CheckIfFit(int _x, int _y, int[,] _slots)
    {
        List<int> indices = new List<int>();
        for(int i = 0; i < _slots.GetLength(0); i++)
        {
            int index = SlotToIndex(_x + _slots[i,0], _y + _slots[i,1]);
            if (IsSlotFree(index) == false)
            {
                indices.Clear();
                return indices;
            }

            indices.Add(index);
        }
        return indices;
    }

    private bool IsSlotFree(int _index)
    {
        return m_inventorySlots[_index].isFree;
    }

    private Slot IndexToSlot(int _index)
    {
        int amountOfSlots = m_inventorySlots.Count;
        if(_index > amountOfSlots)
        {
            Logger.LogError("Out of bounds index", Color.red);
            return new Slot(-1, -1);
        }

        int row = (int)(_index / INVENTORY_COLUMNS);
        int column = _index % INVENTORY_COLUMNS;
        for (int i = 0; i < amountOfSlots; i++)
        {
            if (m_inventorySlots[i].x == row && m_inventorySlots[i].y == column)
                return m_inventorySlots[i];
        }

        Logger.LogError("Something went horribly wrong", Color.red);
        return new Slot(-1, -1);
    }

    private int SlotToIndex(Slot _slot)
    {
        return SlotToIndex(_slot.x, _slot.y);
    }

    private int SlotToIndex(int _x, int _y)
    {
        return _y * INVENTORY_COLUMNS + _x;
    }
}
