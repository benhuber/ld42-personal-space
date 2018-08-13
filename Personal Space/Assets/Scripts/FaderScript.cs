using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderScript : MonoBehaviour {

	public GameObject objectToFade;

	private float currentTime = 0.0f;

	public float fadeTime = 0.1f;

	public enum EFaderstate
	{
		fadeIn,
		fadeOut,
		none
	}

	private EFaderstate state = EFaderstate.none;

	// Use this for initialization
	void Start () {
		StartFadeOut();
	}
	
	// Update is called once per frame
	void Update () {
		if (state != EFaderstate.none)
		{
			currentTime += Time.deltaTime;

			Color color = objectToFade.GetComponent<Image>().color;
			if (state == EFaderstate.fadeIn)
			{
				color.a = easeInOutQuad(currentTime / fadeTime);
			}
			else if (state == EFaderstate.fadeOut) {
				color.a = 1 - easeInOutQuad(currentTime / fadeTime);
			}
			objectToFade.GetComponent<Image>().color = color;

			if (currentTime > fadeTime)
			{
				if (state == EFaderstate.fadeOut) {
					objectToFade.SetActive(false);
				}

				state = EFaderstate.none;
			}
		}
	}

	float easeInOutQuad( float t ) {
    	return t < 0.5 ? 2 * t * t : t * (4 - 2 * t) - 1;
	}

	public void StartFadeIn()
	{
		Color color = objectToFade.GetComponent<Image>().color;
		color.a = 0.0f;
		objectToFade.GetComponent<Image>().color = color;

		objectToFade.SetActive(true);
		currentTime = 0.0f;
		state = EFaderstate.fadeIn;
	}

	public void StartFadeOut()
	{
		Color color = objectToFade.GetComponent<Image>().color;
		color.a = 1.0f;
		objectToFade.GetComponent<Image>().color = color;

		objectToFade.SetActive(true);
		currentTime = 0.0f;
		state = EFaderstate.fadeOut;
	}

	public EFaderstate GetState ()
	{
		return state;
	}
}
