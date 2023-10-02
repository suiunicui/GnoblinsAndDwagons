using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Persistance")]
public class CombatStats : ScriptableObject
{
    //player stats
    public int playerAgility = 0;
    public int playerStrength = 0;
    public int playerToughness = 0;
    public int playerDexterity = 0;

    //enemy stats
    public int enemyAgility = 0;
    public int enemyStrength = 0;
    public int enemyToughness = 0;
    public int enemyDexterity = 0;
}
