using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using System;

public class Inventory: MonoBehaviour
{
    public static event Action<List<Item>> OnInventoryChange;
    public List<Item> inventory = new List<Item>(35);


    private void OnEnable(){
        AddItem.OnItemPurchased += Add;
    }

    private void OnDisable(){
        AddItem.OnItemPurchased -= Add;
    }
    public void Add(Item item)
    {
        inventory.Add(item);
        OnInventoryChange?.Invoke(inventory);
        foreach (Item item1 in inventory)
        Debug.Log($"{item1.getName()}");
    }

    public void Remove(Item item)
    {
        inventory.Remove(item);
        OnInventoryChange?.Invoke(inventory);
    }
}
