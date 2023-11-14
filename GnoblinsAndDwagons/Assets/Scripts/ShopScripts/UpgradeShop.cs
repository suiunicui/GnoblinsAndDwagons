using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using UnityEngine.UI;
using System;


public class UpgradeShop : MonoBehaviour
{

    [SerializeField] public PlayerInventory playerInventory;
    public Button button;
    public Text buttonText;

    private void OnEnable()
    {
        SellItem.OnItemSold += OnChange;
        AddItem.OnItemPurchased += callOnChange;
        InventorySlot.OnInventoryItemClicked += OnChange;
        ShopSlot.OnShopItemClicked += OnChange;
        EquipmentSlot.OnEquipmentSlotClicked += OnChange;
    }

    private void OnDisable()
    {
        SellItem.OnItemSold -= OnChange;
        AddItem.OnItemPurchased -= callOnChange;
        InventorySlot.OnInventoryItemClicked -= OnChange;
        ShopSlot.OnShopItemClicked -= OnChange;
        EquipmentSlot.OnEquipmentSlotClicked -= OnChange;
    }

    public void OnClick()
    {
        playerInventory.gold = playerInventory.gold -(playerInventory.shopLevel * 3000 + 1000);
        playerInventory.LevelUpShop();
        this.OnChange();
    }

    private void callOnChange(Item item)
    {
        this.OnChange();
    }

    public void OnChange()
    {
        if (playerInventory.shopLevel < 3)
        {
            buttonText.text = "Invest in shop (" + (playerInventory.shopLevel * 3000 + 1000) + ")";
            button.image.CrossFadeAlpha(1, 0.0f, false);
            if (playerInventory.gold >= playerInventory.shopLevel * 3000 + 1000)
            {
                button.enabled = true;
            }
            else
            {
                button.enabled = false;
                button.image.CrossFadeAlpha(0.2f, 0.0f, false);
            }

        }
        else
        {
            button.enabled = false;
            buttonText.text = "Shop is maxed out";
            button.image.CrossFadeAlpha(1, 0.0f, false);
        }
    }
}
