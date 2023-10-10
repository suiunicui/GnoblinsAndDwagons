using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
	
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    [SerializeField] public CombatStats stats;

    public PlayerUnit(string name, int level, int damage, int maxHp, CombatStats stats)
    {
        unitName = name;
        unitLevel = level;
        this.damage = damage;
        maxHP = maxHp;
        currentHP = maxHP;
        this.stats = stats;
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        return currentHP <= 0;
    }
}