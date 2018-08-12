using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annoying : MonoBehaviour {

    public SpriteRenderer sr;
    public float value = 1f;
    public bool setColorBasedOnValue;

    private void FixedUpdate()
    {
        if (setColorBasedOnValue && sr != null)
        {
            Color c = Color.Lerp(Color.cyan, Color.red, value / 2);
            sr.color = c;
        }
    }
}
