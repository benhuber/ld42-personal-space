using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour {

	public GameObject CrowdPrefab;

	// Use this for initialization
	void Start () {
		

	}

	void AddRandom() {
		CrowdMovement newP = Instantiate(CrowdPrefab, this.transform.position, this.transform.rotation).GetComponent<CrowdMovement>();
		newP.gameObject.SetActive(false);
		arravingPeople.Add(newP.gameObject);
	}
	
	// total game time 6min = 360 sec
	List<GameObject> arravingPeople = new List<GameObject>();
	List<float> spawnTimes = new List<float>{20, };
	int lastSpawn = 0;
	float myTime = 0f;
	private void FixedUpdate() {
		
	}
}
