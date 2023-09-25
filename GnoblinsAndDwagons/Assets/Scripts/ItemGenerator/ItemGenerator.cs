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
        Random random = new Random();
        int randomStrength = random.Next(1,10);
        int randomToughness = random.Next(1,10);
        int randomDexterity = random.Next(1,10);
        int randomAgility = random.Next(1,10);
        Rarity randomRarity = (Rarity)random.Next(0,5);
        ItemType randomType = (ItemType)random.Next(0,3);
        string itemName;

        if(randomStrength>randomToughness && randomStrength> randomDexterity && randomStrength >randomAgility)
            itemName = "of the Giant";
        else if (randomToughness>randomDexterity && randomToughness >randomAgility)
            itemName = "of the Survivor";
        else if (randomDexterity >randomAgility)
            itemName = "of the Fleetfooted";
        else
            itemName = "of the Rabbit";

        itemName = $"{randomRarity} {randomType} {itemName}";

        return new Item(
            randomStrength,
            randomToughness,
            randomDexterity,
            randomAgility,
            randomRarity,
            randomType,
            itemName
        );
    
    }
}
}
