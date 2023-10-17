using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>(35);
    public Text goldText;
    [SerializeField] public PlayerInventory playerInventory;

    private void OnEnable(){
        PlayerInventory.OnInventoryChange += DrawInventory;
        PlayerInventory.ItemBought += SelectItem;
        PlayerInventory.ClearSelection += DeselectSlotsCall;
    }

    private void Start(){
        DrawInventory(playerInventory.inventory);
        goldText.text = "Gold: " + playerInventory.gold;
    }

    private void OnDisable(){
        PlayerInventory.OnInventoryChange -= DrawInventory;
        PlayerInventory.ItemBought -= SelectItem;
        PlayerInventory.ClearSelection -= DeselectSlotsCall;
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

    public void DeselectAllSlots(int id =-1)
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
                playerInventory.selectedItem = new SelectedItem(item,Panel.Inventory);
                itemFound = true;
            }

        }
        if (!itemFound)
        playerInventory.selectedItem = null;
    }

    private void SelectItem(Item item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (item.specificId == slot.itemId)
            {
                slot.FakeLeftClick();
            }

        }
    }

    private void DeselectSlotsCall()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.selectedShader.enabled = false;
            slot.thisItemSelected = false;
        }
        inventorySlots[0].DeselectSlot();
    }
}
