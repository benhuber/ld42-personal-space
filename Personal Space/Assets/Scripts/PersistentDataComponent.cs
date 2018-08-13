using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PersistentDataComponent : MonoBehaviour {

	public enum EEndings
	{
		EEndings_Default,
		EEndings_AllFriendsDone,
		EEndings_Stay,
		EEndings_Stress,
		EEndings_Time,
		EEndings_NumberOfEndings
	}

	public enum EAchievement
	{
		EAchievement_NightOwl,
		EAchievement_FreeLikeABird,
		EAchievement_ThereWillBeCake,
		EAchievement_Bookclub,
		EAchievement_Prosocial, // ?
		EAchievement_FancyDancer, // ?
		EAchievement_NumberOfAchievement
	}

	private	bool[] endingsCompleted = new bool[(int)EEndings.EEndings_NumberOfEndings];
	private	bool[] achievementsCompleted = new bool[(int)EEndings.EEndings_NumberOfEndings];

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

	public void CompleteAnAchievment(EAchievement achievement)
	{
		Assert.IsTrue((int) achievement < (int) EAchievement.EAchievement_NumberOfAchievement);
		achievementsCompleted[(int) achievement] = true;
	}

	public bool IsAchievmentCompleted(EAchievement achievement)
	{
		Assert.IsTrue((int) achievement < (int) EAchievement.EAchievement_NumberOfAchievement);
		return achievementsCompleted[(int) achievement];
	}
}
