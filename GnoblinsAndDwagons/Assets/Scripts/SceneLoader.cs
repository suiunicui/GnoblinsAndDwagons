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
        gameStateMemory.clearGameState();
        playerStats.Strength = (int)(2 * gameStateMemory.playerAvatar.strengthModifier);
        playerStats.Toughness = (int)(2 * gameStateMemory.playerAvatar.toughnessModifier);
        playerStats.Dexterity = (int)(2 *  gameStateMemory.playerAvatar.dexterityModifier);
        playerStats.Agility = (int)(2 * gameStateMemory.playerAvatar.agilityModifier);
        playerInventory.gold = 200;
        playerInventory.shopLevel = 0;
        SceneManager.LoadScene(sceneToLoad);
    }
}
