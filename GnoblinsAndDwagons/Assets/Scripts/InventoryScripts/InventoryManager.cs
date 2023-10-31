using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class InventoryManagerInventory : MonoBehaviour
{
    public static InventoryManagerInventory instance { get; private set; }
    public GameObject slotPrefab;
    private DisplayPlayerStatsInventory displayPlayerStats;
    public List<InventorySlotInventory> inventorySlots = new List<InventorySlotInventory>(35);
    public event Action inInventory;
    public event Action leaveInventory;
    [SerializeField] public PlayerInventory playerInventory;

    private void OnEnable()
    {
        PlayerInventory.OnInventoryChange += DrawInventory;
        PlayerInventory.ClearSelection += DeselectSlotsCall;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        displayPlayerStats = GameObject.Find("Player_stats").GetComponent<DisplayPlayerStatsInventory>();
        inInventory?.Invoke();
        DrawInventory(playerInventory.inventory);
    }

    public void ExitInventory()
    {
        leaveInventory?.Invoke();
        SceneManager.UnloadSceneAsync("Inventory");
    }

    private void OnDisable()
    {
        PlayerInventory.OnInventoryChange -= DrawInventory;
        PlayerInventory.ClearSelection -= DeselectSlotsCall;
    }

    void ResetInventory()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InventorySlotInventory>(35);
    }

    void DrawInventory(List<Item> inventory)
    {
        ResetInventory();
        displayPlayerStats.Draw();
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

        InventorySlotInventory newSlotComponent = newSlot.GetComponent<InventorySlotInventory>();
        newSlotComponent.ClearSlot();

        inventorySlots.Add(newSlotComponent);
    }

    public void DeselectAllSlots(int id = -1)
    {
        foreach (InventorySlotInventory slot in inventorySlots)
        {
            slot.selectedShader.enabled = false;
            slot.thisItemSelected = false;
        }
        bool itemFound = false;
        foreach (Item item in playerInventory.inventory)
        {
            if (item.specificId == id)
            {
                playerInventory.selectedItem = new SelectedItem(item, Panel.Inventory);
                itemFound = true;
            }

        }
        if (!itemFound)
            playerInventory.selectedItem = null;
    }

    private void SelectItem(Item item)
    {
        foreach (InventorySlotInventory slot in inventorySlots)
        {
            if (item.specificId == slot.itemId)
            {
                slot.FakeLeftClick();
            }

        }
    }

    private void DeselectSlotsCall()
    {
        foreach (InventorySlotInventory slot in inventorySlots)
        {
            slot.selectedShader.enabled = false;
            slot.thisItemSelected = false;
        }
        inventorySlots[0].DeselectSlot();
    }
}

