using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annoying : MonoBehaviour {

    public SpriteRenderer sr;
    public float value = -1f;
    public bool setColorBasedOnValue;

    private void Awake() {
        if (value < 0f) {
            value = Random.Range(0f, 2f);
        }
    }

    private void FixedUpdate()
    {
        if (setColorBasedOnValue && sr != null)
        {
            Color c = Color.Lerp(Color.cyan, Color.red, value / 2);
            sr.color = c;
        }
    }
}
