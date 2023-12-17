using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class bossController : MonoBehaviour, updatable
{
    [SerializeField] private int combatDist = 1;

    public GameObject player;

    [SerializeField]
    public string monsterName;
    [SerializeField]
    public string monsterType;

    [SerializeField]
    public CombatStats enemyStats;

    [SerializeField]
    public int Agility;

    [SerializeField]
    public int Strength;

    [SerializeField]
    public int Toughness;

    [SerializeField]
    public int Dexterity;

    [SerializeField]
    GameStateMemory gameStateMemory;

    [SerializeField]
    public Dialog dialog;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < combatDist)
        {
            triggerCombat();
        }

    }

    private void triggerCombat()
    {
        enemyStats.unitName = monsterName;
        enemyStats.type = monsterType;
        enemyStats.Agility = Agility;
        enemyStats.Strength = Strength;
        enemyStats.Toughness = Toughness;
        enemyStats.Dexterity = Dexterity;

        gameStateMemory.inDungeon = false;
        gameStateMemory.inCombat = true;
        gameStateMemory.leaveCombat = false;
        DialogManager.instance.showDialog(dialog, true, "combat", this.gameObject);


    }
}
