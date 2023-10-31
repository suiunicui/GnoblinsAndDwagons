using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using UnityEngine.UI;
using System;



public class EquipItemInventory : MonoBehaviour
{
    [SerializeField] public PlayerInventory playerInventory;
    public Button button;
    public Text buttonText;

    public static event Action OnItemEquipped;
    public static event Action OnItemUnequipped;

    private void OnEnable()
    {
        InventorySlotInventory.OnInventoryItemClicked += OnChange;
        EquipmentSlotInventory.OnEquipmentSlotClicked += OnChange;
    }

    private void OnDisable()
    {
        InventorySlotInventory.OnInventoryItemClicked -= OnChange;
        EquipmentSlotInventory.OnEquipmentSlotClicked -= OnChange;
    }

    public void OnClick()
    {
        if (playerInventory.selectedItem.panel == Panel.Inventory)
            OnItemEquipped?.Invoke();

        else if (playerInventory.selectedItem.panel == Panel.Equipped_Items)
            OnItemUnequipped?.Invoke();
    }

    public void OnChange()
    {
        if (playerInventory.selectedItem != null)
        {
            if (playerInventory.selectedItem.panel == Panel.Inventory)
            {
                buttonText.text = "Equip selected item";
                button.image.CrossFadeAlpha(1, 0.0f, false);
                button.enabled = true;
            }
            else if (playerInventory.selectedItem.panel == Panel.Equipped_Items)
            {
                buttonText.text = "Unequip selected item";
                button.image.CrossFadeAlpha(1, 0.0f, false);
                button.enabled = true;
            }
            else
            {
                buttonText.text = "Equip selected item";
                button.enabled = false;
                button.image.CrossFadeAlpha(0.2f, 0.0f, false);
            }
        }
        else
        {
            buttonText.text = "Equip selected item";
            button.enabled = false;
            button.image.CrossFadeAlpha(0.2f, 0.0f, false);
        }
    }
}


