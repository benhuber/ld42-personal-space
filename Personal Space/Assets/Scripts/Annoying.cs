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
        if (setColorBasedOnValue && sr != null) {
            if (value < 1f) {
                value = 0;
                sr.color = Color.gray;   
            } else {
                sr.color = Color.red;
            }
        }
    }
}
