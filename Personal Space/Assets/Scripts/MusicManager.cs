using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	AudioSource music;
	AudioLowPassFilter musicFilter;
	AudioSource heart;

	void Start () {
		music = transform.Find("Music").GetComponent<AudioSource>();
		musicFilter = transform.Find("Music").GetComponent<AudioLowPassFilter>();
		heart = transform.Find("Heart").GetComponent<AudioSource>();
	}
	
	
	float lerpedStress = 0;

	void FixedUpdate () {
		lerpedStress = PlayerStatus.thePlayer.annoyance;// Mathf.Lerp(lerpedStress, PlayerStatus.thePlayer.annoyance, 0.1f);
		lerpedStress = Mathf.Min(lerpedStress, 300f);
		musicFilter.cutoffFrequency = 22000 - lerpedStress * 22000f/300f;
		if (lerpedStress < 100) {
			musicFilter.cutoffFrequency = Mathf.Lerp(22000, 2000, lerpedStress/100);
			music.volume = 1f;
			heart.volume = 0f;
		} else if (lerpedStress < 200) {
			musicFilter.cutoffFrequency = Mathf.Lerp(2000, 300, (lerpedStress-100)/100);
			music.volume = 1f;
			heart.volume = (lerpedStress-100)/100f;
		} else {
			musicFilter.cutoffFrequency = Mathf.Lerp(300, 10, (lerpedStress-200)/100);
			heart.volume = 1;
			music.volume = 1 - (lerpedStress-200)/100f;
		}
	}
}
