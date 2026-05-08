using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BaseInventory : MonoBehaviour
{
    public List<Items> items = new List<Items>();

    public virtual void AddItem(Items item)
    {
        items.Add(item);
        RefreshUI();
    }
    public virtual void RemoveItem(Items item)
    {
        items.Remove(item);
        RefreshUI();
    }
    public virtual void RefreshUI()
    {
    }
}
