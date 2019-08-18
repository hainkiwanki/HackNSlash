using UnityEngine;

public enum EItemSize
{
    OneByOne,
    OneByTwo,
    OneByThree,
    TwoByTwo,
    TwoByThree,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public uint UUID => m_uuid;
    public string Name => m_itemName;
    public Sprite Icon => m_icon;
    public EItemSize Size => m_itemSize;

    [SerializeField] uint m_uuid;
    [SerializeField] string m_itemName;
    [SerializeField] Sprite m_icon;
    [SerializeField] EItemSize m_itemSize;

    protected virtual void Use() { }
}
