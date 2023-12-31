using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ItemThings
{
public enum Rarity{
    Common=1,
    Uncommon=3,
    Rare=5,
    Gnepic=10,
    Legendary=20 
}

public enum ItemType
{
    Sword,
    HeadWear,
    Armor,
    FootWear,
    Shield
}
public enum Slot
{
    MainHand,
    OffHand,
    HeadWear,
    Armor,
    FootWear
}
[Serializable]
public class Item
{
    public static int id =0;
    public int specificId;
    public Sprite icon;
    public int Strength = 0;
    public int Toughness = 0;
    public int Dexterity = 0;
    public int Agility = 0;
    public Rarity Rarity;
    public ItemType Type;
    public string Name;
    public int Value = 0;
    public Slot slot;


    public Item(){}

    public Item(int str,int tou, int dex, int agi, Rarity rarity, ItemType type, string name, int Value)
    {
        this.specificId = id;
        id++;
        this.Strength = str;
        this.Toughness = tou;
        this.Dexterity = dex;
        this.Agility = agi;
        this.Rarity = rarity;
        this.Type =type;
        this.Name = name;
        this.icon = Resources.Load<Sprite>("Items_Inventory/Sprites/"+ rarity + "/" + type);
        this.Value = Value;

        if (this.getType()==ItemType.Sword)
        {
            this.slot = Slot.MainHand;
        }
        else if (this.getType()==ItemType.Armor)
        {
            this.slot = Slot.Armor;
        }
        else if (this.getType()==ItemType.Shield)
        {
            this.slot = Slot.OffHand;
        }
        else if (this.getType()==ItemType.HeadWear)
        {
            this.slot = Slot.HeadWear;
        }
        else
        {
            this.slot = Slot.FootWear;
        }
        
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
    public Slot getSlot(){
        return this.slot;
    }
    public int getValue(){
        return this.Value;
    }
    public int getShopValue(){
        return this.Value*4;
    }
}
}