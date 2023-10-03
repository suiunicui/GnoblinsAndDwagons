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
	public HealthScript healthBar;
	[SerializeField] public EnemyStats stats;
	

	public CombatUnit(string name, int level, int damage, int maxHp, EnemyStats stats)
	{
		unitName = name;
		unitLevel = level;
		this.damage = damage;
		maxHP = maxHp;
		healthBar.SetMaxHealth(maxHP);
		currentHP = maxHP;
		this.stats = stats;
	}

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;
		healthBar.SetHealth(currentHP);

		return currentHP <= 0;
	}
}
