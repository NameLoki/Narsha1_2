using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewManager : MonoBehaviour
{

    // Preview Songs
    public AudioClip[] previews;

    private AudioSource source;

    // Now Playing music number
    int curMusic;

    // Load AudioSource
    void Start() {

        source = GetComponent<AudioSource>();
        curMusic = 0;
        source.clip = previews[0];
        playPreview();

    }

    void Update() {

        if (PlayingManagement.Num != curMusic) {   // If current music has changed

            stopPreview();
            curMusic = PlayingManagement.Num;
            source.clip = previews[curMusic];
            playPreview();

        }

    }

    public void playPreview() {
        source.Play();

    }

    public void stopPreview() {
        source.Stop();

    }

    public void fadePreview() {
        Debug.Log("Fading Preview...");
        source.Stop();

    }

}
