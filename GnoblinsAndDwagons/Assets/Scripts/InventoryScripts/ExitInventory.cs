using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitInventoryController : MonoBehaviour
{
    InventoryManagerInventory inventoryManager;
    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory_panel").GetComponent<InventoryManagerInventory>();
    }
    public void OnClick()
    {
        inventoryManager.ExitInventory();
    }
}

