using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultText : MonoBehaviour {


    public Text MusicNameT;
    public Text DifT;
    public Text ComboT;
    public Text PerfectT;
    public Text GoodT;
    public Text BadT;
    public Text MissT;
    public Text PerT;
    public Text APFCT;
    public SpriteRenderer background;
    public Sprite[] backgroundImages;
    public SpriteRenderer Rank;
    public Sprite[] RankImages;

    // Use this for initialization
    void Start() {

        MusicNameT = GameObject.Find("MusicName").GetComponent<Text>();
        DifT = GameObject.Find("Dif").GetComponent<Text>();
        ComboT = GameObject.Find("Combo").GetComponent<Text>();
        PerfectT = GameObject.Find("Perfect").GetComponent<Text>();
        GoodT = GameObject.Find("Good").GetComponent<Text>();
        BadT = GameObject.Find("Bad").GetComponent<Text>();
        MissT = GameObject.Find("Miss").GetComponent<Text>();
        PerT = GameObject.Find("Per").GetComponent<Text>();
        APFCT = GameObject.Find("APFC").GetComponent<Text>();


        MusicCheck(PlayingManagement.Num);
        
        ModeCheck(PlayingManagement.GameMode);

        ComboT.text = ChaeBoSystem.HighCombo + " / " + ChaeBoSystem.allNote;
        PerfectT.text = ChaeBoSystem.Pf.ToString();
        GoodT.text = ChaeBoSystem.Gd.ToString();
        BadT.text = ChaeBoSystem.Bd.ToString();
        MissT.text = (ChaeBoSystem.allNote - (ChaeBoSystem.Pf + ChaeBoSystem.Gd + ChaeBoSystem.Bd)).ToString();

        RankCheck(ChaeBoSystem.Persent);        

        PerT.text = string.Format("{0:0.00}", (float)(Mathf.Round(ChaeBoSystem.Persent * 100) / 100)) + " %";

        ResultCheck();



    }


    public void MusicCheck(int num) {

        if (num == 0) {
            MusicNameT.text = "Forest For Rest";
            background.sprite = backgroundImages[0];

        } else if (num == 1) {
            MusicNameT.text = "HOWUSE";
            background.sprite = backgroundImages[1];

        } else if (num == 2) {
            MusicNameT.text = "Futuring";
            background.sprite = backgroundImages[2];

        } else if (num == 3) {
            MusicNameT.text = "Way Back Home(EDM Remix)";
            background.sprite = backgroundImages[3];

        } else if (num == 4) {
            MusicNameT.text = "Let's Play";
            background.sprite = backgroundImages[4];
            
        } else if (num == 5) {
            MusicNameT.text = "BeLoved Memory";
            background.sprite = backgroundImages[5];

        }
    }

    public void ModeCheck(int num) {

        if (num == 1)
            DifT.text = "Easy";

        else if (num == 2)
            DifT.text = "Hard";

    }

    public void RankCheck(float num) {

        if (ChaeBoSystem.HighCombo == ChaeBoSystem.allNote)  {

            if (ChaeBoSystem.Pf == ChaeBoSystem.allNote)
                Rank.sprite = RankImages[0];   // SS

            else
                Rank.sprite = RankImages[1];   // S+


        }

        else if (num >= 90)
            Rank.sprite = RankImages[2];   // S

        else if (num >= 80)
            Rank.sprite = RankImages[3];   // A


        else if (num >= 70)
            Rank.sprite = RankImages[4];   // B


        else if (num >= 60)
            Rank.sprite = RankImages[5];   // C

        else
            Rank.sprite = RankImages[6];   // D


    }

    public void ResultCheck() {

        if(ChaeBoSystem.Pf == ChaeBoSystem.allNote) {

            APFCT.text = "All Perfect!!";
            APFCT.color = new Color(1f, 1f, 0f, 1f);

        }

        else if (ChaeBoSystem.HighCombo == ChaeBoSystem.allNote) {

            APFCT.text = "All Combo!";
            APFCT.color = new Color(0.5f, 1f, 0.5f, 1f);

        }
    }
}
