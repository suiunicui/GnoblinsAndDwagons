using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies 
{
	// Start is called before the first frame update
	public List<CombatUnit> enemies = new();

	public Enemies()
	{
		var enemyObject = new GameObject("Enemy");

		var enemyUnit = enemyObject.AddComponent<CombatUnit>();
	    
		enemyUnit.unitName = "Gnoblin";
		enemyUnit.unitLevel = 1;
		enemyUnit.currentHP = 10;
		enemyUnit.maxHP = 10;
		enemyUnit.stats = new Stats(1, 1, 1, 1);
	    
		enemies.Add(enemyUnit);
	}
   
        
}
