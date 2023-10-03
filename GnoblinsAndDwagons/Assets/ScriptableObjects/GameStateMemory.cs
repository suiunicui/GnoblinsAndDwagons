using UnityEngine;

[CreateAssetMenu(fileName = "GameStateMemory", menuName = "Persistance/GameState") ]
public class GameStateMemory : ScriptableObject
{
    public bool inDungeon = false;
    public bool leaveDungeon = false;

    //player stats
    public int playerAgility = 0;
    public int playerStrength = 0;
    public int playerToughness = 0;
    public int playerDexterity = 0;
}
