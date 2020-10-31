using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    public DialogManager startingDialog;
    public GameObject giveUp;
    public SoundManager sound;
    public Animator anim;
    public GameObject ending;
    public GameObject player;
    public bool lastOne;

    private int progress = 1;
    private float timer;
    private bool active;

    void Start()
    {
        levelManager = this;
        sound.StartBackgroundSound();
        startingDialog.StartDialog();
        active = false;
    }

    void Update()
    {
        if (progress >= 3 && PlayerMovement.canMove && !active) {
            Debug.Log("Fade");
            sound.StopSound();
            anim.SetBool("Fade", true);
            timer = Time.time + 1f;
            active = true;
        }

        if (progress >= 3 && timer <= Time.time && PlayerMovement.canMove && active) {
            Debug.Log("End Level");
            anim.SetBool("Fade", false);
            player.SetActive(false);
            ending.SetActive(true);
            if (lastOne) ending.GetComponent<Animator>().enabled = true;
            giveUp.SetActive(true);
            progress = 0;
        }
    }

    public void ProgressGame () {
        Debug.Log("Level Progress: " + progress);
        sound.UpdateSound();
        progress++;
    }
}
