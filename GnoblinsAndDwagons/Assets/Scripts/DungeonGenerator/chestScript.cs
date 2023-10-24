using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;

public class chestScript : MonoBehaviour, Interactable
{
    ItemGenerator generator;
    [SerializeField]
    private int commonChance, uncommonChance, rareChance, gnepicChance, legendaryChance;

    public Item storedItem;

    [SerializeField]
    public Dialog itemDialog;
    [SerializeField]
    public Dialog emptyDialog;

    private bool isEmpty;

    [SerializeField]
    PlayerInventory playerInventory;


    public void Interact()
    {
        if (!isEmpty)
        {             
            playerInventory.inventory.Add(storedItem);
            isEmpty = true;
            DialogManager.instance.showDialog(itemDialog);
        } else 
        {
           DialogManager.instance.showDialog(emptyDialog); 
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        storedItem = generator.generateRandomDungeonItem(commonChance, uncommonChance, rareChance, gnepicChance, legendaryChance);
        //itemDialog.lines.Add("You found a " + storedItem.getName());
        //emptyDialog.lines.Add("It is empty");
        //emptyDialog.lines.Add("You already looted it");
        isEmpty = false;
    }



}
