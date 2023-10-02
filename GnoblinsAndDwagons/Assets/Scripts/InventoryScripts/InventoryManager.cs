using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>(35);
    [SerializeField] public PlayerInventory playerInventory;

    private void OnEnable(){
        PlayerInventory.OnInventoryChange += DrawInventory;
    }

    private void Start(){
        List<Item> emptyList = new List<Item>();
        DrawInventory(emptyList);
    }

    private void OnDisable(){
        PlayerInventory.OnInventoryChange -= DrawInventory;
    }

    void ResetInventory()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InventorySlot>(35);
    }

    void DrawInventory(List<Item> inventory)
    {
        ResetInventory();

        for (int i = 0; i < inventorySlots.Capacity; i++)
        {
            CreateInventorySlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();

        inventorySlots.Add(newSlotComponent);
    }

    public void DeselectAllSlots(int id)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.selectedShader.enabled = false;
            slot.thisItemSelected = false;
        }
        bool itemFound = false;
        foreach (Item item in playerInventory.inventory)
        {
            if (item.specificId == id)
            {
                playerInventory.selectedItem = item;
                itemFound = true;
            }

        }
        if (!itemFound)
        playerInventory.selectedItem = null;
    }
}
