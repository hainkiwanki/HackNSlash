using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField]
    private List<Item> m_Items = new List<Item>();

    [Button("Add item 1")]
    public void AddItem1()
    {
        PlayerInventory.Inst.AddItem(m_Items[0]);
    }

    [Button("Add item 2")]
    public void AddItem2()
    {
        PlayerInventory.Inst.AddItem(m_Items[1]);
    }

    [Button("Add item 3")]
    public void AddItem3()
    {
        PlayerInventory.Inst.AddItem(m_Items[2]);
    }

    [Button("Add item 4")]
    public void AddItem4()
    {
        PlayerInventory.Inst.AddItem(m_Items[3]);
    }
}
