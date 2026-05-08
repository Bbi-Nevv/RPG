using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerInventory : BaseInventory
{
    public static ManagerInventory instance;
    public GameObject[] slots;
    public int[] quantityOfItem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        foreach (var slot in slots)
        {

            slot.SetActive(false);
        }
        quantityOfItem = new int[slots.Length];

        if (items.Count > 0) DisplayDescription(0);



        RefreshUI();
    }
    public override void AddItem(Items item)
    {
        if (items.Contains(item))
        {
            int index = items.IndexOf(item);
            quantityOfItem[index]++;
        }
        else
        {
            items.Add(item);
            quantityOfItem[items.Count - 1] = 1;
        }
        RefreshUI();
    }
    public override void RemoveItem(Items item)
    {
        if (items.Contains(item))
        {
            int index = items.IndexOf(item);
            Debug.Log("Remove " + item.itemName + " at index " + index);
            quantityOfItem[index]--;
            if (quantityOfItem[index] <= 0)
            {
                for (int i = index; i < quantityOfItem.Length-1; i++)
                {
                    int flap = quantityOfItem[i + 1];
                    quantityOfItem[i + 1] = quantityOfItem[i];
                    quantityOfItem[i] = flap;
                }
                items.Remove(item);
            }
        }
        RefreshUI();
    }
    public override void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (quantityOfItem[i] <= 0)
            {
                slots[i].SetActive(false);
            }
            else slots[i].SetActive(true);
            if(i < items.Count)
            {
                slots[i].GetComponent<ItemUI>().itemIconUI.sprite = items[i].itemIcon;
                slots[i].GetComponent<ItemUI>().itemQuantityUI.text = quantityOfItem[i].ToString();
            }      
        }
    }

    public void DisplayDescription(int index)
    {
        slots[index].GetComponent<ItemUI>().itemDescriptionUI.text = items[index].itemDescription;
        slots[index].GetComponent<ItemUI>().itemNameUI.text = items[index].itemName;
    }
}
