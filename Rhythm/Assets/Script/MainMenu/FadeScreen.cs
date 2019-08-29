using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    public SpriteRenderer blackScreen;

    void Start() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void fadeOut() {
        StartCoroutine(fadeOuter());
    }

    public void fadeIn() {
        StartCoroutine(fadeInner());
    }

    IEnumerator fadeOuter() {
        float timer = 0f;

        while(timer < 1f) {
            yield return new WaitForEndOfFrame();
            blackScreen.color = new Color(1f, 1f, 1f, timer);
            timer += Time.deltaTime;
        }
        SceneManager.LoadScene(1);
    }

    IEnumerator fadeInner() {
        float timer = 0f;

        yield return new WaitForSeconds(1f);

        while(timer < 0.5f) {
            yield return new WaitForEndOfFrame();
            blackScreen.color = new Color(1f, 1f, 1f, 1 - timer * 2);
            timer += Time.deltaTime;
        }
    }


}
