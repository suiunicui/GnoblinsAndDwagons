using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using UnityEngine.UI;
using System;

public class SellItem : MonoBehaviour
{
    [SerializeField] public PlayerInventory playerInventory;
    public Button button;
    public Text buttonText;
    public Text goldText;

    public static event Action OnItemSold;

    private void OnEnable(){
        InventorySlot.OnInventoryItemClicked += OnChange;
        ShopSlot.OnShopItemClicked += OnChange;
        EquipmentSlot.OnEquipmentSlotClicked += OnChange;
    }

    private void OnDisable(){
        InventorySlot.OnInventoryItemClicked -= OnChange;
        ShopSlot.OnShopItemClicked -= OnChange;
        EquipmentSlot.OnEquipmentSlotClicked -= OnChange;
    }

    public void OnClick(){
        if (playerInventory.selectedItem.panel == Panel.Inventory)
        {
            OnItemSold?.Invoke();
            goldText.text = "Gold: " + playerInventory.gold;
        }
    }

    public void OnChange()
    {
        if (playerInventory.selectedItem != null)
        {
            if (playerInventory.selectedItem.panel == Panel.Inventory)
            {
                buttonText.text = "Sell item for " + playerInventory.selectedItem.selectedItem.getValue() + " gold";
                button.image.CrossFadeAlpha(1,0.0f,false);
                button.enabled= true;
            }
            else
            {
                buttonText.text = "Sell selected item";
                button.enabled= false;
                button.image.CrossFadeAlpha(0.2f,0.0f,false);
            }
        }
        else
        {
            buttonText.text = "Sell selected item";
            button.enabled= false;
            button.image.CrossFadeAlpha(0.2f,0.0f,false);
        }
    }
}

