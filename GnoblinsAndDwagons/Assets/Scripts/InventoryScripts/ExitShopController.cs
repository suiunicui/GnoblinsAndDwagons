using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitShopController : MonoBehaviour
{
    [SerializeField] GameStateMemory gameStateMemory;
    public void OnClick()
    {
        gameStateMemory.inShop = false;
        gameStateMemory.leaveShop = true;
        SceneManager.LoadScene("Camp");
    }
}
