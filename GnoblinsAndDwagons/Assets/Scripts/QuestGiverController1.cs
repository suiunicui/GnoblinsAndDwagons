using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestGiverController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] GameStateMemory gameStateMemory;
    public void Interact()
    {
        gameStateMemory.inDungeon = true;
        gameStateMemory.inShop = false;
        gameStateMemory.leaveDungeon = false;
        gameStateMemory.leaveShop= false;
        gameStateMemory.inCombat = false;
        gameStateMemory.leaveCombat = false;
        gameStateMemory.dungeonLevel = 1;
        DialogManager.instance.showDialog(dialog, true, "RandomDungeon");
    }
}
