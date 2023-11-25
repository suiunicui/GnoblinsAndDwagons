using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager
{
    public string fileName;
    public GameStateMemory gameState;
    public PlayerInventory playerInventory;
    private FileDataHandler dataHandler;
    private GameData data;
    
    public DataPersistenceManager(string fileName){
        this.fileName = fileName;
    }

    public void SaveGame()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.data = new GameData
        {
            gold = playerInventory.gold,
            shopLevel = playerInventory.shopLevel,
            equippedItems = playerInventory.equippedItems,
            inventory = playerInventory.inventory,
            player = gameState.playerAvatar
        };
        Debug.Log(Application.persistentDataPath);
        dataHandler.SaveGame(data);
    }

    public bool LoadGame()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        GameData loadedData = dataHandler.Load();
        if (loadedData != null)
        {
            playerInventory.gold = loadedData.gold;
            playerInventory.shopLevel = loadedData.shopLevel;
            playerInventory.equippedItems = loadedData.equippedItems;
            playerInventory.inventory = loadedData.inventory;
            gameState.playerAvatar = loadedData.player;
            return true;
        }

        return false;
    }
}
