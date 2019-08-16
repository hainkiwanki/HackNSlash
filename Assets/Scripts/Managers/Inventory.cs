using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    private List<Item> m_itemsOwned;

    protected override void _OnAwake()
    {
        base._OnAwake();

        m_itemsOwned = new List<Item>();
    }

    void Update()
    {
        
    }

    public void AddItem(Item _item)
    {
        m_itemsOwned.Add(_item);
    }
}
