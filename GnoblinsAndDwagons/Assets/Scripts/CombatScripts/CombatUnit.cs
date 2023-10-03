using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
	
	public string unitName;
	public int unitLevel;
	public int damage;
	public int maxHP;
	public int currentHP;
	[SerializeField] public Stats stats;

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		return currentHP <= 0;
	}
}
