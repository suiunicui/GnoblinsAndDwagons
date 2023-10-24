using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemThings;
using UnityEngine.EventSystems;
using System;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Image border;
    public int itemId;
    public Image selectedShader;
    public bool thisItemSelected;
    private ShopManager shopManager;
    private InventoryManager inventoryManager;
    private StatPanel statPanel;
    private ShowEquippedItems showEquippedItems;

    public static event Action OnShopItemClicked;


    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory_panel").GetComponent<InventoryManager>();
        shopManager = GameObject.Find("Shop_panel").GetComponent<ShopManager>();
        statPanel = GameObject.Find("Selected_Item_stats").GetComponent<StatPanel>();
        showEquippedItems = GameObject.Find("Equipped_Items").GetComponent<ShowEquippedItems>();
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
        if (item==null){
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
        inventoryManager.DeselectAllSlots();
        shopManager.DeselectAllSlots(itemId);
        showEquippedItems.DeselectAllSlots();
        selectedShader.enabled= true;
        thisItemSelected = true;
        statPanel.Draw();
        OnShopItemClicked?.Invoke();
    }
    private void OnRightClick(){}

    public void DeselectSlot()
    {
        this.Start();
        statPanel.Draw();
    }
}
