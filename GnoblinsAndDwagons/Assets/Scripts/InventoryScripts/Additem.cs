using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using System;

public class AddItem : MonoBehaviour
{
    public static event HandleItemPurchased OnItemPurchased;
    public delegate void HandleItemPurchased(Item item);
    public Item item;
    private ItemGenerator itemGenerator = new ItemGenerator();
    public void OnClick(){
        item = itemGenerator.generateItems(1)[0];
        OnItemPurchased?.Invoke(item);
    }
}
