using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemThings;

public class ShopManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<ShopSlot> shopSlots = new List<ShopSlot>(42);
    private ItemGenerator itemGenerator = new ItemGenerator();
    private List<Item> shopList = new List<Item>();
    [SerializeField] public PlayerInventory playerInventory;

    private void OnEnable(){
        //PlayerInventory.OnInventoryChange += DrawInventory;
    }

    private void Start(){
        shopList = itemGenerator.generateItems(42);
        DrawInventory(shopList);
    }

    private void OnDisable(){
        //PlayerInventory.OnInventoryChange -= DrawInventory;
    }

    void ResetInventory()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        shopSlots = new List<ShopSlot>(42);
    }

    void DrawInventory(List<Item> inventory)
    {
        ResetInventory();

        for (int i = 0; i < shopSlots.Capacity; i++)
        {
            CreateShopSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            shopSlots[i].DrawSlot(inventory[i]);
        }
    }

    void CreateShopSlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        ShopSlot newSlotComponent = newSlot.GetComponent<ShopSlot>();
        newSlotComponent.ClearSlot();

        shopSlots.Add(newSlotComponent);
    }

    public void DeselectAllSlots(int id = -1)
    {
        foreach (ShopSlot slot in shopSlots)
        {
            slot.selectedShader.enabled = false;
            slot.thisItemSelected = false;
        }
        bool itemFound = false;
        foreach (Item item in shopList)
        {
            if (item.specificId == id)
            {
                playerInventory.selectedItem = new SelectedItem(item,true);
                itemFound = true;
            }

        }
        if (!itemFound)
        playerInventory.selectedItem = null;
    }
}
