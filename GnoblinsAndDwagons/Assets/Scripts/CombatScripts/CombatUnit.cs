using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
	public string unitName;
	public int unitLevel;
	public int maxHp;
	public int currentHp;
	[SerializeField] public CombatStats stats;

	public bool TakeDamage(int dmg)
	{
		currentHp -= dmg;

		return currentHp <= 0;
	}
}
