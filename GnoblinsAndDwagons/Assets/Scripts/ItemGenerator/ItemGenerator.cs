using System;
using System.Collections.Generic;
namespace ItemThings
{

    public enum HighStrengthItem
    {
        Mighty,
        Powerful,
        Stalwart,
        Giant,
        Vigorous,
        Herculean,
        Lion,
        Stout,
        Formidable,
        Burly
    }

    public enum HighToughnessItem
    {
        Resilient,
        Sturdy,
        Tenacious,
        Durable,
        Robust,
        Indomitable,
        Hardy,
        Enduring,
        Solid,
        Unyielding
    }

    public enum HighDexterityItem
    {
        Gazelle,
        Fleetfooted,
        Dexterous,
        Sly,
        Nimblefingered,
        Slick,
        Deft,
        Skillful,
        Precise,
        Neat
    }

    public enum HighAgilityItem
    {
        Agile,
        Nimble,
        Rabbit,
        Quick,
        Graceful,
        Acrobatic,
        Fleet,
        Supple,
        Lithe,
        Swift
    }


    public class ItemGenerator
    {
        private static Random random = new Random();
        public List<Item> generateItems(int numberOfItems, int shopLevel)
        {
            List<Item> temporaryItemsList = new List<Item>();
            for (int i = 0; i < numberOfItems; i++)
            {
                temporaryItemsList.Add(generateRandomItem(shopLevel));
            }
            return temporaryItemsList;
        }

        private Item generateRandomItem(int shopLevel)
        {
            Random random = new Random();
            Rarity randomRarity;
            if (shopLevel == 0)
            {
                randomRarity = GenerateRandomRarity(60, 30, 10, 0, 0);
            }
            else if (shopLevel == 1)
            {
                randomRarity = GenerateRandomRarity(50, 30, 15, 5, 0);
            }
            else if (shopLevel == 2)
            {
                randomRarity = GenerateRandomRarity(40, 30, 20, 10, 0);
            }
            else
            {
                randomRarity = GenerateRandomRarity(30, 30, 20, 15, 5);
            }
            int randomStrength = random.Next(1 * (int)randomRarity, 10 * (int)randomRarity);
            int randomToughness = random.Next(1 * (int)randomRarity, 10 * (int)randomRarity);
            int randomDexterity = random.Next(1 * (int)randomRarity, 10 * (int)randomRarity);
            int randomAgility = random.Next(1 * (int)randomRarity, 10 * (int)randomRarity);
            ItemType randomType = (ItemType)random.Next(0, 5);
            string itemName;
            int value;



            if (randomStrength > randomToughness && randomStrength > randomDexterity && randomStrength > randomAgility)
                itemName = "of the " + GenerateRandomHighStrengthItem();
            else if (randomToughness > randomDexterity && randomToughness > randomAgility)
                itemName = "of the " + GenerateRandomHighToughnessItem();
            else if (randomDexterity > randomAgility)
                itemName = "of the " + GenerateRandomHighDexterityItem();
            else
                itemName = "of the " + GenerateRandomHighAgilityItem();

            itemName = $"{randomRarity} {randomType} {itemName}";

            value = (randomStrength + randomToughness + randomDexterity + randomAgility) * (int)(randomRarity);

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

        public Item generateRandomDungeonItem(int commonChance, int uncommonChance, int rareChance, int gnepicChance, int legendaryChance)
        {
            Random random = new Random();
            Rarity randomRarity;

            randomRarity = GenerateRandomRarity(commonChance, uncommonChance, rareChance, gnepicChance, legendaryChance);

            int randomStrength = random.Next(1 * (int)randomRarity, 10 * (int)randomRarity);
            int randomToughness = random.Next(1 * (int)randomRarity, 10 * (int)randomRarity);
            int randomDexterity = random.Next(1 * (int)randomRarity, 10 * (int)randomRarity);
            int randomAgility = random.Next(1 * (int)randomRarity, 10 * (int)randomRarity);
            ItemType randomType = (ItemType)random.Next(0, 5);
            string itemName;
            int value;



            if (randomStrength > randomToughness && randomStrength > randomDexterity && randomStrength > randomAgility)
                itemName = "of the " + GenerateRandomHighStrengthItem();
            else if (randomToughness > randomDexterity && randomToughness > randomAgility)
                itemName = "of the " + GenerateRandomHighToughnessItem();
            else if (randomDexterity > randomAgility)
                itemName = "of the " + GenerateRandomHighDexterityItem();
            else
                itemName = "of the " + GenerateRandomHighAgilityItem();

            itemName = $"{randomRarity} {randomType} {itemName}";

            value = (randomStrength + randomToughness + randomDexterity + randomAgility) * (int)randomRarity;

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

        private Rarity GenerateRandomRarity(int commonChance, int uncommonChance, int rareChance, int gnepicChance, int legendaryChance)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100); // Generate a random number between 1 and 100.

            if (randomNumber <= commonChance)
            {
                return Rarity.Common;
            }
            else if (randomNumber <= uncommonChance + commonChance)
            {
                return Rarity.Uncommon;
            }
            else if (randomNumber <= rareChance + uncommonChance + commonChance)
            {
                return Rarity.Rare;
            }
            else if (randomNumber <= gnepicChance + rareChance + uncommonChance + commonChance)
            {
                return Rarity.Gnepic;
            }
            else
            {
                return Rarity.Legendary;
            }
        }

        public HighStrengthItem GenerateRandomHighStrengthItem()
        {
            Array values = Enum.GetValues(typeof(HighStrengthItem));
            HighStrengthItem randomItem = (HighStrengthItem)values.GetValue(random.Next(values.Length));
            return randomItem;
        }

        public HighDexterityItem GenerateRandomHighDexterityItem()
        {
            Array values = Enum.GetValues(typeof(HighDexterityItem));
            HighDexterityItem randomItem = (HighDexterityItem)values.GetValue(random.Next(values.Length));
            return randomItem;
        }

        public HighToughnessItem GenerateRandomHighToughnessItem()
        {
            Array values = Enum.GetValues(typeof(HighToughnessItem));
            HighToughnessItem randomItem = (HighToughnessItem)values.GetValue(random.Next(values.Length));
            return randomItem;
        }

        public HighAgilityItem GenerateRandomHighAgilityItem()
        {
            Array values = Enum.GetValues(typeof(HighAgilityItem));
            HighAgilityItem randomItem = (HighAgilityItem)values.GetValue(random.Next(values.Length));
            return randomItem;
        }
    }
}
