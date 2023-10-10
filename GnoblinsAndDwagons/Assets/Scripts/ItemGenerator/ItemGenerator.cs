using System;
using System.Collections.Generic;
namespace ItemThings
{
public class ItemGenerator
{
    public List<Item> generateItems(int numberOfItems)
    {
        List<Item> temporaryItemsList = new List<Item>();
        for (int i = 0; i < numberOfItems; i++)
        {
            temporaryItemsList.Add(generateRandomItem());
        }
        return temporaryItemsList;
    }

    private Item generateRandomItem()
        {
        int[] numbers = { 1, 3, 5, 10, 20 };
        Random random = new Random();
        int randomIndex = random.Next(0, numbers.Length);
        Rarity randomRarity =(Rarity) numbers[randomIndex];
        int randomStrength = random.Next(1,10);
        int randomToughness = random.Next(1,10);
        int randomDexterity = random.Next(1,10);
        int randomAgility = random.Next(1,10);
        ItemType randomType = (ItemType)random.Next(0,5);
        string itemName;
        int value;



        if(randomStrength>randomToughness && randomStrength> randomDexterity && randomStrength >randomAgility)
            itemName = "of the giant";
        else if (randomToughness>randomDexterity && randomToughness >randomAgility)
            itemName = "of the survivor";
        else if (randomDexterity >randomAgility)
            itemName = "of the fleetfooted";
        else
            itemName = "of the rabbit";

        itemName = $"{randomRarity} {randomType} {itemName}";

        value = (randomStrength+randomToughness+randomDexterity+randomAgility)*(int)(randomRarity);

        return new Item(
            randomStrength,
            randomToughness,
            randomDexterity,
            randomAgility,
            randomRarity,
            randomType,
            itemName,
            value
        );
    
    }
}
}
