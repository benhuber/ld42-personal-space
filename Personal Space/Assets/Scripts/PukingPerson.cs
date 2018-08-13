using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukingPerson : MonoBehaviour {

	GameObject pukingEvent;
	CrowdMovement movement;
	Rigidbody2D rb;

	private void Start() {
		pukingEvent = transform.Find("Puke").gameObject;
		pukingEvent.SetActive(false);
		movement = GetComponent<CrowdMovement>();
		rb = GetComponent<Rigidbody2D>();
	}

	float myTime = 0f;
	private void FixedUpdate() {
		myTime += Time.fixedDeltaTime;
		if (myTime % 120 > 90) {
			pukingEvent.SetActive(true);
			movement.enabled = false;
			rb.velocity = new Vector3();
		} else {
			pukingEvent.SetActive(false);
			movement.enabled = true;
		}
	}
}
