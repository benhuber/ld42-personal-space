using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpeedup : MonoBehaviour {

	public float speed = 2f;

	private void FixedUpdate() {
		if (Time.timeScale > 0.1f) {
			Time.timeScale = speed;
		}
	}
}
