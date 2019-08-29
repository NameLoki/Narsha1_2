using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSkipper : MonoBehaviour {

    private SceneLoader sceneLoader;
    private ArdBlue_Unity ardBlue;

    private float ButtonTimer = 0f;
    private bool isSkip = false;

    void Start () {
        ardBlue = GameObject.Find("ArdBlue").GetComponent<ArdBlue_Unity>();
		sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
	}
	
    void Update() {
        if (Input.GetKey(KeyCode.LeftShift))
            ButtonTimer += Time.deltaTime;
        else
            ButtonTimer = 0f;
        
        if (ButtonTimer >= 2f && !isSkip) {
            isSkip = true;
            ToResult();
        }
    }

    // 결과 창으로 로딩
    public void ToResult() {
        Debug.Log("Track Skip");
        sceneLoader.StartSceneLoader(3);

    }

}

