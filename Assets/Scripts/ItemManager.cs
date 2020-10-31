using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public string itemName;

    public bool dialogue;
    public bool anim;
    public bool pickUp;

    public string condition;

    public Animator anim_;
    public DialogManager dialogManager;

    private bool active;

    public bool UseItem (Vector3 pos) {
        if (!active && PlayerMovement.canMove && Vector3.Distance(pos, transform.position) <= 0.75f) {
            if (condition == "" || ConditonMet()) {
                if (dialogue && dialogManager != null) {
                    dialogManager.StartDialog();
                }

                if (anim && anim_ != null) {
                    anim_.enabled = true;
                }

                if (pickUp) {
                    PlayerMovement.inventory.Add(itemName);
                }
                LevelManager.levelManager.ProgressGame();
                active = false;
                gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }

    bool ConditonMet() {
        for (int i = 0; i < PlayerMovement.inventory.Count; i++) {
            if (PlayerMovement.inventory[i] == condition) {
                return true;
            }
        }
        return false;
    }
}
