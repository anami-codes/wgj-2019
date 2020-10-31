using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource[] audioClips;
    public float soundBegining;
    public float soundMiddle;
    public float soundEnd;

    private bool begining;
    private bool middle;
    private bool end;

    public void StartBackgroundSound()
    {
        begining = true;
        foreach (AudioSource audio in audioClips) {
            audio.volume = soundBegining;
            audio.Play();
        }
    }

    public void UpdateSound()
    {
        if (begining && !middle) {
            middle = true;
            foreach (AudioSource audio in audioClips) {
                audio.volume = soundMiddle;
            }
        } else if (middle && !end) {
            end = true;
            foreach (AudioSource audio in audioClips) {
                audio.volume = soundEnd;
            }
        }
    }

    public void StopSound() {
        foreach (AudioSource audio in audioClips) {
            audio.Stop();
        }
    }

    public void PlaySFX(int id) {
        sfx[id].Play();
    }
}
