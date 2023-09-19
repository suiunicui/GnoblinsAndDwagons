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
    [SerializeField] PlayerController playerController;

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
    }

    private void Update()
    {
        if (state == GameState.FREE_ROAM)
        {
            playerController.HandleUpdate();
        }else if (state == GameState.DIALOG)
        {
            DialogManager.instance.HandleUpdate();
        }
    }
}
