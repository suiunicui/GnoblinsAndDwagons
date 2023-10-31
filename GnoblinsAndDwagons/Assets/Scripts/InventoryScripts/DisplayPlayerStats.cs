using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayPlayerStatsInventory : MonoBehaviour
{
    [SerializeField] public PlayerInventory playerInventory;
    public Text Strength;
    public Text Toughness;
    public Text Dexterity;
    public Text Agility;

    private void Start()
    {
        Strength.text = "Strength: " + playerInventory.playerStats.Strength;
        Toughness.text = "Toughness: " + playerInventory.playerStats.Toughness;
        Dexterity.text = "Dexterity: " + playerInventory.playerStats.Dexterity;
        Agility.text = "Agility: " + playerInventory.playerStats.Agility;
    }

    public void Draw()
    {
        Strength.text = "Strength: " + playerInventory.playerStats.Strength;
        Toughness.text = "Toughness: " + playerInventory.playerStats.Toughness;
        Dexterity.text = "Dexterity: " + playerInventory.playerStats.Dexterity;
        Agility.text = "Agility: " + playerInventory.playerStats.Agility;
    }
}

