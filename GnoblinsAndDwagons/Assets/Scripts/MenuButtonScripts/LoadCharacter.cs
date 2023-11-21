using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCharacter : MonoBehaviour
{
    [SerializeField] CombatStats playerStats;
    [SerializeField] GameStateMemory gameStateMemory;
    [SerializeField] PlayerInventory playerInventory;
     DataPersistenceManager dataPersistenceManager;

    public void OnClick()
    {
        this.dataPersistenceManager = new DataPersistenceManager("Human");
        this.dataPersistenceManager.gameState = gameStateMemory;
        this.dataPersistenceManager.playerInventory = playerInventory;
        this.dataPersistenceManager.LoadGame();

        gameStateMemory.clearGameState();
		playerStats.Strength = (int)((playerInventory.equippedItems.getTotalStrength() + 2)*gameStateMemory.playerAvatar.strengthModifier);
		playerStats.Toughness = (int)((playerInventory.equippedItems.getTotalToughness() + 2)*gameStateMemory.playerAvatar.toughnessModifier);
		playerStats.Dexterity = (int)((playerInventory.equippedItems.getTotalDexterity() + 2)*gameStateMemory.playerAvatar.dexterityModifier);
		playerStats.Agility = (int)((playerInventory.equippedItems.getTotalAgility() + 2)*gameStateMemory.playerAvatar.agilityModifier);
        SceneManager.LoadScene("Camp");
    }
}
