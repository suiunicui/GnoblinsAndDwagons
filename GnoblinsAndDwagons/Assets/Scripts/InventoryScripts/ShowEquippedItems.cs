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

    private void Start(){
        DrawSlots();
    }

    private void DrawSlots()
    {
        if (playerInventory.equippedItems.equippedHead.getValue() != 0)
        {
            Image BorderWithTexture = equippedHead.transform.Find("Border_With_Texture").GetComponent<Image>();
            Image Border = equippedHead.transform.Find("Border").GetComponent<Image>();
            Image Icon = equippedHead.transform.Find("Icon").GetComponent<Image>();
            BorderWithTexture.enabled = false;
            Border.enabled = true;
            Icon.enabled = true;
            Icon.sprite = playerInventory.equippedItems.equippedHead.icon;
        }
        if (playerInventory.equippedItems.equippedChest.getValue() != 0)
        {
            Image BorderWithTexture = equippedChest.transform.Find("Border_With_Texture").GetComponent<Image>();
            Image Border = equippedChest.transform.Find("Border").GetComponent<Image>();
            Image Icon = equippedChest.transform.Find("Icon").GetComponent<Image>();
            BorderWithTexture.enabled = false;
            Border.enabled = true;
            Icon.enabled = true;
            Icon.sprite = playerInventory.equippedItems.equippedChest.icon;
        }
        if (playerInventory.equippedItems.equippedBoots.getValue() != 0)
        {
            Image BorderWithTexture = equippedBoots.transform.Find("Border_With_Texture").GetComponent<Image>();
            Image Border = equippedBoots.transform.Find("Border").GetComponent<Image>();
            Image Icon = equippedBoots.transform.Find("Icon").GetComponent<Image>();
            BorderWithTexture.enabled = false;
            Border.enabled = true;
            Icon.enabled = true;
            Icon.sprite = playerInventory.equippedItems.equippedBoots.icon;
        }
        if (playerInventory.equippedItems.equippedMainHand.getValue() != 0)
        {
            Image BorderWithTexture = equippedMainHand.transform.Find("Border_With_Texture").GetComponent<Image>();
            Image Border = equippedMainHand.transform.Find("Border").GetComponent<Image>();
            Image Icon = equippedMainHand.transform.Find("Icon").GetComponent<Image>();
            BorderWithTexture.enabled = false;
            Border.enabled = true;
            Icon.enabled = true;
            Icon.sprite = playerInventory.equippedItems.equippedMainHand.icon;
        }
        if (playerInventory.equippedItems.equippedOffHand.getValue() != 0)
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
