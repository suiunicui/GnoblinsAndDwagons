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
        AddItem.OnItemPurchased += Remove;
        SellItem.OnItemSold += Add;
    }

    private void Start(){
        int itemsToGenerate = (playerInventory.shopLevel*6) + 12;
        shopList = itemGenerator.generateItems(itemsToGenerate,playerInventory.shopLevel);
        DrawShopList(shopList);
    }

    private void OnDisable(){
        AddItem.OnItemPurchased -= Remove;
        SellItem.OnItemSold -= Add;
    }

    void ResetShopList()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        shopSlots = new List<ShopSlot>(42);
    }

    void DrawShopList(List<Item> inventory)
    {
        ResetShopList();

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
                playerInventory.selectedItem = new SelectedItem(item,Panel.Shop);
                itemFound = true;
            }

        }
        if (!itemFound)
        playerInventory.selectedItem = null;
    }

    private void Remove(Item item)
    {
        foreach (Item listItem in shopList)
        {
            if (listItem.specificId == item.specificId)
            {
                shopList.Remove(listItem);
                DrawShopList(shopList);
                return;
            }

        }
    }
    private void Add()
    {
            if (shopList.Count < 42)
            {
                shopList.Add(playerInventory.selectedItem.selectedItem);
                DrawShopList(shopList);
                playerInventory.selectedItem = null;
                shopSlots[0].DeselectSlot();
            }
    }
}
