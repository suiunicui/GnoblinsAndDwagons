using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace inventoryThings
{
    public class ExitInventoryController : MonoBehaviour
    {
        [SerializeField] GameStateMemory gameStateMemory;
        public void OnClick()
        {
            gameStateMemory.inShop = false;
            gameStateMemory.inDungeon = false;
            gameStateMemory.leaveDungeon = false;
            gameStateMemory.leaveShop = true;
            SceneManager.LoadScene("Camp");
        }
    }
}
