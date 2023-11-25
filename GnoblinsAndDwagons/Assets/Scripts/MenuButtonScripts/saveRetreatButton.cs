using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveRetreatButton : MonoBehaviour
{

    [SerializeField]
    GameStateMemory gameStateMemory;

    [SerializeField]
    ExitController retreatButton;

    [SerializeField] 
    PlayerInventory playerInventory;
    private DataPersistenceManager dataPersistenceManager;


    // Start is called before the first frame update
    void Start()
    {
        if (gameStateMemory.inDungeon)
        {
            this.GetComponentInChildren<Text>().text = "Retreat";
        }
        else
        {
            this.GetComponentInChildren<Text>().text = "Save";
        }

        if (gameStateMemory.playerAvatar.path == PlayerAvatar.Human)
        {
            dataPersistenceManager = new DataPersistenceManager("Human");
        }
        else if (gameStateMemory.playerAvatar.path == PlayerAvatar.Elf)
        {
            dataPersistenceManager = new DataPersistenceManager("Elf");
        }
        else if (gameStateMemory.playerAvatar.path == PlayerAvatar.Dwarf)
        {
            dataPersistenceManager = new DataPersistenceManager("Dwarf");
        }
        else if(gameStateMemory.playerAvatar.path == PlayerAvatar.Orc)
        {
            dataPersistenceManager = new DataPersistenceManager("Orc");
        }
        dataPersistenceManager.gameState = this.gameStateMemory;
        dataPersistenceManager.playerInventory = this.playerInventory;
    }

    public void OnClick()
    {
        if (gameStateMemory.inDungeon)
        {
            retreatButton.Interact();
        }
        else
        {
            dataPersistenceManager.SaveGame();
            this.GetComponentInChildren<Text>().text = "Game saved";
        }
    }
}
