using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateMemory", menuName = "Persistance/GameState") ]
public class GameStateMemory : ScriptableObject
{
    public bool inDungeon = false;
    public bool inShop = false;
    public bool inCombat = false;
    public bool leaveShop = false;
    public bool leaveDungeon = false;
    public bool leaveCombat = false;
    public bool inTutorial = false;
    public bool load = false;

    public int dungeonLevel = 0;
    public int totalLevels = 20;
    public List<PlayerAvatar> selectablePlayerAvatars = new List<PlayerAvatar>()
    {
        new PlayerAvatar(),
        new PlayerAvatar(0.75f,1.5f,0.5f,1.25f,PlayerAvatar.Elf),
        new PlayerAvatar(1.25f,0.75f,1.5f,0.5f,PlayerAvatar.Dwarf),
        new PlayerAvatar(1.5f,0.5f,1.25f,0.75f,PlayerAvatar.Orc)
    };
    public PlayerAvatar playerAvatar = new PlayerAvatar();
    public Vector3Int DungeonStartPos = new Vector3Int(0,0,0);

    public void clearGameState()
    {
        inDungeon = false;
        inShop = false;
        inCombat = false;
        leaveDungeon = false;
        leaveCombat = false;
        leaveShop = false;
        inTutorial = false;
        load = false;
    }
}
