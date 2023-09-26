using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;

public class MerchantController : MonoBehaviour, Interactable
{
    ItemGenerator itemGenerator = new ItemGenerator();
    public void Interact()
    {

        List<Item> Items = itemGenerator.generateItems(5);
        for (int i = 0; i < Items.Count;i++)
         Debug.Log($"{Items[i].getName()} \n Strength: {Items[i].getStrength()} Toughness: {Items[i].getToughness()} Dexterity: {Items[i].getDexterity()} Agility: {Items[i].getAgility()}");
    }
}
