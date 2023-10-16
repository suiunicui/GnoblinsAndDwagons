using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatGameController : MonoBehaviour
{
    [SerializeField] GameStateMemory gameStateMemory;

    GameState state;

    private void Start()
    {

        DialogManager.instance.onShowDialog += () =>
        {
            state = GameState.DIALOG;
        };
        DialogManager.instance.onHideDialog += () =>
        {
            if (state == GameState.DIALOG)
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
   
        if (state == GameState.DIALOG)
        {
            DialogManager.instance.HandleUpdate();
        }else if (state == GameState.BATTLE)
        {
          
        }
    }
    
}
