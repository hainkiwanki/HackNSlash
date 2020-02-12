using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    private static int C_ROWS = 6;
    private static int C_COLS = 9;
    private static int C_TABS = 3;

    private Inventory[] m_inventories;

    protected override void _OnAwake()
    {
        m_inventories = new Inventory[3];
        for (int i = 0; i < C_TABS; i++)
        {
            m_inventories[i] = new Inventory(C_ROWS, C_COLS);
        }
    }

    public void AddItem(Item _item)
    {
        bool successfullyAdded = false;
        for (int i = 0; i < C_TABS; i++)
        {
            var inv = m_inventories[i];
            if (inv.AddItem(_item))
            {
                successfullyAdded = true;
                break;
            }
        }

        if(!successfullyAdded)
        {
            Logger.LogError("Couldn't add item to inventory", Color.red);
        }
    }
}
