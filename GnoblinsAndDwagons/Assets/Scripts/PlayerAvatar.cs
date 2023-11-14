using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAvatar
{
    public static string Human = "Heroes/Human/hero_Human";
    public static string Elf = "Heroes/Elf/hero_Elf";
    public string path;

    public float strengthModifier,  agilityModifier, toughnessModifier, dexterityModifier= 1.00f;

    public PlayerAvatar(float strengthModifier=1, float agilityModifier=1, float toughnessModifier=1, float dexterityModifier=1,string chosenAvatar = "Heroes/Human/hero_Human")
    {
        this.path = chosenAvatar;
        this.strengthModifier = strengthModifier;
        this.agilityModifier = agilityModifier;
        this.toughnessModifier = toughnessModifier;
        this.dexterityModifier = dexterityModifier;
    }
}
