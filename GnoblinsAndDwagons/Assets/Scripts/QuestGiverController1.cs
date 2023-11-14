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
        gameStateMemory.clearGameState();
        gameStateMemory.inDungeon = true;
        gameStateMemory.dungeonLevel = 1;
        DialogManager.instance.showDialog(dialog, true, "EnterDungeon");
    }
}
