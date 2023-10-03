using UnityEngine;

public class EnemyStats : StatBlock
{

    public EnemyStats(int Agility, int Strength, int Dexterity, int Toughness)
    {
        this.Agility = Agility;
        this.Strength = Strength;
        this.Dexterity = Dexterity;
        this.Toughness = Toughness;
    }

}