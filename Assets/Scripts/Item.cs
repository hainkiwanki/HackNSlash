using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public uint UUID => m_uuid;
    public string Name => m_itemName;
    public Sprite Icon => m_icon;

    [SerializeField] uint m_uuid;
    [SerializeField] string m_itemName;
    [SerializeField] Sprite m_icon;

    protected virtual void Use() { }
}
