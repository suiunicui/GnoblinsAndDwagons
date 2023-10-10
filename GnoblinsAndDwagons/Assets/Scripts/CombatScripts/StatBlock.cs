using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // This makes the Stats class visible in the Inspector.
public class Stats
{
	[SerializeField] private int strength;
	[SerializeField] private int agility;
	[SerializeField] private int dexterity;
	[SerializeField] private int toughness;

	public Stats(int strength, int agility, int dexterity, int toughness)
	{
		this.strength = strength;
		this.agility = agility;
		this.dexterity = dexterity;
		this.toughness = toughness;
	}

	// Add getters and setters for each field if needed.
	public int Strength { get => strength; set => strength = value; }
	public int Agility { get => agility; set => agility = value; }
	public int Dexterity { get => dexterity; set => dexterity = value; }
	public int Toughness { get => toughness; set => toughness = value; }
}

public class StatBlock 
{
    public int Agility = 0;
    public int Strength = 0;
    public int Toughness = 0;
    public int Dexterity = 0;
}
