using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventoryThings
{
    public class ExitInventoryController : MonoBehaviour
    {
        InventoryManager inventoryManager;
        private void Start()
        {
            inventoryManager = GameObject.Find("Inventory_panel").GetComponent<InventoryManager>();
        }
        public void OnClick()
        {
            inventoryManager.ExitInventory();
        }
    }
}
