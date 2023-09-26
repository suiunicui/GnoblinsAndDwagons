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

    public event Action onShowDialog;
    public event Action onHideDialog;

    public static DialogManager instance { get; private set; }

    private bool sceneChange;
    private string sceneHolder;

    private void Awake()
    {
        instance = this;
        dialogBox.SetActive(false);
    }

    int currentLine = 0;
    Dialog dialog;
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
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
                if(sceneChange)
                {
                    SceneManager.LoadScene(sceneHolder);
                }
            }

        }
    }

    public void showDialog(Dialog dialog, bool changeScene = false, string newScene = "")
    {
        sceneChange = changeScene;
        sceneHolder = newScene;

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
