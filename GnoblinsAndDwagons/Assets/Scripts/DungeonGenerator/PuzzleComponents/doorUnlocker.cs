using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorUnlocker : MonoBehaviour, Interactable
{
    public string doorTag;
    [SerializeField]
    public Dialog dialog;

    public bool isRoomLocked = false;
    public void Interact()
    {
        if (isRoomLocked)
        {
             GameObject[] doors = GameObject.FindGameObjectsWithTag(doorTag);
            foreach (GameObject door in doors)
            {
                unlockedDoor realDoor = (unlockedDoor)door.GetComponent(typeof(unlockedDoor));
                if (realDoor != null)
                {
                    realDoor.Unlock();
                }
            }
            DialogManager.instance.showDialog(dialog);
        }
    }
}
