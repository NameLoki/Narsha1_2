using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackSel : MonoBehaviour {
	private ArdBlue_Unity ardBlue;
	private FadeScreen fadeScreen;
	private AudioManager audioManager;

	private bool isStart;

	// Use this for initialization
	void Start () {
		ardBlue = GameObject.Find("ArdBlue").GetComponent<ArdBlue_Unity>();
		fadeScreen = GameObject.Find("BlackScreen").GetComponent<FadeScreen>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

		isStart = false;
	}
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetKeyDown(KeyCode.LeftShift) || ardBlue.checkStatus() == 3) && !isStart) {
			isStart = true;
			audioManager.Play(6);
            fadeScreen.fadeOut();

        }
		
	} 
}
