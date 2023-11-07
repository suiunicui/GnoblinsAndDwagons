using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAvatar
{
    public static readonly string Human = "Heroes/Human/hero_Human";
    public static readonly string Elf = "Heroes/Elf/hero_Elf";
    public string path;

    public float strengthModifier,  agilityModifier, toughnessModifier, dexterityModifier= 1.00f;

    public PlayerAvatar(string chosenAvatar = "Heroes/Human/hero_Human")
    {
        this.path = chosenAvatar;
    }
}
