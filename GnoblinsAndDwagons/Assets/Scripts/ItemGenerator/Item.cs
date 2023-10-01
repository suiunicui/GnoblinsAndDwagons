using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemThings
{
public enum Rarity{
    common,
    uncommon,
    rare,
    gnepic,
    legendary 
}

public enum ItemType
{
    Sword,
    Armor,
    Shield
}
[CreateAssetMenu]
public class Item
{
    public Sprite icon;
    private int Strength;
    private int Toughness;
    private int Dexterity;
    private int Agility;
    private Rarity Rarity;
    private ItemType Type;
    private string Name;


    public Item(int str,int tou, int dex, int agi, Rarity rarity, ItemType type, string name)
    {
        this.Strength = str;
        this.Toughness = tou;
        this.Dexterity = dex;
        this.Agility = agi;
        this.Rarity = rarity;
        this.Type =type;
        this.Name = name;
        this.icon = Resources.Load<Sprite>("Items_Inventory/Sprites/" + type);
    }

    public int getStrength(){
        return this.Strength;
    }
    public int getToughness(){
        return this.Toughness;
    }
    public int getDexterity(){
        return this.Dexterity;
    }
    public int getAgility(){
        return this.Agility;
    }
    public Rarity getRarity(){
        return this.Rarity;
    }
    public ItemType getType(){
        return this.Type;
    }
    public string getName(){
        return this.Name;
    }
}
}