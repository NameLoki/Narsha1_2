using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
	
	public Image rightImage;		// Right of progressbar
	private RectTransform rect;		// Progressbar image's position, scale

	private float original_xPos;	// First start xPosition
	private float yPos;				// Keep fasten yPosition
	private float xScale;			// To extend image horizontally

	public int sec;		// Song Length
	float timer = 0f;
	

	void Start () {
		rightImage = GameObject.Find("ProgressBarRight").GetComponent<Image>();
		rect = GetComponent<RectTransform>();

		original_xPos = rightImage.rectTransform.anchoredPosition.x;
		yPos = GetComponent<RectTransform>().anchoredPosition.y;
		xScale = 0;

		sec = Camera.main.GetComponent<ChaeBoSystem>().musicLength - 2;
		StartCoroutine(Progress());
	}
	

	IEnumerator Progress() {
		float right_xPos = 0f;	// Right image's xPos
		float progress = 0f;	// Overall progress
		float progress_xPos = 0f;		// ProgressBar image's xPos

		while (timer < sec) {
			yield return new WaitForEndOfFrame();
			
			timer += Time.deltaTime;
			progress = timer / sec;
			xScale = progress;

			progress_xPos = Mathf.Lerp(original_xPos, 0, progress);
			right_xPos = Mathf.Lerp(original_xPos, 872, progress);

			rightImage.rectTransform.anchoredPosition = new Vector2(right_xPos, yPos);	// Set new position of rightImage
			rect.anchoredPosition = new Vector3(progress_xPos, yPos, 0);			// Set new positioin of ProgressBar
			rect.localScale = new Vector3(xScale, 1, 1);							// Extend ProgressBar horizontally
		}
	}

}
