using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour {

	TextMeshProUGUI clocktext;
    bool nightowl = false;

	// Use this for initialization
	void Start () {
		clocktext = GetComponentInChildren<TextMeshProUGUI>();
	}
	
	float myTime = 0f;
	// Update is called once per frame
	private void FixedUpdate() {
		myTime += Time.fixedDeltaTime;
		int timeInMin = (int)(myTime / 60f * 20f);
		int hour = (20 + timeInMin/60);
		int min = (timeInMin%60);
		bool sep = (myTime % 2) < 1;
		clocktext.text = hour + (sep?":":" ") + (min<10?"0":"") + min;

        if (hour >= 23 && !nightowl) NightOwl();
	}

    void NightOwl()
    {
        nightowl = true;
        FindObjectOfType<PersistentDataComponent>().CompleteAnAchievment(PersistentDataComponent.EAchievement.EAchievement_NightOwl);
        MessageHandler.me.EnqueMessage("Archievement 'Night Owl': You stayed at the party longer than expected!");
    }
}
