using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

	// Single Turn
	//static public SongManager instance;

	// Songs
	public AudioClip[] clips;

	public AudioSource theAudio;

	// Single Turn
	//private void Awake() {
	//	if (instance != null)
	//		Destroy(this.gameObject);
	//	else {
	//		DontDestroyOnLoad(this.gameObject);
	//		instance = this;
	//	}
	//}

	// Load AudioSource

	void Start () {
        

    }

	// Load Selected Song
	public void SongPlay(int songNum) {	// Get Song Number
        
        Debug.Log(songNum);
        theAudio.clip = clips[songNum];
		Debug.Log("Loaded Song Completely");

		switch(songNum) {
			case 0:
				Invoke("Play", 2.3f);
				break;

			case 1:
				Invoke("Play", 2.3f);
				break;

			case 2:
				Invoke("Play", 2.3f);
				break;

			case 3:
				Invoke("Play", 2.3f);
				break;

			case 4:
				Invoke("Play", 2.3f);
				break;

			case 5:
				Invoke("Play", 2.3f);
				break;
		}
	}

	// Play Song
	public void Play() {
        theAudio.Play();
		Debug.Log("Music Play");
	}

	// Stop Song
	public void Stop() {
        theAudio.Stop();
		Debug.Log("Music Stop");
	}

}	
