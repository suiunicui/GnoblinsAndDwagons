using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyStats : MonoBehaviour
{
    public Enemies enemies = new Enemies();
    public Text Name;
    public Text HP;
    
 
    void Start()
    {
        Name.text = enemies.enemies[0].unitName;
        HP.text = enemies.enemies[0].currentHP.ToString();

    }
}