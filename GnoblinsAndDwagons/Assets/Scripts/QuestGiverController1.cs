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
        gameStateMemory.leaveDungeon = false;
        DialogManager.instance.showDialog(dialog, true, "SmallDungeon");
    }
}
