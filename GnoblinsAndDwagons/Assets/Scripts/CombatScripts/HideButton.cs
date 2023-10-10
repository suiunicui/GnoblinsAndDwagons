using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideButton : MonoBehaviour
{
    public Button button;
    
    public void HideButtons()
    {
        button.enabled = false;
        button.image.enabled = false;
    }

}
