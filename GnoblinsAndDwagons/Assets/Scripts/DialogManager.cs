using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;

    [SerializeField] GameStateMemory gameStateMemory;

    public event Action onShowDialog;
    public event Action onHideDialog;

    public static DialogManager instance { get; private set; }

    private bool sceneChange;
    private string sceneHolder;
    private GameObject destroyCaller;

    private void Awake()
    {
        instance = this;
        dialogBox.SetActive(false);
    }

    int currentLine = 0;
    Dialog dialog;
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            ++currentLine;
            if(currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                onHideDialog?.Invoke();
                Debug.Log(destroyCaller);
                if(destroyCaller != null)
                {
                    Destroy(destroyCaller);
                }
                if(sceneChange)
                {
                    if(gameStateMemory.inCombat || gameStateMemory.inShop)
                    {
                        if (gameStateMemory.leaveCombat) {
                            gameStateMemory.inCombat = false;
                            SceneManager.UnloadSceneAsync("Combat");
                        }
                        else
                        {
                            SceneManager.LoadScene(sceneHolder, LoadSceneMode.Additive);
                        }
                    } 
                    else
                    {
                        SceneManager.LoadScene(sceneHolder);
                    }
                }
                
            }

        }
    }

    public void showDialog(Dialog dialog, bool changeScene = false, string newScene = "", GameObject caller = null)
    {
        sceneChange = changeScene;
        sceneHolder = newScene;
        destroyCaller = caller;

        onShowDialog?.Invoke();
        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public IEnumerator TypeDialog(string lines) 
    {
        dialogText.text = "";
        foreach (var letter in lines)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }
}
