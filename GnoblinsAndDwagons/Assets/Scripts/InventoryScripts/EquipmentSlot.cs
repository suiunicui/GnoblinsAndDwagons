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
        private StatPanel selected_Item_Panel;
        private EquippedItemStats equipped_Item_Panel;
        private DisplayPlayerStats displayPlayerStats;
        [SerializeField] public PlayerInventory playerInventory;

        public static event Action OnClearSignal;
        public static event Action OnEquipmentSlotClicked;

        private void Start()
        {
            selected_Item_Panel = GameObject.Find("Selected_Item_stats").GetComponent<StatPanel>();
            equipped_Item_Panel = GameObject.Find("Equipped_Item_stats").GetComponent<EquippedItemStats>();
            displayPlayerStats = GameObject.Find("Player_stats").GetComponent<DisplayPlayerStats>();
        }

        private void OnEnable()
        {
            OnClearSignal += HandleClearSignal;
        }

        private void OnDisable()
        {
            OnClearSignal -= HandleClearSignal;
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
            selected_Item_Panel.Draw();
            equipped_Item_Panel.Draw();
            OnEquipmentSlotClicked?.Invoke();
            displayPlayerStats.Draw();
        }
        private void OnRightClick() { }

        private void HandleClearSignal()
        {
            this.ClearSlot();
        }
    }
}
