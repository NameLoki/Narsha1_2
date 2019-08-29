using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayMusic : MonoBehaviour {
    
	public SongManager song;
	public AudioManager theAudio;
    public PreviewManager preview;

    public string clickSound;	// Click SFX

    void Start() {
        preview = FindObjectOfType<PreviewManager>();
        theAudio = FindObjectOfType<AudioManager>();
        song = FindObjectOfType<SongManager>();

    }

    public void onGame() {
        
        Debug.Log("Start Game");
        preview.fadePreview();

        
        //theAudio.Play(clickSound);

    }

    public void offGame() {

        Debug.Log("Finish Game");
        song.Stop();
        
        SceneManager.LoadScene(3);

    }

}