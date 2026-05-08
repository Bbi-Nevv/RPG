using UnityEngine;

public abstract class Items : ScriptableObject
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;

    public abstract Items GetItem();
    public abstract MaterialClass GetMaterial();
}
