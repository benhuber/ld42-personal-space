using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stressbar : MonoBehaviour {
    public RectTransform rt;
    public RectTransform resolutioninfo;
    public Image fill;
    public TextMeshProUGUI text;
    public float minval = 0;
    public float maxval = 300;
    public float minloc = 1825;
    public float maxloc = 80;
    public Color calm;
    public Color stressed;

    public float d;

    void Start()
    {
        
    }


	// Update is called once per frame
	void Update () {
        minloc = resolutioninfo.rect.width;


        Debug.Log("pos: " + rt.position.x);
        Debug.Log("rect: " + rt.rect.width);
        Debug.Log("pos + rect: " + rt.position.x  + rt.rect.width);

        float a = PlayerStatus.thePlayer.annoyance;
        d = (minloc - maxloc) / maxval * a;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, d);
        Color c = Color.Lerp(calm, stressed, a / maxval);
        c.a = 1;
        //c = Color.blue;
        fill.color = c;
        text.color = c;
	}
}
