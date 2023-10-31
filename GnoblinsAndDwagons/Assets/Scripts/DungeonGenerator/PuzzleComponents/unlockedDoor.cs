using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockedDoor : MonoBehaviour, Interactable
{
    [SerializeField]
    Dialog unlockedDialog;
    [SerializeField]
    Dialog lockedDialog;

    private bool isLocked = true;
    public void Interact()
    {
        if (isLocked)
        {
            DialogManager.instance.showDialog(lockedDialog);
        }
        else
        {
            DialogManager.instance.showDialog(unlockedDialog);
            Destroy(gameObject);
        }
    }

    public void Unlock()
    {
        if (isLocked) { isLocked = false; }
    }
}

