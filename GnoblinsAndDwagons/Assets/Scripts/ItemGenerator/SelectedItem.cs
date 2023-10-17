using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ItemThings
{
    public enum Panel{
        Shop,
        Inventory,
        Equipped_Items
    }


    public class SelectedItem
    {
        public Item selectedItem;
        public Panel? panel;

        public SelectedItem(Item item, Panel panel)
        {
            this.selectedItem = item;
            this.panel = panel;
        }
    }
}
