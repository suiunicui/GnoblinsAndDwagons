using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerAvatar
{
    public static string Human = "Heroes/Human/hero_Human";
    public static string Elf = "Heroes/Elf/hero_Elf";
    public static string Dwarf = "Heroes/Dwarf/hero_Dwarf";
    public static string Orc = "Heroes/Orc/hero_Orc";
    public string path;
    public string species;
    public string avatar;

    public float strengthModifier,  agilityModifier, toughnessModifier, dexterityModifier= 1.00f;

    public PlayerAvatar(float strengthModifier=1, float agilityModifier=1, float toughnessModifier=1, float dexterityModifier=1,string chosenAvatar = "Heroes/Human/hero_Human", string CombatAvatar = "Heroes/Human/Combat", string species = "Human")
    {
        this.species = species;
        this.path = chosenAvatar;
        this.avatar = CombatAvatar;
        this.strengthModifier = strengthModifier;
        this.agilityModifier = agilityModifier;
        this.toughnessModifier = toughnessModifier;
        this.dexterityModifier = dexterityModifier;
    }
}
