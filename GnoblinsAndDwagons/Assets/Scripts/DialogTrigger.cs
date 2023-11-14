using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class dialogTrigger : MonoBehaviour, updatable
{
    [SerializeField] private int triggerDist = 1;

    public GameObject player;

    [SerializeField]
    public Dialog dialog;

    public void HandleUpdate()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < triggerDist)
        {
            Debug.Log("trigger");
            trigger();
        }

    }

    private void trigger()
    {
        DialogManager.instance.showDialog(dialog, false, "", this.gameObject);
    }
}
