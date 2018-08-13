using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PersistentDataComponent : MonoBehaviour {

	public enum EEndings
	{
		EEndings_StressOverload,
		EEndings_AllQuestCompleted,
		EEndings_BackHome,
		EEndings_NumberOfEndings
	}

	private	bool[] endingsCompleted = new bool[(int)EEndings.EEndings_NumberOfEndings];
	private static bool created = false;
	
	void Start () {
		if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
		}	
	}
	
	void Update () {
		
	}

	public void CompleteAnEnding(EEndings ending)
	{
		Assert.IsTrue((int) ending < (int) EEndings.EEndings_NumberOfEndings);
		endingsCompleted[(int) ending] = true;
	}

	public bool IsEndingCompleted(EEndings ending)
	{
		Assert.IsTrue((int) ending < (int) EEndings.EEndings_NumberOfEndings);
		return endingsCompleted[(int) ending];
	}
}
