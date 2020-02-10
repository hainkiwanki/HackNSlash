using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public enum EItemSize
    {
        OneByOne,
        OneByTwo,
        OneByThree,
        TwoByTwo,
        TwoByThree,
    }

    public uint m_uuid;
    public string m_itemName;
    public Sprite m_icon;
    [SerializeField]
    private EItemSize m_gridLayout;
    public bool m_isStackable = false;

    protected virtual void Use() { }

    public Slot[] GetGridLayout()
    {
        return GetGridLayout(m_gridLayout);
    }

    static public Slot[] GetGridLayout(EItemSize _itemSize)
    {
        switch (_itemSize)
        {
            case EItemSize.OneByOne:
                return new Slot[1] { new Slot(0, 0) };
            case EItemSize.OneByThree:
                return new Slot[3] { new Slot(0, 0), new Slot(0, 1), new Slot(0, 2) };
            case EItemSize.OneByTwo:
                return new Slot[2] { new Slot(0, 0), new Slot(0, 1) };
            case EItemSize.TwoByThree:
                return new Slot[6] { new Slot(0, 0), new Slot(1, 0), new Slot(0, 1), new Slot(1, 1), new Slot(0, 2), new Slot(1, 2) };
            case EItemSize.TwoByTwo:
                return new Slot[4] { new Slot(0, 0), new Slot(1, 0), new Slot(0, 1), new Slot(1, 1) };
            default:
                return new Slot[0];
        }
    }
}
