using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using System;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Persistance/Inventory")]
public class PlayerInventory : ScriptableObject
{
	public static event Action<List<Item>> OnInventoryChange;
	public static event Action OnEquipmentChange;
	public static event Action<Item> ItemBought;
	public static event Action ClearSelection;
	public static event Action ShopLevelUp;
	public SelectedItem selectedItem;
	public List<Item> inventory = new List<Item>(35);
	public PlayerEquippedItems equippedItems = new PlayerEquippedItems();
	public int gold = 200;
	public int shopLevel = 0;
	[SerializeField] public CombatStats playerStats;


	private void OnEnable()
	{
		AddItem.OnItemPurchased += Add;
		EquipItem.OnItemEquipped += Equip;
		EquipItem.OnItemUnequipped += Unequip;
		SellItem.OnItemSold += Remove;
	}

	private void OnDisable()
	{
		AddItem.OnItemPurchased -= Add;
		EquipItem.OnItemEquipped -= Equip;
		EquipItem.OnItemUnequipped -= Unequip;
		SellItem.OnItemSold -= Remove;
	}
	public void Add(Item item)
	{
		gold = gold - item.getShopValue();
		inventory.Add(item);

		OnInventoryChange?.Invoke(inventory);
		ItemBought?.Invoke(item);
	}

	public void Equip()
	{
		Item oldEquippedItem = null;
		if (selectedItem.selectedItem.getSlot() == Slot.MainHand)
		{
			if (equippedItems.equippedMainHand.getValue() != 0)
				oldEquippedItem = equippedItems.equippedMainHand;
			equippedItems.equippedMainHand = selectedItem.selectedItem;
			OnEquipmentChange?.Invoke();
		}

		if (selectedItem.selectedItem.getSlot() == Slot.OffHand)
		{
			if (equippedItems.equippedOffHand.getValue() != 0)
				oldEquippedItem = equippedItems.equippedOffHand;
			equippedItems.equippedOffHand = selectedItem.selectedItem;
			OnEquipmentChange?.Invoke();
		}

		if (selectedItem.selectedItem.getSlot() == Slot.HeadWear)
		{
			if (equippedItems.equippedHead.getValue() != 0)
				oldEquippedItem = equippedItems.equippedHead;
			equippedItems.equippedHead = selectedItem.selectedItem;
			OnEquipmentChange?.Invoke();
		}

		if (selectedItem.selectedItem.getSlot() == Slot.Armor)
		{
			if (equippedItems.equippedChest.getValue() != 0)
				oldEquippedItem = equippedItems.equippedChest;
			equippedItems.equippedChest = selectedItem.selectedItem;
			OnEquipmentChange?.Invoke();
		}

		if (selectedItem.selectedItem.getSlot() == Slot.FootWear)
		{
			if (equippedItems.equippedBoots.getValue() != 0)
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
		playerStats.Strength = equippedItems.getTotalStrength() + 2;
		playerStats.Toughness = equippedItems.getTotalToughness() + 2;
		playerStats.Dexterity = equippedItems.getTotalDexterity() + 2;
		playerStats.Agility = equippedItems.getTotalAgility() + 2;
		ClearSelection?.Invoke();
	}

	private void Unequip()
	{
		if (selectedItem.selectedItem.getSlot() == Slot.MainHand)
		{
			inventory.Add(equippedItems.equippedMainHand);
			selectedItem = null;
			equippedItems.equippedMainHand = new Item();
			OnEquipmentChange?.Invoke();
		}

		else if (selectedItem.selectedItem.getSlot() == Slot.OffHand)
		{
			inventory.Add(equippedItems.equippedOffHand);
			selectedItem = null;
			equippedItems.equippedOffHand = new Item();
			OnEquipmentChange?.Invoke();
		}

		else if (selectedItem.selectedItem.getSlot() == Slot.HeadWear)
		{
			inventory.Add(equippedItems.equippedHead);
			selectedItem = null;
			equippedItems.equippedHead = new Item();
			OnEquipmentChange?.Invoke();
		}

		else if (selectedItem.selectedItem.getSlot() == Slot.Armor)
		{
			inventory.Add(equippedItems.equippedChest);
			selectedItem = null;
			equippedItems.equippedChest = new Item();
			OnEquipmentChange?.Invoke();
		}

		else if (selectedItem.selectedItem.getSlot() == Slot.FootWear)
		{
			inventory.Add(equippedItems.equippedBoots);
			selectedItem = null;
			equippedItems.equippedBoots = new Item();
			OnEquipmentChange?.Invoke();
		}
		OnInventoryChange?.Invoke(inventory);
		playerStats.Strength = equippedItems.getTotalStrength() + 2;
		playerStats.Toughness = equippedItems.getTotalToughness() + 2;
		playerStats.Dexterity = equippedItems.getTotalDexterity() + 2;
		playerStats.Agility = equippedItems.getTotalAgility() + 2;
		ClearSelection?.Invoke();
	}

	public void Remove()
	{
		inventory.Remove(this.selectedItem.selectedItem);
		gold = gold + this.selectedItem.selectedItem.getValue();
		OnInventoryChange?.Invoke(inventory);
	}

	public void LevelUpShop()
	{
		shopLevel++;
		ShopLevelUp?.Invoke();
	}

}
