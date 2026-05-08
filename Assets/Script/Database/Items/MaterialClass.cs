using UnityEngine;
[CreateAssetMenu(fileName = "New Material", menuName = "Items/Material")]
public class MaterialClass : Items
{
    public override Items GetItem() { return null; }
    public override MaterialClass GetMaterial() { return this; }

}
