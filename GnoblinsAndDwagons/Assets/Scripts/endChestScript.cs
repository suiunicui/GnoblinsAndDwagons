using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endChestScript : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] GameStateMemory gameStateMemory;
    public void Interact()
    {
        gameStateMemory.inDungeon = false;
        gameStateMemory.inShop = false;
        gameStateMemory.leaveDungeon = false;
        gameStateMemory.leaveShop= false;
        gameStateMemory.inCombat = false;
        gameStateMemory.leaveCombat = false;
        gameStateMemory.dungeonLevel = 0;
        DialogManager.instance.showDialog(dialog, true, "EndScene");
    }
}
