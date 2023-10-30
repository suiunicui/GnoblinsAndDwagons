using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using inventoryThings;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public enum GameState
{
    FREE_ROAM,
    DIALOG,
    BATTLE,
    INVENTORY
}

public class GameController : MonoBehaviour
{
    [SerializeField] public PlayerController playerController;
    [SerializeField] GameStateMemory gameStateMemory;
    [SerializeField] DungeonController dungeonController;
    [SerializeField] public List<GameObject> npcControllers = new List<GameObject>();

    GameState state;

    private void Start()
    {
        if (gameStateMemory.inDungeon && gameStateMemory.leaveCombat == false)
        {
            dungeonController.handleStart();
        }
        playerController.handleStart();

        DialogManager.instance.onShowDialog += () =>
        {
            state = GameState.DIALOG;
        };
        DialogManager.instance.onHideDialog += () =>
        {
            if (state == GameState.DIALOG && gameStateMemory.inCombat && !gameStateMemory.leaveCombat)
            {
                state = GameState.BATTLE;
            }else if (state == GameState.DIALOG)
            {
                state = GameState.FREE_ROAM;
            }
        };
        BattleSystem.instance.StartCombat += () =>
        {
            state = GameState.BATTLE;
        };
        inventoryThings.InventoryManager.instance.inInventory += () =>
        {
            state = GameState.INVENTORY;
        };
        inventoryThings.InventoryManager.instance.leaveInventory += () =>
        {
            gameStateMemory.leaveInventory = true;
        };
    }

    private void Update()
    {
        if (state == GameState.FREE_ROAM)
        {
            playerController.HandleUpdate();
            foreach (var controller in npcControllers)
            {
                if(controller != null)
                {
                    controller.GetComponent<updatable>()?.HandleUpdate();
                }
            }
        }else if (state == GameState.DIALOG)
        {
            DialogManager.instance.HandleUpdate();
        }else if (state == GameState.BATTLE)
        {
        }
        else if ( gameStateMemory.leaveInventory == true)
        {
            state = GameState.FREE_ROAM;
            SceneManager.UnloadSceneAsync("Inventory");
            gameStateMemory.leaveInventory = false;
        }
    }
    
}
