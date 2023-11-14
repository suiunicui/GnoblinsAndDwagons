using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, Interactable
{
    [SerializeField] GameStateMemory gameStateMemory;
    [SerializeField] CombatStats playerStats;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] public string sceneToLoad;
    public void Interact()
    {
        gameStateMemory.clearGameState();
        playerStats.Strength = 2;
        playerStats.Toughness = 2;
        playerStats.Dexterity = 2;
        playerStats.Agility = 2;
        playerInventory.gold = 200;
        playerInventory.shopLevel = 0;
        SceneManager.LoadScene(sceneToLoad);
    }
}
