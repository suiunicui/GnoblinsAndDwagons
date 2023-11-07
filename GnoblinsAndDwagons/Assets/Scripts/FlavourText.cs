using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FlavourText : MonoBehaviour
{

    public Text text;
    void Start()
    {
        text.text = "Click on a character to learn more about them!";
    }


    private void OnEnable()
    {
        HeroSelection.OnHeroSelectionClicked += ChangeText;
    }

        private void OnDisable()
    {
        HeroSelection.OnHeroSelectionClicked -= ChangeText;
    }

    private void ChangeText(string flavourText)
    {
        text.text= flavourText;
    }
}
