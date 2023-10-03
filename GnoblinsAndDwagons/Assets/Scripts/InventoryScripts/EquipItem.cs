using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;
using System;

public class EquipItem : MonoBehaviour
{
    public static event Action OnItemEquipped;
    public void OnClick(){
        OnItemEquipped?.Invoke();
    }
}

