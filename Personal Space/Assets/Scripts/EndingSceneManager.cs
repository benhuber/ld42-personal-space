using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour {

	public TextMeshProUGUI nameText;

	public FaderScript fader;

	private bool waitingForLoad = false;
	// Use this for initialization
	void Start () {
		fader.StartFadeOut();
	}
	
	// Update is called once per frame
	void Update () {
		if (waitingForLoad && fader.GetState() == FaderScript.EFaderstate.none)
		{
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
		}
	}

	public void OnMenuButtonClick()
	{
		fader.StartFadeIn();
		waitingForLoad = true;
	}

	public void SetNameText(string text)
	{
		nameText.text = text;
	}
}
