using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;

public class MerchantController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] GameStateMemory gameStateMemory;
    public void Interact()
    {
        gameStateMemory.inDungeon = false;
        gameStateMemory.inShop = true;
        gameStateMemory.leaveDungeon = false;
        gameStateMemory.leaveShop = false;
        DialogManager.instance.showDialog(dialog, true, "Shop");
    }
}
