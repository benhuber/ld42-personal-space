using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour {

	public TextMeshProUGUI nameText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMenuButtonClick()
	{
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

	public void SetNameText(string text)
	{
		nameText.text = text;
	}
}
