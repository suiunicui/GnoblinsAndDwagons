using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ItemThings
{
    [Serializable]
    public class PlayerEquippedItems
    {
        public Item equippedHead  = new Item();
        public Item equippedChest = new Item();
        public Item equippedBoots = new Item();
        public Item equippedMainHand = new Item();
        public Item equippedOffHand = new Item();

        public PlayerEquippedItems()
        {}

        public int getTotalStrength()
        {
            return equippedHead.getStrength() + equippedChest.getStrength() + equippedBoots.getStrength() + equippedOffHand.getStrength() + equippedMainHand.getStrength();
        }

        public int getTotalToughness()
        {
            return equippedHead.getToughness() + equippedChest.getToughness() + equippedBoots.getToughness() + equippedOffHand.getToughness() + equippedMainHand.getToughness();
        }

        public int getTotalDexterity()
        {
            return equippedHead.getDexterity() + equippedChest.getDexterity() + equippedBoots.getDexterity() + equippedOffHand.getDexterity() + equippedMainHand.getDexterity();
        }

        public int getTotalAgility()
        {
            return equippedHead.getAgility() + equippedChest.getAgility() + equippedBoots.getAgility() + equippedOffHand.getAgility() + equippedMainHand.getAgility();
        }
    }
}
