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
    public uint m_uuid;
    public string m_itemName;
    public Sprite m_icon;
    public EItemSize m_itemSize;
    public bool isStackable = false;

    protected virtual void Use() { }
}
