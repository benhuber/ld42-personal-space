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

    private void Start()
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
            aud.volume = a;
        }
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
