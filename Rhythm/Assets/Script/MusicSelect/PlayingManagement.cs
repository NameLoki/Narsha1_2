using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class PlayingManagement : MonoBehaviour {
    

    public GameObject[] V;
    public VideoPlayer[] VP;
    public Sprite songname0, songname1, songname2, songname3, songname4, songname5;
    public SpriteRenderer songName;
    public SpriteRenderer easy, hard;
    public Text difficultyText;
    public Text bpmText;



    public float delayTime;
    static public int Num = 0;      // 곡 번호.
    static public int GameMode = 1; // 난이도. 1: easy, 2: hard.
    static public int difficulty;
    static public int bpm;

    private ArdBlue_Unity ardBlue;
    private PlayMusic playMusic;
    private SceneLoader sceneLoader;
    private AudioManager audioManager;
    private FadeScreen fadeScreen;

    private float timer = 0f;
    private float changeTimer = 0f;
    private bool checkStart = false;
    private bool isGameStart = true;

    void Start () {
        VP[0] = V[0].GetComponent<VideoPlayer>();
        VP[1] = V[1].GetComponent<VideoPlayer>();
        VP[2] = V[2].GetComponent<VideoPlayer>();
        VP[3] = V[3].GetComponent<VideoPlayer>();
        VP[4] = V[4].GetComponent<VideoPlayer>();
        VP[5] = V[5].GetComponent<VideoPlayer>();

        ardBlue = GameObject.Find("ArdBlue").GetComponent<ArdBlue_Unity>();
        playMusic = GetComponent<PlayMusic>();
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        fadeScreen = GameObject.Find("BlackScreen").GetComponent<FadeScreen>();
        timer = 0f;
        GameMode = 1;
        ardBlue.status = 0;

        
        change_Video_Name(Num);
        change_bpmText(Num);
        change_difficulty(Num, GameMode);
        fadeScreen.fadeIn();
    }



    void Update () {
        if (timer < 1f && !checkStart) {
            timer += Time.deltaTime;
            return;
        } else {
            checkStart = true;
        }


        changeTimer += Time.deltaTime;

        if (isGameStart) {
            if (Input.GetKeyDown(KeyCode.Space) || ardBlue.status == 2) {
                ardBlue.status = 0;
                if (changeTimer > 0.5f) {
                    audioManager.Play(0);
                    Num += 1;
                    if (Num > 5)
                        Num = 0;            // change song EffSnd
                    change_Video_Name(Num);
                    change_bpmText(Num);
                    change_difficulty(Num, GameMode);
                    changeTimer = 0f;
                }

            } else if (Input.GetKeyDown(KeyCode.LeftShift) || ardBlue.status == 3) {
                ardBlue.status = 0;
                if (changeTimer > 0.3) {
                    if (GameMode == 1){
                        easy.color = new Color(0.3f, 0.3f, 0.3f, 1f);
                        hard.color = new Color(1f, 1f, 1f, 1f);
                        GameMode = 2;
                    } else {
                        easy.color = new Color(1f, 1f, 1f, 1f);
                        hard.color = new Color(0.3f, 0.3f, 0.3f, 1f);
                        GameMode = 1;
                    }
                    audioManager.Play(1);       // change diffcultly Effsnd
                    change_difficulty(Num, GameMode);
                    changeTimer = 0f;
                }

            } else if (Input.GetKeyDown(KeyCode.W) || ardBlue.status == 1) {
                audioManager.Play(2);
                gameStart();    

            }
        }
    }

    void change_Video_Name (int num) {
        switch (num) {

            case 0:
                VP[0].Play();

                songName.sprite = songname0;
                StartCoroutine(DelayStop(VP[5]));
                break;

            case 1:
                VP[1].Play();

                songName.sprite = songname1;
                StartCoroutine(DelayStop(VP[0]));
                break;

            case 2:
                VP[2].Play();

                songName.sprite = songname2;
                StartCoroutine(DelayStop(VP[1]));
                break; 

            case 3:
                VP[3].Play();

                songName.sprite = songname3;
                StartCoroutine(DelayStop(VP[2]));
                break;
            
            case 4:
                VP[4].Play();

                songName.sprite = songname4;
                StartCoroutine(DelayStop(VP[3]));
                break;

            case 5:
                VP[5].Play();

                songName.sprite = songname5;
                StartCoroutine(DelayStop(VP[4]));
                break;

        }
    }

    private IEnumerator DelayStop(VideoPlayer VP) {
        yield return new WaitForSeconds(delayTime);
        VP.Stop();
    }

    void change_bpmText(int num) {
        switch(num) {
            case 0:
                bpmText.text = "BPM : 131";
                break;
            
            case 1:
                bpmText.text = "BPM : 128";
                break;

            case 2:
                bpmText.text = "BPM : 131";
                break;

            case 3:
                bpmText.text = "BPM : 120";
                break;
            
            case 4:
                bpmText.text = "BPM : 179";
                break;

            case 5:
                bpmText.text = "BPM : 120";
                break;
        }
    }

    void change_difficulty(int num, int gameModeDiffcult){      // Num : 0, 1, 2, 3     dif : 1, 2
        int temp = num * 2 + gameModeDiffcult;

        switch(temp) {
            case 1:
                difficultyText.text = "**";          // Forest For Rest
                break;
            
            case 2:
                difficultyText.text = "*******";
                break;

            case 3:
                difficultyText.text = "***";        // HOWUSE
                break;

            case 4:
                difficultyText.text = "********";
                break;

            case 5:
                difficultyText.text = "****";      // Futuring
                break;

            case 6:
                difficultyText.text = "*******";   
                break;

            case 7:
                difficultyText.text = "***";       // Way Back Home
                break;

            case 8:
                difficultyText.text = "*******";
                break;

            case 9:
                difficultyText.text = "*****";      // Let's Play
                break;

            case 10:
                difficultyText.text = "    ?";
                break;

            case 11:
                difficultyText.text = "****";      // BeLoved Memory
                break;

            case 12:
                difficultyText.text = "*******";
                break;
        }
    }

    public void gameStart() {
        isGameStart = false;
        ardBlue.status = 0;
        playMusic.onGame();
        sceneLoader.StartSceneLoader(2);
    }
}