using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemThings;
using UnityEngine.EventSystems;
using System;

namespace inventoryThings
{
    public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
    {
        public Image selectedShader;
        private InventoryManager inventoryManager;
        private StatPanel statPanel;
        [SerializeField] public PlayerInventory playerInventory;

        public static event Action OnClearSignal;
        public static event Action OnEquipmentSlotClicked;

        private void Start()
        {
            inventoryManager = GameObject.Find("Inventory_panel").GetComponent<InventoryManager>();
            statPanel = GameObject.Find("Selected_Item_stats").GetComponent<StatPanel>();
        }

        private void OnEnable()
        {
            EquipmentSlot.OnClearSignal += HandleClearSignal;
        }

        private void OnDisable()
        {
            EquipmentSlot.OnClearSignal -= HandleClearSignal;
        }
        public void ClearSlot()
        {
            selectedShader.enabled = false;
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
            OnClearSignal?.Invoke();
            inventoryManager.DeselectAllSlots();
            selectedShader.enabled = true;
            if (gameObject.name.ToString() == "EquippedHead" && this.playerInventory.equippedItems.equippedHead.getValue() != 0)
                this.playerInventory.selectedItem = new SelectedItem(this.playerInventory.equippedItems.equippedHead, Panel.Equipped_Items);
            if (gameObject.name.ToString() == "EquippedChest" && this.playerInventory.equippedItems.equippedChest.getValue() != 0)
                this.playerInventory.selectedItem = new SelectedItem(this.playerInventory.equippedItems.equippedChest, Panel.Equipped_Items);
            if (gameObject.name.ToString() == "EquippedBoots" && this.playerInventory.equippedItems.equippedBoots.getValue() != 0)
                this.playerInventory.selectedItem = new SelectedItem(this.playerInventory.equippedItems.equippedBoots, Panel.Equipped_Items);
            if (gameObject.name.ToString() == "EquippedMainHand" && this.playerInventory.equippedItems.equippedMainHand.getValue() != 0)
                this.playerInventory.selectedItem = new SelectedItem(this.playerInventory.equippedItems.equippedMainHand, Panel.Equipped_Items);
            if (gameObject.name.ToString() == "EquippedOffHand" && this.playerInventory.equippedItems.equippedOffHand.getValue() != 0)
                this.playerInventory.selectedItem = new SelectedItem(this.playerInventory.equippedItems.equippedOffHand, Panel.Equipped_Items);
            statPanel.Draw();
            OnEquipmentSlotClicked?.Invoke();
        }
        private void OnRightClick() { }

        private void HandleClearSignal()
        {
            this.ClearSlot();
        }
    }
}
