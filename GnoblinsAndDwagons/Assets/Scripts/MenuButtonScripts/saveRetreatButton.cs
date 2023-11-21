using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveRetreatButton : MonoBehaviour
{

    [SerializeField]
    GameStateMemory gameStateMemory;

    [SerializeField]
    ExitController retreatButton;


    // Start is called before the first frame update
    void Start()
    {
        if (gameStateMemory.inDungeon)
        {
            this.GetComponentInChildren<Text>().text = "Retreat";
        }
        else
        {
            this.GetComponentInChildren<Text>().text = "Save";
        }
    }

    void OnClick()
    {
        if (gameStateMemory.inDungeon)
        {
            retreatButton.Interact();
        }
        else
        {
            
        }
    }
}
