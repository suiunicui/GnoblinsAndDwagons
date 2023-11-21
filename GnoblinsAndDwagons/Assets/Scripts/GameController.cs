using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public enum GameState
{
    FREE_ROAM,
    DIALOG,
    BATTLE,
    INVENTORY,
    MENU
}

public class GameController : MonoBehaviour
{
    [SerializeField] public PlayerController playerController;
    [SerializeField] GameStateMemory gameStateMemory;
    [SerializeField] DungeonController dungeonController;
    [SerializeField] public List<GameObject> npcControllers = new List<GameObject>();

    private bool doesBattleSystemExist = false;
    public static GameController instance { get; private set; }

    GameState state;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        if (gameStateMemory.inDungeon && gameStateMemory.leaveCombat == false)
        {
            gameStateMemory.DungeonStartPos = new Vector3Int(-11, 0, 0);
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
            }
            else if (state == GameState.DIALOG)
            {
                state = GameState.FREE_ROAM;
            }
        };
        returnButton.instance.leaveEscapeMenu += () =>
        {
            if(state == GameState.MENU)
            {
                state= GameState.FREE_ROAM;
                SceneManager.UnloadSceneAsync("EscapeMenu");
            }
        };

    }

    private void Update()
    {

        if (BattleSystem.instance != null)
        {
            if (doesBattleSystemExist == false)
            {
                BattleSystem.instance.StartCombat += () =>
                {
                    state = GameState.BATTLE;
                };
                doesBattleSystemExist = true;
            }
        }


        if (state == GameState.FREE_ROAM)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                state = GameState.MENU;
                SceneManager.LoadScene("EscapeMenu", LoadSceneMode.Additive);
            }
            playerController.HandleUpdate();
            foreach (var controller in npcControllers)
            {
                if (controller != null)
                {
                    controller.GetComponent<updatable>()?.HandleUpdate();
                }
            }
        }
        else if (state == GameState.DIALOG)
        {
            DialogManager.instance.HandleUpdate();
        }
        else if (state == GameState.BATTLE || state == GameState.INVENTORY || state == GameState.MENU)
        {
        }
    }

    public IEnumerator StartInventorySubRoutine()
    {

        InventoryManagerInventory.instance.inInventory += () =>
        {
            state = GameState.INVENTORY;
        };
        InventoryManagerInventory.instance.leaveInventory += () =>
        {
            SceneManager.UnloadSceneAsync("Inventory");
            state = GameState.FREE_ROAM;
        };

        yield return null;
    }

}
