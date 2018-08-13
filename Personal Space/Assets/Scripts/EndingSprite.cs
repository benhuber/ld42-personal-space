using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndingSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

	protected PersistentDataComponent dataCom;
	public EndingSceneManager endingManager;
	public string text;
	public PersistentDataComponent.EEndings ending = PersistentDataComponent.EEndings.EEndings_None;

	private Color unlockedColor = new Color(0.1462264f, 1.0f, 0.2670789f, 1.0f);
	private Color lockedColor = new Color(0.3773585f, 0.3773585f, 0.3773585f, 1.0f);

	void Start () {
		// Get the data component
		dataCom = GameObject.Find("DataManager").GetComponent<PersistentDataComponent>();
		Assert.IsNotNull(dataCom, "Error when fetching data component");

//////// DEBUG
		//dataCom.CompleteAnEnding(ending);
//////// ~DEBUG

		if (dataCom.IsEndingCompleted(ending))
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        endingManager.SetNameText("");
    }
}
