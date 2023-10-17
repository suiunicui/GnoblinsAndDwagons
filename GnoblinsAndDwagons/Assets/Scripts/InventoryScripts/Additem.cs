using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using UnityEngine.UI;
using System;


public class AddItem : MonoBehaviour
{
    public static event HandleItemPurchased OnItemPurchased;
    public delegate void HandleItemPurchased(Item item);

    [SerializeField] public PlayerInventory playerInventory;
    public Button button;
    public Text buttonText;
    public Text goldText;

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
        if (playerInventory.selectedItem.panel == Panel.Shop)
        {
            OnItemPurchased?.Invoke(playerInventory.selectedItem.selectedItem);
            goldText.text= "Gold: " + playerInventory.gold;
        }
    }

    public void OnChange()
    {
        if (playerInventory.selectedItem != null)
        {
            if (playerInventory.selectedItem.panel == Panel.Shop)
            {
                button.image.CrossFadeAlpha(1,0.0f,false);
                if(playerInventory.gold >= playerInventory.selectedItem.selectedItem.getShopValue())
                {
                    button.enabled= true;
                    buttonText.text = "Buy for " + playerInventory.selectedItem.selectedItem.getShopValue() + " gold";
                }
                else
                {
                    buttonText.text = "Not enough gold";
                }

            }
            else
            {
                button.enabled= false;
                buttonText.text = "Buy selected item";
                button.image.CrossFadeAlpha(0.2f,0.0f,false);
            }
        }
        else
        {
                button.enabled= false;
                button.image.CrossFadeAlpha(0.2f,0.0f,false);
        }
    }
}
