using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ItemThings;
using Unity.VisualScripting;

public class GameData
{
    public int gold, shopLevel;
    public PlayerEquippedItems equippedItems;
    public List<ItemThings.Item> inventory;
    public PlayerAvatar player;

}
