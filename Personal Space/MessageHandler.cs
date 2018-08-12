using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageHandler : MonoBehaviour {

    public RectTransform Rtext;
    public TextMeshProUGUI text;

    Vector3 originalPos;

    bool coming;
    bool waiting;
    bool going;

    public float Debug;

	// Use this for initialization
	void Start () {
        originalPos = text.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Debug = Rtext.transform.position.x;

        if (coming)
        {
            if(Rtext.transform.position.x >= 0f) { }

        }
		
	}


}
