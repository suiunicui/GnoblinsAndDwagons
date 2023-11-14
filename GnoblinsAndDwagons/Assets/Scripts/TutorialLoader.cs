using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLoader : MonoBehaviour, Interactable
{
    [SerializeField] GameStateMemory gameStateMemory;
    [SerializeField] CombatStats playerStats;
    [SerializeField] PlayerInventory playerInventory;

    public void Interact()
    {
        gameStateMemory.clearGameState();
        gameStateMemory.inTutorial = true;
        playerStats.Strength = (int)(2 * gameStateMemory.playerAvatar.strengthModifier);
        playerStats.Toughness = (int)(2 * gameStateMemory.playerAvatar.toughnessModifier);
        playerStats.Dexterity = (int)(2 *  gameStateMemory.playerAvatar.dexterityModifier);
        playerStats.Agility = (int)(2 * gameStateMemory.playerAvatar.agilityModifier);
        playerInventory.gold = 200;
        playerInventory.shopLevel = 0;
        playerInventory.inventory = new List<ItemThings.Item>(35);
        SceneManager.LoadScene("Tutorial");
    }
}
