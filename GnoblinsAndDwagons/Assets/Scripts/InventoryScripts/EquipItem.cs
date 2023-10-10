using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using UnityEngine.UI;
using System;

public class EquipItem : MonoBehaviour
{
    [SerializeField] public PlayerInventory playerInventory;
    public Button button;
    public Text buttonText;

    public static event Action OnItemEquipped;

    private void OnEnable(){
        InventorySlot.OnInventoryItemClicked += OnChange;
        ShopSlot.OnShopItemClicked += OnChange;
    }

    private void OnDisable(){
        InventorySlot.OnInventoryItemClicked -= OnChange;
        ShopSlot.OnShopItemClicked -= OnChange;
    }

    public void OnClick(){
        if (playerInventory.selectedItem.inShop ==false)
            OnItemEquipped?.Invoke();
    }

    public void OnChange()
    {
        if (playerInventory.selectedItem != null)
        {
            if (playerInventory.selectedItem.inShop ==false)
            {
                buttonText.text = "Equip selected item";
                button.enabled= true;
                button.image.enabled = true;
                buttonText.enabled = true;
            }
            else
            {
                button.enabled= false;
                button.image.enabled = false;
                buttonText.enabled = false;
            }
        }
    }
}

