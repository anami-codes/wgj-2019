using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressToGiveUp : MonoBehaviour {
    public GameObject anim;
    public Animator fade;

    private bool active;
    private float timer;

    public void GiveUp(string sceneNAme) {
        anim.GetComponent<Animator>().enabled = true;
        fade.SetBool("Fade", true);

        timer = Time.time + 5f;
        active = true;
    }

    private void Update() {
        if (timer <= Time.time && active) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
