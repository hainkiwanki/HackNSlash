using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField]
    private List<Item> m_Items = new List<Item>();

    [Button]
    public void AddItem1()
    {
        Inventory.Inst.AddItem(m_Items[0]);
    }

    [Button]
    public void AddItem2()
    {
        Inventory.Inst.AddItem(m_Items[1]);
    }

    [Button]
    public void AddItem3()
    {
        Inventory.Inst.AddItem(m_Items[2]);
    }

    [Button]
    public void AddItem4()
    {
        Inventory.Inst.AddItem(m_Items[3]);
    }
}
