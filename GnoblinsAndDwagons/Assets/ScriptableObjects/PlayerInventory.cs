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
    [SerializeField] public CombatStats playerStats;


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
            if (equippedItems.equippedMainHand.getValue() != 0)
				oldEquippedItem = equippedItems.equippedMainHand;
            equippedItems.equippedMainHand = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }

        if(selectedItem.selectedItem.getSlot() == Slot.OffHand)
        {
            if (equippedItems.equippedOffHand.getValue() != 0)
				oldEquippedItem = equippedItems.equippedOffHand;
            equippedItems.equippedOffHand = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }

        if(selectedItem.selectedItem.getSlot() == Slot.HeadWear)
        {
            if (equippedItems.equippedHead.getValue() != 0)
				oldEquippedItem = equippedItems.equippedHead;
            equippedItems.equippedHead = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }

        if(selectedItem.selectedItem.getSlot() == Slot.Armor)
        {
            if (equippedItems.equippedChest.getValue() != 0)
				oldEquippedItem = equippedItems.equippedChest;
            equippedItems.equippedChest = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }

        if(selectedItem.selectedItem.getSlot() == Slot.FootWear)
        {
            if (equippedItems.equippedBoots.getValue() != 0 )
				oldEquippedItem = equippedItems.equippedBoots;
            equippedItems.equippedBoots = selectedItem.selectedItem;
            OnEquipmentChange?.Invoke();
        }
        inventory.Remove(selectedItem.selectedItem);
        selectedItem = null;
        if (oldEquippedItem != null)
        {
            inventory.Add(oldEquippedItem);
        }
        OnInventoryChange?.Invoke(inventory);
        playerStats.Strength = equippedItems.getTotalStrength()+2;
        playerStats.Toughness = equippedItems.getTotalToughness()+2;
        playerStats.Dexterity = equippedItems.getTotalDexterity()+2;
        playerStats.Agility = equippedItems.getTotalAgility()+2;
    }

    public void Remove(Item item)
    {
        inventory.Remove(item);
        OnInventoryChange?.Invoke(inventory);
    }

}
