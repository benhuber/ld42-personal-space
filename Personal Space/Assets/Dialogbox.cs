using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogbox : MonoBehaviour {

    public static Dialogbox Dialogsystem;

	// Use this for initialization
	void Awake () {
        Dialogsystem = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartDialog()
    {
        Time.timeScale = 0f;
    }

    public void EndDialog()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);

    }
}
