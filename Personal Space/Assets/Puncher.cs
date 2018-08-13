using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour {

	GameObject punchingEvent;
	CrowdMovement movement;
	Rigidbody2D rb;
	public Transform punchPosition;

	private void Start() {
		punchingEvent = transform.Find("Punch").gameObject;
		punchingEvent.SetActive(false);
		movement = GetComponent<CrowdMovement>();
		rb = GetComponent<Rigidbody2D>();
	}

	float myTime = 0f;
	private void FixedUpdate() {
		myTime += Time.fixedDeltaTime;
		if (myTime % 180 > 120) {
			// punching
			if ((punchPosition.position - transform.position).magnitude > 0.5f) {
				movement.ForceWalkTo(punchPosition.position);
			} else {
				movement.enabled = false;
				punchingEvent.SetActive(true);
				rb.velocity = new Vector3();
			}
		} else {
			// crowding
			punchingEvent.SetActive(false);
			movement.enabled = true;
		}
	}
}
