using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtGrabber : MonoBehaviour {

	GameObject grabbingEvent;
	public LayerMask peopleLayer;

	private void Start() {
		grabbingEvent = transform.Find("Butt").gameObject;
		grabbingEvent.SetActive(false);
	}

	private void FixedUpdate() {
		Collider2D[] personalSpaceColliders = Physics2D.OverlapCircleAll(transform.position, 2f, peopleLayer).Where(x=>x.gameObject != gameObject).ToArray();
		grabbingEvent.SetActive(personalSpaceColliders.Length > 0);
	}
}
