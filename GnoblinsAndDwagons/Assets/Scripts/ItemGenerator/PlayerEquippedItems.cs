using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ItemThings
{
    public class PlayerEquippedItems
    {
        public Item equippedHead;
        public Item equippedChest;
        public Item equippedBoots;
        public Item equippedMainHand;
        public Item equippedOffHand;


        public int? getTotalStrength()
        {
            return equippedHead?.getStrength() + equippedChest?.getStrength() + equippedBoots?.getStrength() + equippedOffHand?.getStrength() + equippedMainHand?.getStrength();
        }

        public int? getTotalToughness()
        {
            return equippedHead?.getToughness() + equippedChest?.getToughness() + equippedBoots?.getToughness() + equippedOffHand?.getToughness() + equippedMainHand?.getToughness();
        }

        public int? getTotalDexterity()
        {
            return equippedHead?.getDexterity() + equippedChest?.getDexterity() + equippedBoots?.getDexterity() + equippedOffHand?.getDexterity() + equippedMainHand?.getDexterity();
        }

        public int? getTotalAgility()
        {
            return equippedHead?.getAgility() + equippedChest?.getAgility() + equippedBoots?.getAgility() + equippedOffHand?.getAgility() + equippedMainHand?.getAgility();
        }
    }
}
