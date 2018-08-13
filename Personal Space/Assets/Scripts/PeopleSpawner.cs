using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour {

	public Transform defaultTarget;
	public Transform waveOneTarget;
	public Transform waveTwoTarget;
	public Transform waveThreeTarget;

	public List<GameObject> peoplePool;
	int nextPoolIdx = 0;
	List<GameObject> arravingPeople = new List<GameObject>();
	List<float> spawnTimes = new List<float>();
	int nextSpawn = 0;


	// Use this for initialization
	void Start () {
		for (int i=0; i<transform.childCount; ++i) {
			peoplePool.Add(transform.GetChild(i).gameObject);
			peoplePool[peoplePool.Count-1].SetActive(false);
		}

		spawnTimes.Add(20f); AddRandom();
		spawnTimes.Add(40f); AddRandom();
		spawnTimes.Add(60f); AddRandom();
		// first wave at 80 sec
		spawnTimes.AddRange(new float[]{80,83,86,89,92,95,98,101});
		AddWave(8, waveOneTarget);

		spawnTimes.Add(120f); AddRandom();
		spawnTimes.Add(140f); AddRandom();
		// first wave at 160 sec
		spawnTimes.AddRange(new float[]{160,163,166});
		AddWave(3, waveTwoTarget);

		spawnTimes.Add(180f); AddRandom();
		spawnTimes.Add(200f); AddRandom();

		// second wave at 220 sec
		spawnTimes.AddRange(new float[]{220,223,226,229,232,235,238,241});
		AddWave(8, waveThreeTarget);
		
		spawnTimes.Add(260f); AddRandom();
		spawnTimes.Add(280f); AddRandom();
		spawnTimes.Add(300f); AddRandom();
		spawnTimes.Add(320f); AddRandom();
		spawnTimes.Add(340f); AddRandom();

		// after 22:00 = 360 sec
		for (float t = 360f; t<360+150; t += 10f) {
			spawnTimes.Add(t); AddRandom();
		}
	}

	CrowdMovement GetNextPerson() {
		// Debug.Log(nextPoolIdx + " <? " + peoplePool.Count);
		CrowdMovement newP = peoplePool[nextPoolIdx].GetComponent<CrowdMovement>();
		nextPoolIdx += 1;
		return newP;
	}

	void AddWave(int count, Transform target) {
		for (int i=0; i<count; ++i) {
			CrowdMovement newP = GetNextPerson();
			newP.transform.position = this.transform.position;
			newP.transform.rotation = this.transform.rotation;
			newP.ForceWalkTo(target.position);
			newP.dontLeaveTheRoom = true;
			arravingPeople.Add(newP.gameObject);
		}
	}

	void AddRandom() {
		CrowdMovement newP = GetNextPerson();
		newP.transform.position = this.transform.position;
		newP.transform.rotation = this.transform.rotation;
		newP.ForceWalkTo(defaultTarget.position);
		arravingPeople.Add(newP.gameObject);
	}
	
	// total game time 6min = 360 sec
	
	float myTime = 0f;
	private void FixedUpdate() {
		myTime += Time.fixedDeltaTime;
		if (nextSpawn < spawnTimes.Count && spawnTimes[nextSpawn] <= myTime) {
			arravingPeople[nextSpawn].SetActive(true);
			nextSpawn += 1;
		}
	}
}
