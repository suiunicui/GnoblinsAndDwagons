using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEquippedItems : MonoBehaviour
{
    public Image equippedHead;
    public Image equippedChest;
    public Image equippedBoots;
    public Image equippedMainHand;
    public Image equippedOffHand;
    [SerializeField] public PlayerInventory playerInventory;

    private void OnEnable(){
        PlayerInventory.OnEquipmentChange += DrawSlots;
    }

    private void OnDisable(){
        PlayerInventory.OnEquipmentChange -= DrawSlots;
    }

    private void DrawSlots()
    {
        if (playerInventory.equippedItems.equippedHead != null)
        {
            Image BorderWithTexture = equippedHead.transform.Find("Border_With_Texture").GetComponent<Image>();
            Image Border = equippedHead.transform.Find("Border").GetComponent<Image>();
            Image Icon = equippedHead.transform.Find("Icon").GetComponent<Image>();
            BorderWithTexture.enabled = false;
            Border.enabled = true;
            Icon.enabled = true;
            Icon.sprite = playerInventory.equippedItems.equippedHead.icon;
        }
        if (playerInventory.equippedItems.equippedChest != null)
        {
            Image BorderWithTexture = equippedChest.transform.Find("Border_With_Texture").GetComponent<Image>();
            Image Border = equippedChest.transform.Find("Border").GetComponent<Image>();
            Image Icon = equippedChest.transform.Find("Icon").GetComponent<Image>();
            BorderWithTexture.enabled = false;
            Border.enabled = true;
            Icon.enabled = true;
            Icon.sprite = playerInventory.equippedItems.equippedChest.icon;
        }
        if (playerInventory.equippedItems.equippedBoots != null)
        {
            Image BorderWithTexture = equippedBoots.transform.Find("Border_With_Texture").GetComponent<Image>();
            Image Border = equippedBoots.transform.Find("Border").GetComponent<Image>();
            Image Icon = equippedBoots.transform.Find("Icon").GetComponent<Image>();
            BorderWithTexture.enabled = false;
            Border.enabled = true;
            Icon.enabled = true;
            Icon.sprite = playerInventory.equippedItems.equippedBoots.icon;
        }
        if (playerInventory.equippedItems.equippedMainHand != null)
        {
            Image BorderWithTexture = equippedMainHand.transform.Find("Border_With_Texture").GetComponent<Image>();
            Image Border = equippedMainHand.transform.Find("Border").GetComponent<Image>();
            Image Icon = equippedMainHand.transform.Find("Icon").GetComponent<Image>();
            BorderWithTexture.enabled = false;
            Border.enabled = true;
            Icon.enabled = true;
            Icon.sprite = playerInventory.equippedItems.equippedMainHand.icon;
        }
        if (playerInventory.equippedItems.equippedOffHand != null)
        {
            Image BorderWithTexture = equippedOffHand.transform.Find("Border_With_Texture").GetComponent<Image>();
            Image Border = equippedOffHand.transform.Find("Border").GetComponent<Image>();
            Image Icon = equippedOffHand.transform.Find("Icon").GetComponent<Image>();
            BorderWithTexture.enabled = false;
            Border.enabled = true;
            Icon.enabled = true;
            Icon.sprite = playerInventory.equippedItems.equippedOffHand.icon;
        }
    }
}
