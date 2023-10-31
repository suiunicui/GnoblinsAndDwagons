using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemThings;
using UnityEngine.EventSystems;
using System;


public class InventorySlotInventory : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Image border;
    public int itemId;
    public Image selectedShader;
    public bool thisItemSelected;
    private InventoryManagerInventory inventoryManager;
    private ShowEquippedItemsInventory showEquippedItems;
    private StatPanelInventory selected_Item_Panel;
    private EquippedItemStatsInventory equipped_Item_Panel;
    private DisplayPlayerStatsInventory displayPlayerStats;

    public static event Action OnInventoryItemClicked;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory_panel").GetComponent<InventoryManagerInventory>();
        selected_Item_Panel = GameObject.Find("Selected_Item_stats").GetComponent<StatPanelInventory>();
        equipped_Item_Panel = GameObject.Find("Equipped_Item_stats").GetComponent<EquippedItemStatsInventory>();
        displayPlayerStats = GameObject.Find("Player_stats").GetComponent<DisplayPlayerStatsInventory>();
        showEquippedItems = GameObject.Find("Equipped_Items").GetComponent<ShowEquippedItemsInventory>();
        OnInventoryItemClicked?.Invoke();
    }
    public void ClearSlot()
    {
        icon.enabled = false;
        border.enabled = true;
        itemId = -1;
        selectedShader.enabled = false;
        thisItemSelected = false;
    }

    public void DrawSlot(Item item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }
        icon.enabled = true;
        border.enabled = true;
        icon.sprite = item.icon;
        this.itemId = item.specificId;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        showEquippedItems.DeselectAllSlots();
        inventoryManager.DeselectAllSlots(itemId);
        selectedShader.enabled = true;
        thisItemSelected = true;
        selected_Item_Panel.Draw();
        equipped_Item_Panel.Draw();
        OnInventoryItemClicked?.Invoke();
        displayPlayerStats.Draw();
    }
    private void OnRightClick() { }

    public void FakeLeftClick()
    {
        this.Start();
        showEquippedItems.DeselectAllSlots();
        inventoryManager.DeselectAllSlots(itemId);
        selectedShader.enabled = true;
        thisItemSelected = true;
        selected_Item_Panel.Draw();
        equipped_Item_Panel.Draw();
        OnInventoryItemClicked?.Invoke();
        displayPlayerStats.Draw();
    }

    public void DeselectSlot()
    {
        this.Start();
        selected_Item_Panel.Draw();
        equipped_Item_Panel.Draw();
        OnInventoryItemClicked?.Invoke();
    }
}
