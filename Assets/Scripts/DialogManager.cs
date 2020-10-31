using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject textBox;
    public Text text;
    public string[] dialogs = new string[1];
    public string condition;
    public int secondDialog;
    public float writeWaitTime;

    private int maxDialog;
    private char[] characters = new char[1];
    private int dialogIndex = 0;
    private int charIndex = 0;
    private bool inDialog = false;
    private bool writing = false;

    private float timer;
    private float cooldown;
    private bool secondDialogueActive;

    public void StartDialog()
    {
        if (!inDialog && cooldown <= Time.time) {
            if (secondDialog == 0) secondDialog = dialogs.Length;

            if (condition != "" && ConditonMet()) {
                dialogIndex = secondDialog;
                maxDialog = dialogs.Length;
                secondDialogueActive = true;
                LevelManager.levelManager.ProgressGame();
            } else if (secondDialogueActive) {
                dialogIndex = secondDialog;
                maxDialog = dialogs.Length;
            } else {
                dialogIndex = 0;
                maxDialog = secondDialog;
                secondDialogueActive = false;
            }
            textBox.SetActive(true);
            text.text = "";
            inDialog = true;
            writing = true;
            charIndex = 0;
            characters = dialogs[dialogIndex].ToCharArray();
            PlayerMovement.canMove = false;
        }
    }

    void Update() {
        if (writing && timer <= Time.time) {
            text.text += characters[charIndex];
            timer += writeWaitTime;
            charIndex++;
            if (charIndex >= characters.Length) {
                writing = false;
                dialogIndex++;
            }
        }

        if (Input.GetMouseButtonDown(0) && !writing && inDialog) {
            if (dialogIndex >= maxDialog) {
                textBox.SetActive(false);
                inDialog = false;
                cooldown = Time.time + 0.1f;
                Debug.Log("End Dialogue");
                PlayerMovement.canMove = true;
            } else {
                writing = true;
                charIndex = 0;
                text.text = "";
                characters = dialogs[dialogIndex].ToCharArray();
            }
        }
    }

    bool ConditonMet() {
        for (int i = 0; i < PlayerMovement.inventory.Count; i++) {
            if (PlayerMovement.inventory[i] == condition) {
                PlayerMovement.inventory.RemoveAt(i);
                return true;
            }
        }
        return false;
    }


}
