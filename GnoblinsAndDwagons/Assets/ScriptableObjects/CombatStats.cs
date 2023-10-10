using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Persistance/CombatStats")]
public class CombatStats : ScriptableObject
{
    public int Agility = 2;
    public int Strength = 2;
    public int Toughness = 2;
    public int Dexterity = 2;



    public CombatStats(int Agility, int Strength, int Dexterity, int Toughness)
    {
        this.Agility = Agility;
        this.Strength = Strength;
        this.Dexterity = Dexterity;
        this.Toughness = Toughness;
    }
    

}
