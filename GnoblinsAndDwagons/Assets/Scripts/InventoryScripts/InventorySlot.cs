using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemThings;
using UnityEngine.EventSystems;
using System;

namespace inventoryThings
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        public Image icon;
        public Image border;
        public int itemId;
        public Image selectedShader;
        public bool thisItemSelected;
        private InventoryManager inventoryManager;
        private ShowEquippedItems showEquippedItems;
        private StatPanel statPanel;

        public static event Action OnInventoryItemClicked;

        private void Start()
        {
            inventoryManager = GameObject.Find("Inventory_panel").GetComponent<InventoryManager>();
            statPanel = GameObject.Find("Selected_Item_stats").GetComponent<StatPanel>();
            showEquippedItems = GameObject.Find("Equipped_Items").GetComponent<ShowEquippedItems>();
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
            statPanel.Draw();
            OnInventoryItemClicked?.Invoke();
        }
        private void OnRightClick() { }

        public void FakeLeftClick()
        {
            this.Start();
            showEquippedItems.DeselectAllSlots();
            inventoryManager.DeselectAllSlots(itemId);
            selectedShader.enabled = true;
            thisItemSelected = true;
            statPanel.Draw();
            OnInventoryItemClicked?.Invoke();
        }

        public void DeselectSlot()
        {
            this.Start();
            statPanel.Draw();
            OnInventoryItemClicked?.Invoke();
        }
    }
}
