using UnityEngine;

[CreateAssetMenu(fileName = "GameStateMemory", menuName = "Persistance/GameState") ]
public class GameStateMemory : ScriptableObject
{
    public bool inDungeon = false;
    public bool inShop = false;
    public bool leaveShop = false;
    public bool leaveDungeon = false;
}
