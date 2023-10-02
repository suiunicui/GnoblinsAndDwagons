using UnityEngine;

public class HeroController : MonoBehaviour
{
    public CombatStats heroStats; 


    private void Start()
    {

        if (heroStats == null)
        {
            Debug.LogError("Hero stats are not assigned to the HeroController.");
            return;
        }


        int agility = heroStats.playerAgility;
        int strength = heroStats.playerStrength;
        int toughness = heroStats.playerToughness;
        int dexterity = heroStats.playerDexterity;


        Debug.Log("Hero Stats: ");
        Debug.Log("Agility: " + agility);
        Debug.Log("Strength: " + strength);
        Debug.Log("Toughness: " + toughness);
        Debug.Log("Dexterity: " + dexterity);
        
    }

}