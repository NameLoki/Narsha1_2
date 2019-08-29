using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ChaeBoSystem : MonoBehaviour {

    public LineFade lineFade;
    public ParticleManager particleManager;
    private AudioManager audioManager;
    private SongManager songManager;
    private ArdBlue_Unity ardBlue;
    public SpriteRenderer perfect;
    public SpriteRenderer good;
    public SpriteRenderer bad;
    public SpriteRenderer miss;


    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;


    public GameObject Pan;
    
    private bool isCoroutineRun;

    public List<GameObject> noc = new List<GameObject>();
    public List<int> NoteType = new List<int>();

    public Sprite[] backgroundImages;
    public SpriteRenderer background;

    public float bpm;
    public int beat, step, musicLength;


    public float rest = 0f;
    public int everyStep;


    public float Speed;

    public int listCount;

    private float dis, not_x;
    private float newDis, newNot_k;
    private int k;


    public int input;


    static public float Persent = 0f;
 
    static public int Combo = 0, HighCombo = 0;

    static public int Pf = 0, Gd = 0, Bd = 0, Ms = 0, allNote = 0;

    public int effColor;


    public int msNum; // 곡 번호
    public int mode;  // 곡 난이도. 1: 이지, 2: 하드
    


    void Start() {
        
        Persent = 0f; Combo = 0; HighCombo = 0;
        Pf = 0; Gd = 0; Bd = 0; Ms = 0;
        Speed = 7f;
        lineFade = GameObject.Find("Check").GetComponent<LineFade>();
        ardBlue = GameObject.Find("ArdBlue").GetComponent<ArdBlue_Unity>();
        songManager = GetComponent<SongManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        msNum = PlayingManagement.Num;
        mode = PlayingManagement.GameMode;

        songManager.SongPlay(msNum);

        perfect.color = new Color(1f, 1f, 1f, 0f);
        good.color = new Color(1f, 1f, 1f, 0f);
        bad.color = new Color(1f, 1f, 1f, 0f);
        miss.color = new Color(1f, 1f, 1f, 0f);

        switch (msNum){

            case 0: // Going to School
                if (mode == 1) {
                    GetComponent<Forest_For_Rest_E>().enabled = true;
                    Debug.Log("Easy");
                } else {
                    GetComponent<Forest_For_Rest_H>().enabled = true;
                    Debug.Log("Hard");
                }

                background.sprite = backgroundImages[0];
                musicLength = 108;
                break;

            case 1:
                if (mode == 1) {
                    GetComponent<HOWUSE_E>().enabled = true;
                    Debug.Log("Easy");

                } else {
                    GetComponent<HOWUSE_H>().enabled = true;
                    Debug.Log("Hard");

                }

                background.sprite = backgroundImages[1];
                musicLength = 106;
                break;

            case 2:
                if (mode == 1) {
                    GetComponent<Futuring_E>().enabled = true;
                    Debug.Log("Easy");

                } else {
                    GetComponent<Futuring_H>().enabled = true;
                    Debug.Log("Hard");

                }

                background.sprite = backgroundImages[2];
                musicLength = 146;
                break;

            case 3:
                if (mode == 1) {
                    GetComponent<Way_Back_Home_E>().enabled = true;
                    Debug.Log("Easy");

                } else {
                    GetComponent<Way_Back_Home_H>().enabled = true;
                    Debug.Log("Hard");
                }

                background.sprite = backgroundImages[3];
                musicLength = 149;
                break;

            case 4:
                if (mode == 1) {
                    GetComponent<Lets_Play_E>().enabled = true;
                    Debug.Log("Easy");

                } else {
                    GetComponent<Lets_Play_H>().enabled = true;
                    Debug.Log("Hard");

                }

                background.sprite = backgroundImages[4];
                musicLength = 122;
                break;

            case 5:
                if (mode == 1) {
                    GetComponent<BeLoved_Memory_E>().enabled = true;
                    Debug.Log("Easy");

                } else {
                    GetComponent<BeLoved_Memory_H>().enabled = true;
                    Debug.Log("Hard");

                }

                background.sprite = backgroundImages[5];
                musicLength = 122;
                break;
        }


        StartCoroutine(GoResult(musicLength));

    }


    void Update() {

        input = ardBlue.checkStatus();

        if (allNote != 0)
            Persent = (25*Bd / allNote) + (85*Gd / allNote) + (100f*Pf / allNote);
        else
            Persent = 0f;

        for(int i = 0; i < noc.Count; i++) {

            if (noc[i].transform.position.x >= -10) {

                not_x = noc[0].transform.position.x;
                dis = (Pan.transform.position.x - not_x) * (-1);
                
                for(k = 0; k < noc.Count; k++){
                    
                    newNot_k = noc[k].transform.position.x;
                    newDis = (Pan.transform.position.x - newNot_k) * (-1);

                    if(newDis < -1f){

                        Combo = 0;
                        continue;

                    }
                
                    if (newDis >= -1f && newDis <= 1f) {

                        if (panjungCheck(k)) {

                            ardBlue.status = 0;
                            
                            Destroy(noc[k]);
                            noc.RemoveAt(k);
                            NoteType.RemoveAt(k);

                            addScore(dis);

                        }

                    }

                }


                if(dis < -1f){

                    if(isCoroutineRun) {
                        StopCoroutine(panjungImage(miss));
                        isCoroutineRun = false;
                        StartCoroutine(panjungImage(miss));
                    } else {
                        StartCoroutine(panjungImage(miss));
                    }

                    if (dis < -3f) {
                        
                        Destroy(noc[0]);
                        noc.RemoveAt(0);
                        NoteType.RemoveAt(0);

                    }

                }

                if(noc.Count != 0)
                    noc[i].transform.Translate(Vector2.left * Speed * Time.deltaTime);

            }

        }
        
    }

    IEnumerator panjungImage(SpriteRenderer image) {

        isCoroutineRun = true;
        float alpha = 0f;
        float timer = 0f;

        perfect.color = new Color(1f, 1f, 1f, 0f);
        good.color = new Color(1f, 1f, 1f, 0f);
        bad.color = new Color(1f, 1f, 1f, 0f);
        miss.color = new Color(1f, 1f, 1f, 0f);

        while (timer < 0.3) {
            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime;
            alpha = 1f - Mathf.Clamp01(timer / 0.3f);

            image.color = new Color(1f, 1f, 1f, alpha);
        }
        isCoroutineRun = false;
    }

    public void addScore(float dis) {

        if (dis <= 0.5f && dis >= -0.5f) {

            Pf += 1;
            Combo += 1;
            lineFade.PerfectLine();
            if(isCoroutineRun) {
                StopCoroutine(panjungImage(perfect));
                isCoroutineRun = false;
                StartCoroutine(panjungImage(perfect));
            } else {
                StartCoroutine(panjungImage(perfect));
            }
            effColor = 1;

        }

        else if (dis <= 0.8f && dis >= -0.8f) {

            Gd += 1;
            Combo += 1;
            lineFade.GoodLine();
            if(isCoroutineRun) {
                StopCoroutine(panjungImage(good));               
                isCoroutineRun = false;
                StartCoroutine(panjungImage(good));
            } else {
                StartCoroutine(panjungImage(good));
            }
            effColor = 2;

        }

        else if (dis <= 1f && dis >= -1f) {

            Bd += 1;
            Combo = 0;
            if(isCoroutineRun) {
                StopCoroutine(panjungImage(bad));
                isCoroutineRun = false;
                StartCoroutine(panjungImage(bad));
            } else {
                StartCoroutine(panjungImage(bad));
            }
            effColor = 3;

        }

            colorEff();

        if (Combo > HighCombo)
            HighCombo = Combo;

    }


    public bool panjungCheck(int num) {

        if (input == NoteType[num]) {
            switch(input) {
                case 1:
                    audioManager.Play(3);
                    break;
                case 2:
                    audioManager.Play(4);
                    break;
                case 3:
                    audioManager.Play(5);
                    break;
                default:
                    break;
            }
            return true;
        }

        return false;

    }

    public void colorEff() {
        particleManager.PlayParitcle(input, effColor);
    }




    async public void m(int shape, int beat, int step) {


        allNote += 1;

        everyStep = (beat - 1) * 16 + (step - 1);
        rest = everyStep * 60f / bpm / 4f;

        await Task.Delay(Convert.ToInt32(Math.Round(rest * 1000))).ConfigureAwait(false);
            

        switch (shape) {

            case 1:
                UnityMainThreadDispatcher.Instance().Enqueue(Note_case_1());
                break;

            case 2:
                UnityMainThreadDispatcher.Instance().Enqueue(Note_case_2());
                break;

            case 3:
                UnityMainThreadDispatcher.Instance().Enqueue(Note_case_3());
                break;

        }

    }


    public IEnumerator Note_case_1() {
        try{
            GameObject clonedNote = (GameObject)Instantiate(Note1, Note1.transform.position, Note1.transform.rotation);
            noc.Add(clonedNote);
            NoteType.Add(1);
        } catch (MissingReferenceException){

        }
        yield return null;
    }

    public IEnumerator Note_case_2() {
        try{
            GameObject clonedNote = (GameObject)Instantiate(Note2, Note2.transform.position, Note2.transform.rotation);
            noc.Add(clonedNote);
            NoteType.Add(2);
        } catch (MissingReferenceException) {

        }
        yield return null;

    }

    public IEnumerator Note_case_3() {
        try{
            GameObject clonedNote = (GameObject)Instantiate(Note3, Note3.transform.position, Note3.transform.rotation);
            noc.Add(clonedNote);
            NoteType.Add(3);
        } catch (MissingReferenceException) {

        }
        yield return null;

    }

    public IEnumerator GoResult(int Length) {

        yield return new WaitForSeconds(Length);
        Camera.main.GetComponent<PlayMusic>().offGame();
    }

    private void OnApplicationQuit() {
        Destroy(this);
    }
}