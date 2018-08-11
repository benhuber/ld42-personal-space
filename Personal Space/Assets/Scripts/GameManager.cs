using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

	protected PersistentDataComponent dataCom;

	void Start () {
		// Get the datacomponent
		dataCom = GameObject.Find("DataManager").GetComponent<PersistentDataComponent>();
		Assert.IsNotNull(dataCom, "Error when fetching data component");
	}
	
	void Update () {
		
	}
}
