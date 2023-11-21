using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;

public class chestScript : MonoBehaviour, Interactable
{
    ItemGenerator generator;

    [SerializeField]
    GameStateMemory gameStateMemory;

    [SerializeField]
    private int commonChance, uncommonChance, rareChance;

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
            Debug.Log(playerInventory.inventory);
            Debug.Log(storedItem);   
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
        int dungeonLevel = gameStateMemory.dungeonLevel;
        int dungeonMax = gameStateMemory.totalLevels;
        generator = new ItemGenerator();
        storedItem = generator.generateRandomDungeonItem(commonChance + (dungeonMax-dungeonLevel)*2 , uncommonChance - (dungeonMax - dungeonLevel), rareChance - (dungeonMax - dungeonLevel), 0, 0);
        isEmpty = false;
        //itemDialog.lines.Add("You found a " + storedItem.getName());
        //emptyDialog.lines.Add("It is empty");
        //emptyDialog.lines.Add("You already looted it");
    }



}
