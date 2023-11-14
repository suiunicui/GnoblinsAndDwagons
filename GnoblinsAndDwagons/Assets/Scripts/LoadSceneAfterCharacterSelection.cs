using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneAfterCharacterSelection : MonoBehaviour, Interactable
{

    [SerializeField] GameStateMemory gameStateMemory;
    [SerializeField] CombatStats playerStats;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] public string sceneToLoad;
    public Button button;
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
