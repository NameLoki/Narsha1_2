using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	// SingleTon
	private static SceneLoader instance;
	// Loading Scene
	public int nextSceneNum;
	// Fade In
	public static Color fadeColor;
	public SpriteRenderer[] fadeImage;
    private bool FadeOut_isDone = false;

    // the Panel must don't be destroyed
    void Awake() {
		if (instance != null) {
			Destroy(this.gameObject);
			return;
		}
		instance = this;
        DontDestroyOnLoad(this);
    }
	// Fade Sprites alpha value
	void Start () {
        fadeColor = new Color (255f, 255f, 255f, 0f);
		foreach (var fi in fadeImage)
        	fi.color = fadeColor;
	}
	
    public void StartSceneLoader(int _nextSceneNum) {
		nextSceneNum = _nextSceneNum;
        StartCoroutine(FadeOut());
    }
    // To get Start
    void Update () {
        if (FadeOut_isDone) {
            LoadLoadingScene();
			FadeOut_isDone = false;
        }
    }

	// load LoadingScene and load NextScene
	public void LoadLoadingScene() {
		Debug.Log("LoadLoadingScene");
		SceneManager.LoadScene(4);
		StartCoroutine(LoadNextScene());
	}


	IEnumerator LoadNextScene() {
		yield return null;

		AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneNum);
		op.allowSceneActivation = false;

		float timer = 0.0f;
		while (!op.isDone) {
			yield return null;

			timer += Time.deltaTime;
			if (op.progress >= 0.9f && timer > 3) {
				op.allowSceneActivation = true;
			}
		}
        StartCoroutine(FadeIn());
	}
    // on Loading, Fade Out
    IEnumerator FadeOut() {
		float timer = 0f;
		float fadeTime = 1.5f;

		while (timer < fadeTime) {
			yield return new WaitForEndOfFrame();

			timer += Time.deltaTime;
			fadeColor.a = Mathf.Clamp01(timer / fadeTime);
			foreach (var fi in fadeImage)
        		fi.color = fadeColor;
		}
        FadeOut_isDone = true;
	}
    // After Loading, Fade in to next scene
    IEnumerator FadeIn() {
		float timer = 0f;
		float fadeTime = 1.5f;

		while (timer < fadeTime) {
			yield return new WaitForEndOfFrame();

			timer += Time.deltaTime;
			fadeColor.a = 1.0f - Mathf.Clamp01(timer / fadeTime);
			foreach (var fi in fadeImage)
        		fi.color = fadeColor;
		}

		Debug.Log("Loaded Scene Completely");

	}
	
}