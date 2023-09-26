using UnityEngine;

[CreateAssetMenu(fileName = "GameStateMemory", menuName = "Persistance") ]
public class GameStateMemory : ScriptableObject
{
    public bool inDungeon = false;
    public bool leaveDungeon = false;
}
