using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    [SerializeField] 
    private RoomFirstGenerator roomFirstGenerator;
    [SerializeField]
    private CorridorFirstGenerator corridorFirstGenerator;

    // Start is called before the first frame update
    void Start()
    {
        roomFirstGenerator.generateDungeon();
    }


}
