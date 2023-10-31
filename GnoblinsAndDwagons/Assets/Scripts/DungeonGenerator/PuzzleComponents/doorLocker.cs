using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorLocker : MonoBehaviour, Interactable
{
    public string doorTag;
    public string unlockTag;

    [SerializeField]
    public Dialog dialog;
    public void Interact()
    {
        DialogManager.instance.showDialog(dialog);
        GameObject[] doors = GameObject.FindGameObjectsWithTag(doorTag);
        foreach (GameObject door in doors)
        {
            door.GetComponent<BoxCollider2D>().enabled = true;
            door.GetComponent<Renderer>().enabled = true;
        }
        GameObject[] unlockers = GameObject.FindGameObjectsWithTag(unlockTag);
        foreach(GameObject unlocker in unlockers)
        {
            unlocker.GetComponent<doorUnlocker>().isRoomLocked = true;
        }
    }
}
