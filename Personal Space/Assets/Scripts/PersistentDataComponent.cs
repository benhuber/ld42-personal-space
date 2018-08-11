using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataComponent : MonoBehaviour {

	public enum EEndings
	{
		EEndings_StressOverload,
		EEndings_AllQuestCompleted,
		EEndings_BackHome,
		EEndings_NumberOfEndings
	}

	private	bool[] endingsCompleted = new bool[(int)EEndings.EEndings_NumberOfEndings];
	
	void awake () {
		DontDestroyOnLoad(this.gameObject);
	}

	void Start () {
		
	}
	
		void Update () {
		
	}

	public bool IsEndingCompleted(EEndings ending)
	{
		return endingsCompleted[(int) ending];
	}
}
