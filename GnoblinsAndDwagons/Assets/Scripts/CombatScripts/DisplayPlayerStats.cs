using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerStats : MonoBehaviour
{
    public Text Strength;
    public Text Dexterity;
    public Text Agility;
    public Text Toughness;
    [SerializeField] public CombatStats stats;
 
    void Start()
    {
        Strength.text = "Strength: " + stats.Strength.ToString();
        Dexterity.text = "Dexterity: " + stats.Dexterity.ToString();
        Agility.text = "Agility: " + stats.Agility.ToString();
        Toughness.text = "Toughness: " + stats.Toughness.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Strength.text = "Strength: " + stats.Strength.ToString();
        Dexterity.text = "Dexterity: " + stats.Dexterity.ToString();
        Agility.text = "Agility: " + stats.Agility.ToString();
        Toughness.text = "Toughness: " + stats.Toughness.ToString();
    }
}
