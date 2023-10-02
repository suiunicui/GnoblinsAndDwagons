using UnityEngine;
using UnityEngine.UI;

public class DisplayHeroStats : MonoBehaviour
{
    public Text statsText;
    public CombatStats heroStats;

    private void Start()
    {
        // Ensure that you've assigned the Text component and heroStats scriptable object in the Unity Inspector.
        if (statsText == null || heroStats == null)
        {
            Debug.LogError("Please assign Text and heroStats in the Inspector.");
            return;
        }

        // Create a string to display the hero's stats.
        string statsString = "Hero Stats:\n";
        statsString += "Agility: " + heroStats.playerAgility + "\n";
        statsString += "Strength: " + heroStats.playerStrength + "\n";
        statsString += "Toughness: " + heroStats.playerToughness + "\n";
        statsString += "Dexterity: " + heroStats.playerDexterity;

        // Update the UI Text with the stats string.
        statsText.text = statsString;
    }
}