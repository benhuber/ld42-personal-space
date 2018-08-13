using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour {

	public FaderScript fader;
	private bool waitingForLoad = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (waitingForLoad && fader.GetState() == FaderScript.EFaderstate.none)
		{
			SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
		}
	}

	public void OnLoadGameClick()
	{
		fader.StartFadeIn();
		waitingForLoad = true;
	}
}
