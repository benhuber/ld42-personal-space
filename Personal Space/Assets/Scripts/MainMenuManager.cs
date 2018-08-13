using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
	public Button QuitButton;
	public Button PlayButton;
	public Button MenuButton;
	public Button CreditButton;

	public GameObject MenuBackground;
	public GameObject CreditBackground;

	// Use this for initialization
	void Start () {
		ShowMenu();
		HideCredit();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPlayButtonClick()
	{
		// Load the game scene
		SceneManager.LoadScene("IntroCutscene", LoadSceneMode.Single);
	}

	public void OnOpenCreditButtonClick()
	{
		ShowCredit();
		HideMenu();
	}

	public void OnQuitCreditButtonClick()
	{
		ShowMenu();
		HideCredit();
	}

	public void OnQuitGameButtonClick()
	{
		Application.Quit();
	}

	void HideCredit()
	{
		MenuButton.gameObject.SetActive(false);
		CreditBackground.SetActive(false);
	}
	void ShowCredit()
	{
		MenuButton.gameObject.SetActive(true);
		CreditBackground.SetActive(true);
	}
	void HideMenu()
	{
		PlayButton.gameObject.SetActive(false);
		CreditButton.gameObject.SetActive(false);
		MenuBackground.gameObject.SetActive(false);
	}
	void ShowMenu()
	{
		PlayButton.gameObject.SetActive(true);
		CreditButton.gameObject.SetActive(true);
		MenuBackground.gameObject.SetActive(true);
	}
}
