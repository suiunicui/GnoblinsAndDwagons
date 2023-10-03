using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemThings;

public class StatPanel : MonoBehaviour
{
    [SerializeField] public PlayerInventory playerInventory;
    public Text Name;
    public Text Strength;
    public Text Toughness;
    public Text Dexterity;
    public Text Agility;
    public Text Slot;
    public void ClearSlot()
    {
        Name.text = "No item selected!";
        Strength.text = "";
        Toughness.text = "";
        Dexterity.text = "";
        Agility.text = "";
        Slot.text = "";
    }

    public void DrawSlot()
    {
        if (playerInventory.selectedItem == null)
        {
            ClearSlot();
            return;
        }
        Name.text = playerInventory.selectedItem.getName();
        Strength.text = "Strength: " + playerInventory.selectedItem.getStrength().ToString();
        Toughness.text = "Toughness: "+ playerInventory.selectedItem.getToughness().ToString();
        Dexterity.text = "Dexterity: "+ playerInventory.selectedItem.getDexterity().ToString();
        Agility.text = "Agility: "+ playerInventory.selectedItem.getAgility().ToString();
        Slot.text = "Slot: "+ playerInventory.selectedItem.getSlot().ToString();
    }
}
