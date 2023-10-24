using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    FREE_ROAM,
    DIALOG,
    BATTLE
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
    }
    
}
