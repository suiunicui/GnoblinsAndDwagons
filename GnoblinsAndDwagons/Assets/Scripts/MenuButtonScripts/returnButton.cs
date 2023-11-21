using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnButton : MonoBehaviour
{
    public event Action leaveEscapeMenu;

    public static returnButton instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void OnClick()
    {
        leaveEscapeMenu.Invoke();
    }
}
