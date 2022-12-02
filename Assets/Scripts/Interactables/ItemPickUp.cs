using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item m_item;

    protected override void Interact()
    {
        Inventory.Inst.AddItem(m_item);

        base.Interact();
    }
}
