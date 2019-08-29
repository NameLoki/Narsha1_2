using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStart : MonoBehaviour {

    private ArdBlue_Unity ardBlue;
    private AudioManager audioManager;
    public AudioClip mainSong;
    private AudioSource source;
    private FadeScreen fadeScreen;
    public SpriteRenderer arrow, text;

    public float min1_on, min1_off, max1_on, max1_off;
    public float min2_on, min2_off, max2_on, max2_off;
    private float Time1_on, Time1_off, Time2_on, Time2_off;
    private bool isOn1, isOn2;
    private bool isStart;

    void Start() {
        ardBlue = GameObject.Find("ArdBlue").GetComponent<ArdBlue_Unity>();
        fadeScreen = GameObject.Find("BlackScreen").GetComponent<FadeScreen>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        source = GetComponent<AudioSource>();
        source.clip = mainSong;

        mainStart();

        min1_on = 0.05f;
        max1_on = 0.2f;
        min1_off = 0.5f;
        max1_off = 4f;

        min2_on = 0.05f;
        max2_on = 0.2f;
        min2_off = 0.5f;
        max2_off = 5f;


        isOn1 = true;
        isOn2 = true;
        isStart = false;
        StartCoroutine(arrowColor());
        StartCoroutine(textColor());
    }



	
	void Update () {
        if ((Input.GetKeyDown(KeyCode.W) || ardBlue.checkStatus() == 1) && !isStart)
            mainEnd();


    }

    IEnumerator arrowColor() {
        while (true) {
            Time1_on = Random.Range(min1_on, max1_on);           // arrow Turn on Time
            Time1_off = Random.Range(min1_off, max1_off);        // arrow Turn off Time

            if (isOn1){
                arrow.color = new Color(1f, 1f, 1f, 1f);
                yield return new WaitForSeconds(Time1_off);
                arrow.color = new Color(0.2f, 0.2f, 0.2f, 1f);
                isOn1 = false;
            } else {
                yield return new WaitForSeconds(Time1_on);

                arrow.color = new Color(1f, 1f, 1f, 1f);
                isOn1 = true;
            }
        }
    }

    IEnumerator textColor() {
        while (true) {
            Time2_on = Random.Range(min2_on, max2_on);           // arrow Turn on Time
            Time2_off = Random.Range(min2_off, max2_off);        // arrow Turn off Time

            if (isOn2){
                text.color = new Color(1f, 1f, 1f, 1f);
                yield return new WaitForSeconds(Time2_off);
                text.color = new Color(0.2f, 0.2f, 0.2f, 1f);
                isOn2 = false;
            } else {
                yield return new WaitForSeconds(Time2_on);

                text.color = new Color(1f, 1f, 1f, 1f);
                isOn2 = true;
            }
        }
    }

    public void mainStart() {
        source.Play();

    }

    public void mainEnd() {
        isStart = true;
        source.Stop();
        audioManager.Play(6);
        fadeScreen.fadeOut();

    }
}