using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Persistance/CombatStats")]
public class CombatStats : ScriptableObject
{
    public int Agility = 0;
    public int Strength = 0;
    public int Toughness = 0;
    public int Dexterity = 0;



    public CombatStats(int Agility, int Strength, int Dexterity, int Toughness)
    {
        this.Agility = Agility;
        this.Strength = Strength;
        this.Dexterity = Dexterity;
        this.Toughness = Toughness;
    }
    

}
