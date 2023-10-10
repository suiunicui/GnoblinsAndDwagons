using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mimicController : MonoBehaviour, Interactable
{
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
    public Dialog dialog;

    public void Interact()
    {
        enemyStats.unitName = "Mimic";
        enemyStats.type = "mimic";
        enemyStats.Agility = Agility;
        enemyStats.Strength = Strength;
        enemyStats.Toughness = Toughness;
        enemyStats.Dexterity = Dexterity;

        DialogManager.instance.showDialog(dialog, true, "combat");

    }
}
