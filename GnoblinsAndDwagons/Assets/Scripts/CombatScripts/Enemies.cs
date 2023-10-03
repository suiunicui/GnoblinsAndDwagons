using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies 
{
    // Start is called before the first frame update
    public List<CombatUnit> enemies = new List<CombatUnit>();

    public Enemies()
    {
        enemies.Add(new CombatUnit("Gnoblin", 1, 10, 10, new EnemyStats(1, 1, 1, 1))); 
    }
   
        
}
