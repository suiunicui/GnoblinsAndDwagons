using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ItemThings;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image border;
    public void ClearSlot()
    {
        icon.enabled = false;
        border.enabled = true;
    }

    public void DrawSlot(Item item)
    {
        if (item==null){
            ClearSlot();
            return;
        }
        icon.enabled = true;
        border.enabled = true;
        icon.sprite = item.icon;
    }
}
