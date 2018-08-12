using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour {
	AsyncOperation sceneLoader;

	private void Update() {
		if (sceneLoader == null) {
			// needs to be here instead of Awake due to a known bug...
			sceneLoader = SceneManager.LoadSceneAsync(1);
			sceneLoader.allowSceneActivation = false;
		}
	}

	public void BtnPress() {
		sceneLoader.allowSceneActivation = true;
	}
}
