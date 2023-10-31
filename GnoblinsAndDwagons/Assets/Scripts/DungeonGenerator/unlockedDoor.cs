using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockedDoor : MonoBehaviour, Interactable
{
    [SerializeField]
    Dialog dialog;
    public void Interact()
    {
        DialogManager.instance.showDialog(dialog);
        Destroy(gameObject);
    }


}
