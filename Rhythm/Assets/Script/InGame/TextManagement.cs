using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManagement : MonoBehaviour {


    public Text CT;
    public Text Per;
    public Text HighCT;


    void Start () {

        CT = GameObject.Find("ComboText").GetComponent<Text>();
        Per = GameObject.Find("Persentage").GetComponent<Text>();
        HighCT = GameObject.Find("HighComboText").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        CT.text = "Combo : " + ChaeBoSystem.Combo;
        HighCT.text = "High Combo  : " + ChaeBoSystem.HighCombo;
        Per.text = string.Format("{0:0.00}", (float)(Mathf.Round(ChaeBoSystem.Persent * 100) / 100)) + "%";

    }

}
