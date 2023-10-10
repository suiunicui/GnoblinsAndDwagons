using UnityEngine;

[CreateAssetMenu(fileName = "GameStateMemory", menuName = "Persistance/GameState") ]
public class GameStateMemory : ScriptableObject
{
    public bool inDungeon = false;
    public bool inShop = false;
    public bool leaveShop = false;
    public bool leaveDungeon = false;

    public int dungeonLevel = 0;

    public Vector3Int DungeonStartPos = new Vector3Int(0,0,0);
}
