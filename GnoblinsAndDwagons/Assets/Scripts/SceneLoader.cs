using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour, Interactable
{
    [SerializeField] GameStateMemory gameStateMemory;
    [SerializeField] CombatStats playerStats;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] public string sceneToLoad;

    public void Interact()
    {
        gameStateMemory.inShop = false;
        gameStateMemory.inDungeon = false;
        gameStateMemory.leaveDungeon = false;
        gameStateMemory.leaveShop = false;
        gameStateMemory.leaveCombat = false;
        gameStateMemory.inCombat = false;
        playerStats.Strength = 2;
        playerStats.Toughness = 2;
        playerStats.Dexterity = 2;
        playerStats.Agility = 2;
        playerInventory.gold = 200;
        playerInventory.shopLevel = 0;
        SceneManager.LoadScene(sceneToLoad);
    }
}
