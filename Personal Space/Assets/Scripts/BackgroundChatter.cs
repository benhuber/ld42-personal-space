using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class BackgroundChatter : MonoBehaviour {

    public Transform localdisplay;
    bool active = false;
    public Vector3 offset;
    AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void Display(string text, Color color, float plusDuration, AudioClip clip)
    {
        Transform displayclone = Instantiate(localdisplay, transform.position + offset, transform.rotation ,this.transform);
        TextMeshProUGUI tmp = displayclone.GetComponentInChildren<TextMeshProUGUI>();
        tmp.color = color;
        tmp.text = text;


        Destroy(displayclone, clip.length + plusDuration);
    }
}
