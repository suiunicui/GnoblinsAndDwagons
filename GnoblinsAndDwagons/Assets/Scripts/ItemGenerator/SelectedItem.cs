using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ItemThings
{
    public class SelectedItem
    {
        public Item selectedItem;
        public bool inShop;

        public SelectedItem(Item item, bool inShop)
        {
            this.selectedItem = item;
            this.inShop = inShop;
        }
    }
}
