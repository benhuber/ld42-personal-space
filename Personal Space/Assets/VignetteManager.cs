using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteManager : MonoBehaviour {

	Kino.Vignette vignette;
	MusicManager music;
	public float maxVignette = 2f;

	// Use this for initialization
	void Start () {
		vignette = GetComponent<Kino.Vignette>();
		music = GameObject.FindObjectOfType<MusicManager>();
	}
	
	void Update () {
		float a = PlayerStatus.thePlayer.annoyance;
		a = Mathf.Min(a, 300f);
		vignette._falloff = a/300f * maxVignette;
		if (a > 100) {
			float alpha = 1 - (music.GetBeatTime() % 1);
			vignette._alpha = Mathf.Lerp(0f,0.25f,(a-100f)/100f) * alpha*alpha;
		} else {
			vignette._alpha = 0f;
		}
		

	}
}
