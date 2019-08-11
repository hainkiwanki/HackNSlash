using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/")]
public class Item : ScriptableObject
{
    public delegate void OnItemPickUp();
    public OnItemPickUp onItemPickUpCallback;

    
}
