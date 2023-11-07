using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroSelection : MonoBehaviour, IPointerClickHandler
{
    public string playerAvatarPath;
    public string flavourText;
    public string species;
    public Image playerAvatar;
    public Text text;
    public static event Action<string> OnHeroSelectionClicked;

    void Start()
    {
        playerAvatar.sprite = Resources.Load<Sprite>(playerAvatarPath);
        text.text = species;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        Debug.Log("Heelo");
        OnHeroSelectionClicked?.Invoke(flavourText);
    }
    private void OnRightClick() { }


}
