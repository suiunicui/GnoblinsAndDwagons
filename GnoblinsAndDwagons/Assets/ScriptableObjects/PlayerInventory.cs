using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using System;

[CreateAssetMenu(fileName = "PlayerInventory",menuName = "Persistance/Inventory") ]
public class PlayerInventory : ScriptableObject
{
    public static event Action<List<Item>> OnInventoryChange;
    public static event Action OnEquipmentChange;
    public SelectedItem selectedItem;
    public List<Item> inventory = new List<Item>(35);
    public PlayerEquippedItems equippedItems = new PlayerEquippedItems();


    private void OnEnable(){
        AddItem.OnItemPurchased += Add;
        EquipItem.OnItemEquipped += Equip;
    }

    private void OnDisable(){
        AddItem.OnItemPurchased -= Add;
        EquipItem.OnItemEquipped -= Equip;
    }
    public void Add(Item item)
    {
        inventory.Add(item);
        OnInventoryChange?.Invoke(inventory);
    }

    public void Equip()
    {
        Item oldEquippedItem = null;
        if(selectedItem.selectedItem.getSlot() == Slot.MainHand)
        {
            if (equippedItems.equippedMainHand != null)
            oldEquippedItem = equippedItems.equippedMainHand;
            equippedItems.equippedMainHand = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }

        if(selectedItem.selectedItem.getSlot() == Slot.OffHand)
        {
            if (equippedItems.equippedOffHand != null)
            oldEquippedItem = equippedItems.equippedOffHand;
            equippedItems.equippedOffHand = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }

        if(selectedItem.selectedItem.getSlot() == Slot.HeadWear)
        {
            oldEquippedItem = equippedItems.equippedHead;
            equippedItems.equippedHead = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }

        if(selectedItem.selectedItem.getSlot() == Slot.Armor)
        {
            if (equippedItems.equippedChest!= null)
            oldEquippedItem = equippedItems.equippedChest;
            equippedItems.equippedChest = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }

        if(selectedItem.selectedItem.getSlot() == Slot.FootWear)
        {
            oldEquippedItem = equippedItems.equippedBoots;
            equippedItems.equippedBoots = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }
        inventory.Remove(selectedItem.selectedItem);
        if (oldEquippedItem != null)
        {
            inventory.Add(oldEquippedItem);
        }
        OnInventoryChange?.Invoke(inventory);
        Debug.Log($"Player Strength: {equippedItems.getTotalStrength()}");
        Debug.Log($"Player Toughness: {equippedItems.getTotalToughness()}");
        Debug.Log($"Player Dexterity: {equippedItems.getTotalDexterity()}");
        Debug.Log($"Player Agility: {equippedItems.getTotalAgility()}");

    }

    public void Remove(Item item)
    {
        inventory.Remove(item);
        OnInventoryChange?.Invoke(inventory);
    }

}
