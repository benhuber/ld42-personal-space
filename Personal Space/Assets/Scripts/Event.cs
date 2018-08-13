using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour {

    GameObject icon;
    GameObject scene;
    public bool oneShot = false;
    public float stressChange = 0f;

    SpriteRenderer sr;
    bool growing;
    bool extended;
    bool shrunk = true;
    bool shrinking;

    Vector3 origin;

    public float speed = 1.5f;
    public float amplitude = .6f;

    public LayerMask player;
    public float radius = 1f;

    public float growthspeed = .6f;


	void Start () {
        icon = transform.Find("Icon").gameObject;
        origin = icon.transform.localScale;
        scene = transform.Find("screen").gameObject;
        sr = scene.GetComponent<SpriteRenderer>();
	}

    private void Update()
    {
        if(1-Time.timeScale<.1) icon.transform.localScale = origin + new Vector3(amplitude*Mathf.Sin(Time.time*speed), amplitude * Mathf.Sin(Time.time * speed), 0);
        if (growing) scene.transform.localScale += new Vector3(growthspeed * Time.deltaTime, growthspeed * Time.deltaTime, 0);
        if(shrinking) scene.transform.localScale -= new Vector3(growthspeed * Time.deltaTime, growthspeed * Time.deltaTime, 0);
        sr.enabled = !shrunk;
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (1 - scene.transform.localScale.x < .05 && !extended) {
            extended = true;
            PlayerStatus.thePlayer.annoyance += stressChange;
            growing = false;
        }
        if (scene.transform.localScale.x < .1 && !shrunk)
        {
            shrunk = true;
            shrinking = false;
        }

        Collider2D c = Physics2D.OverlapCircle(transform.position, radius, player);
        if (c != null)
        {
            if (!extended) growing = true;
            shrinking = false;
            shrunk = false;
        }
        else if (!shrunk)
        {
            shrinking = true;
            growing = false;
            if (extended && oneShot) {
                gameObject.SetActive(false);
            }
            extended = false;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
