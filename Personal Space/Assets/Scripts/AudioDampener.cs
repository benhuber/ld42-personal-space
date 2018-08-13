using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDampener : MonoBehaviour {

	AudioSource audioSource;
	AudioLowPassFilter musicFilter;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		musicFilter = GetComponent<AudioLowPassFilter>();
	}
	
	void FixedUpdate () {
		float lerpedStress = PlayerStatus.thePlayer.annoyance;
		lerpedStress = Mathf.Min(lerpedStress, 300f);

		if (lerpedStress < 100) {
			// 0 - 100: cutoff increasing, heartbeat slowly appears
			musicFilter.cutoffFrequency = Mathf.Lerp(22000, 2000, lerpedStress/100);
			audioSource.volume = 1f;
		} else if (lerpedStress < 200) {
			// 100 - 200: cutoff clearly distort music, heartbeat audible
			musicFilter.cutoffFrequency = Mathf.Lerp(2000, 300, (lerpedStress-100)/100);
			audioSource.volume = 1f;
		} else {
			// 200 - 300: heartbeat at full volume, music drowning out
			musicFilter.cutoffFrequency = Mathf.Lerp(300, 10, (lerpedStress-200)/100);
			audioSource.volume = 1 - (lerpedStress-200)/100f;
		}
	}
}
