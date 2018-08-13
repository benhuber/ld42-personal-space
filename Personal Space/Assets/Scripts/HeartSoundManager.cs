using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSoundManager : MonoBehaviour {

	AudioSource heart;

	public static HeartSoundManager theHeart;

	private void Awake() {
		theHeart = this;
	}

	void Start () {
		heart = GetComponent<AudioSource>();
	}
	
	public float GetBeatTime() {
		return heart.time;
	}

	void FixedUpdate () {
		float lerpedStress = PlayerStatus.thePlayer.annoyance;
		lerpedStress = Mathf.Min(lerpedStress, 300f);

		if (lerpedStress < 100) {
			// 0 - 100: cutoff increasing, heartbeat slowly appears
			heart.volume = (lerpedStress)/200f;
		} else if (lerpedStress < 200) {
			// 100 - 200: cutoff clearly distort music, heartbeat audible
			heart.volume = (lerpedStress)/200f;
		} else {
			// 200 - 300: heartbeat at full volume, music drowning out
			heart.volume = 1;
		}
	}
}
