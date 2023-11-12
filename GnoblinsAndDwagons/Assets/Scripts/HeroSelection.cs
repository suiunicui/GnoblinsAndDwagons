using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroSelection : MonoBehaviour, IPointerClickHandler
{
    public string playerAvatarPath;
    public string playerAvatarAnimator;
    public string flavourText;
    public string species;
    public Image playerAvatar,selectionPanel;
    public Text text;
    public static event Action<string> OnHeroSelectionClicked;
    [SerializeField] private GameStateMemory gameState;

    void OnEnable()
    {
        OnHeroSelectionClicked += clearSelection;
    }

    void OnDisable()
    {
        OnHeroSelectionClicked -= clearSelection;
    }
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
        OnHeroSelectionClicked?.Invoke(flavourText);
        selectionPanel.enabled = true;
        gameState.playerAvatar = new PlayerAvatar(playerAvatarAnimator);
    }
    private void OnRightClick() { }

    private void clearSelection(string dummy)
    {
        selectionPanel.enabled = false;
    }

}
