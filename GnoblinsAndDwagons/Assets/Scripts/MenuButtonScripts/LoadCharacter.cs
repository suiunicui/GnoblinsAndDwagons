using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadCharacter : MonoBehaviour
{
    [SerializeField] CombatStats playerStats;
    [SerializeField] GameStateMemory gameStateMemory;
    [SerializeField] PlayerInventory playerInventory;
    DataPersistenceManager dataPersistenceManager;
    public Button button;

    public void OnClick()
    {

        this.dataPersistenceManager = new DataPersistenceManager(gameStateMemory.playerAvatar.species);
        this.dataPersistenceManager.gameState = gameStateMemory;
        this.dataPersistenceManager.playerInventory = playerInventory;
        if (this.dataPersistenceManager.LoadGame())
        {
            gameStateMemory.clearGameState();
            gameStateMemory.load = true;
            playerStats.Strength = (int)((playerInventory.equippedItems.getTotalStrength() + 2) * gameStateMemory.playerAvatar.strengthModifier);
            playerStats.Toughness = (int)((playerInventory.equippedItems.getTotalToughness() + 2) * gameStateMemory.playerAvatar.toughnessModifier);
            playerStats.Dexterity = (int)((playerInventory.equippedItems.getTotalDexterity() + 2) * gameStateMemory.playerAvatar.dexterityModifier);
            playerStats.Agility = (int)((playerInventory.equippedItems.getTotalAgility() + 2) * gameStateMemory.playerAvatar.agilityModifier);
            SceneManager.LoadScene("Camp");
        }
        else
        {
            gameStateMemory.inShop = false;
            gameStateMemory.inDungeon = false;
            gameStateMemory.leaveDungeon = false;
            gameStateMemory.leaveShop = false;
            gameStateMemory.leaveCombat = false;
            gameStateMemory.inCombat = false;
            gameStateMemory.load = false;
            playerStats.Strength = 2;
            playerStats.Toughness = 2;
            playerStats.Dexterity = 2;
            playerStats.Agility = 2;
            playerInventory.gold = 200;
            playerInventory.shopLevel = 0;
            SceneManager.LoadScene("Cutscenes");
        }
    }

    void Start()
    {
        button.image.CrossFadeAlpha(0.2f, 0.0f, false);
        button.enabled = false;
    }

    void OnEnable()
    {
        HeroSelection.OnHeroSelectionClicked += enableButton;
    }

    void OnDisable()
    {
        HeroSelection.OnHeroSelectionClicked -= enableButton;
    }

    private void enableButton(string dummy)
    {
        button.image.CrossFadeAlpha(1, 0.0f, false);
        button.enabled = true;
    }

}
