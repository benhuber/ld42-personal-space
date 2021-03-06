﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchievementSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
protected PersistentDataComponent dataCom;
	public PersistentDataComponent.EAchievement achievement = PersistentDataComponent.EAchievement.EAchievement_None;
	public EndingSceneManager endingManager;
	public string text;
	private Color unlockedColor = new Color(0.1462264f, 1.0f, 0.2670789f, 1.0f);
	private Color lockedColor = new Color(0.3773585f, 0.3773585f, 0.3773585f, 1.0f);

	void Start () {
		// Get the data component
		dataCom = GameObject.Find("DataManager").GetComponent<PersistentDataComponent>();
		Assert.IsNotNull(dataCom, "Error when fetching data component");

//////// DEBUG
		//dataCom.CompleteAnEnding(ending);
//////// ~DEBUG

		if (dataCom.IsAchievmentCompleted(achievement))
		{
			GetComponent<Image>().color = unlockedColor;
		}
		else
		{
			GetComponent<Image>().color = lockedColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        endingManager.SetNameText(text);

		Color newColor = GetComponent<Image>().color;
		newColor.r = newColor.r + 0.2f;
		newColor.g = newColor.g + 0.2f;
		newColor.b = newColor.b + 0.2f;
		GetComponent<Image>().color = newColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        endingManager.SetNameText("");

		Color newColor = GetComponent<Image>().color;
		newColor.r = newColor.r - 0.2f;
		newColor.g = newColor.g - 0.2f;
		newColor.b = newColor.b - 0.2f;
		GetComponent<Image>().color = newColor;
    }
}
