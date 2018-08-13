using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class BackgroundChatter : MonoBehaviour {

    public Transform localdisplay;
    float doneat;
    public Vector3 offset;
    AudioSource aud;
    TextMeshProUGUI tmp;
    public float maxdist = 10f;

    public delegate void callbacktype();
    callbacktype currentcallback;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (tmp != null)
        {
            float dist = Vector2.Distance(transform.position, PlayerStatus.thePlayer.transform.position);
            float a = (maxdist - dist)/maxdist;
            Color c = tmp.color;
            c.a = a;
            tmp.color = c;
            aud.volume = a*3;
        }
    }

    public void Display(string text, Color color, float plusDuration, AudioClip clip, callbacktype callback)
    {
        Transform displayclone = Instantiate(localdisplay, transform.position + offset, transform.rotation, this.transform);
        tmp = displayclone.GetComponentInChildren<TextMeshProUGUI>();
        tmp.color = color;
        tmp.text = text;
        aud.clip = clip;
        aud.Play();
        doneat = Time.time + clip.length + plusDuration;
        Destroy(displayclone.gameObject, clip.length + plusDuration);
        currentcallback = callback;
        Invoke("Callback", clip.length + plusDuration);
    }

    public void Callback()
    {
        if (currentcallback != null) currentcallback();
    }

    public void Display(string text, Color color, float plusDuration, AudioClip clip)
    {
        Transform displayclone = Instantiate(localdisplay, transform.position + offset, transform.rotation ,this.transform);
        tmp = displayclone.GetComponentInChildren<TextMeshProUGUI>();
        tmp.color = color;
        tmp.text = text;
        aud.clip = clip;
        aud.Play();
        doneat = Time.time + clip.length + plusDuration;
        Destroy(displayclone.gameObject, clip.length + plusDuration);
    }

    public bool AmIIdle()
    {
        return (Time.time > doneat);
    }
}
