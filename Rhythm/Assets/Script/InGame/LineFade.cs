using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineFade : MonoBehaviour {
	public SpriteRenderer line;
	public Text combo, persent, highCombo;

	private Color newColor;


	public void PerfectLine() {
		StartCoroutine(PerfectColor());
	}

	public void GoodLine() {
		StartCoroutine(GoodColor());
	}

	IEnumerator PerfectColor() {
		float blue = 0f;
		float timer = 0f;
		float fadeTime = 0.5f;

		while (timer < fadeTime) {
			yield return new WaitForEndOfFrame();

			timer += Time.deltaTime;
			blue = Mathf.Clamp01(timer / fadeTime);
			newColor = new Color (1f, 1f, blue, 1f);
			line.color = newColor;
			combo.color = newColor;
			persent.color = newColor;
			highCombo.color = newColor;
		}
	}
	IEnumerator GoodColor() {
		float red = 0f;
		float blue = 0f;
		float timer = 0f;
		float fadeTime = 0.5f;

		while (timer < fadeTime) {
			yield return new WaitForEndOfFrame();

			timer += Time.deltaTime;
			red = Mathf.Clamp01(timer / fadeTime);
			blue = timer + 0.5f;
			newColor = new Color (red, 1f, blue, 1f);
			line.color = newColor;
			combo.color = newColor;
			persent.color = newColor;
			highCombo.color = newColor;
		}
	}
}
