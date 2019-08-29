using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound {
	public AudioClip clip;
	private AudioSource source;

	public void SetSource(AudioSource _source) {
		source = _source;
		source.clip = clip;
	}

	public void Play() {
		source.Play();
	}
}


public class AudioManager : MonoBehaviour {
    
	// Single Turn
	static public AudioManager instance;
	
	private void Awake() {	// Single Turn
        if (instance != null)
            Destroy(this.gameObject);
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
	}

	public Sound[] sounds;	// Real sound files

	// Load objects which contain sound info
	void Start() {
		for(int i=0; i<sounds.Length; i++) {
			GameObject soundObject = new GameObject("Sound : " + i.ToString());
			sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
			soundObject.transform.SetParent(this.transform);
		}
	}
	
	void Update() {
	}
	
	// Play SFX with sound number
	public void Play(int num) {
		sounds[num].Play();
	}
	

}