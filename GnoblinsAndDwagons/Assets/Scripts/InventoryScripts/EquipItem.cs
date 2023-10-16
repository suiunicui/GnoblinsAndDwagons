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
                button.image.CrossFadeAlpha(1,0.0f,false);
                button.enabled= true;
            }
            else
            {
                button.enabled= false;
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

