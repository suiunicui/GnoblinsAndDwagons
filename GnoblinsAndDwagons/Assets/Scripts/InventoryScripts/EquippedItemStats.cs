using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemThings;

namespace inventoryThings
{
    public class EquippedItemStats : MonoBehaviour
    {
        [SerializeField] public PlayerInventory playerInventory;
        public Text Name;
        public Text Strength;
        public Text Toughness;
        public Text Dexterity;
        public Text Agility;
        public Text Slot;
        public Text Value;
        public void ClearSlot()
        {
            Name.text = "No item selected!";
            Strength.text = "";
            Toughness.text = "";
            Dexterity.text = "";
            Agility.text = "";
            Slot.text = "";
            Value.text = "";
        }

        public void Draw()
        {
            Item item = null;
            if (playerInventory.selectedItem == null)
            {
                ClearSlot();
                return;
            }
            if(playerInventory.selectedItem.selectedItem.getSlot() == ItemThings.Slot.MainHand)
            {
                if(playerInventory.equippedItems.equippedMainHand == null)
                {
                    ClearSlot();
                    return;
                }
                else
                item = playerInventory.equippedItems.equippedMainHand;
            }
            else if(playerInventory.selectedItem.selectedItem.getSlot() == ItemThings.Slot.OffHand)
            {
                if(playerInventory.equippedItems.equippedOffHand == null)
                {
                    ClearSlot();
                    return;
                }
                else
                item = playerInventory.equippedItems.equippedOffHand;
            }
            else if(playerInventory.selectedItem.selectedItem.getSlot() == ItemThings.Slot.HeadWear)
            {
                if(playerInventory.equippedItems.equippedHead == null)
                {
                    ClearSlot();
                    return;
                }
                else
                item = playerInventory.equippedItems.equippedHead;
            }
            else if(playerInventory.selectedItem.selectedItem.getSlot() == ItemThings.Slot.Armor)
            {
                if(playerInventory.equippedItems.equippedChest == null)
                {
                    ClearSlot();
                    return;
                }
                else
                item = playerInventory.equippedItems.equippedChest;
            }
            else if(playerInventory.selectedItem.selectedItem.getSlot() == ItemThings.Slot.FootWear)
            {
                if(playerInventory.equippedItems.equippedBoots == null)
                {
                    ClearSlot();
                    return;
                }
                else
                item = playerInventory.equippedItems.equippedBoots;
            }

            Name.text = item.getName();
            Strength.text = "Strength: " + item.getStrength().ToString();
            Toughness.text = "Toughness: " + item.getToughness().ToString();
            Dexterity.text = "Dexterity: " + item.getDexterity().ToString();
            Agility.text = "Agility: " + item.getAgility().ToString();
            Slot.text = "Slot: " + item.getSlot().ToString();
            Value.text = "Value: " + item.getValue().ToString();
        }
    }
}
