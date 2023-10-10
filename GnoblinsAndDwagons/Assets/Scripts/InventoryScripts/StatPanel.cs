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
    public Text Value;
    public void ClearSlot()
    {
        Name.text = "No item selected!";
        Strength.text = "";
        Toughness.text = "";
        Dexterity.text = "";
        Agility.text = "";
        Slot.text = "";
        Value.text = "";
    }

    public void Draw()
    {
        if (playerInventory.selectedItem == null)
        {
            ClearSlot();
            return;
        }
        Name.text = playerInventory.selectedItem.selectedItem.getName();
        Strength.text = "Strength: " + playerInventory.selectedItem.selectedItem.getStrength().ToString();
        Toughness.text = "Toughness: "+ playerInventory.selectedItem.selectedItem.getToughness().ToString();
        Dexterity.text = "Dexterity: "+ playerInventory.selectedItem.selectedItem.getDexterity().ToString();
        Agility.text = "Agility: "+ playerInventory.selectedItem.selectedItem.getAgility().ToString();
        Slot.text = "Slot: "+ playerInventory.selectedItem.selectedItem.getSlot().ToString();
        Value.text = "Value: "+ playerInventory.selectedItem.selectedItem.getValue().ToString();
    }
}
