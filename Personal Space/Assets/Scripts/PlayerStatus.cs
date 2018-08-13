using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    public bool devmode = true;

    public static PlayerStatus thePlayer;

    public float annoyance = 0f;

    public float radius = 3f;
    public LayerMask people;

    public Slider Sannoyance;

    private float calmSince;

    //flags 
    public bool foundCake = false;

    private void Awake()
    {
        thePlayer = this;
    }

    void FixedUpdate () {
        CheckArea();
	}

    private void CheckArea()
    {
        
        Collider2D[] areaPeople = Physics2D.OverlapCircleAll(transform.position, radius, people);
        foreach(Collider2D p in areaPeople)
        {
            Annoying a = p.GetComponent<Annoying>();
            if (a == null) continue;
            float change = ((radius - Mathf.Min(Vector2.Distance(p.transform.position, transform.position), radius)) * a.value);
            ChangeAnnoyance(change);
        }

        if (annoyance > 1) {
            if (calmSince + 4f < Time.time) ChangeAnnoyance(-.6f);
            else if (calmSince + 2f < Time.time) ChangeAnnoyance(-.4f);
            else if (calmSince + 1f < Time.time) ChangeAnnoyance(-.2f);
        }
        
        Sannoyance.value = annoyance;
    }


    public void ChangeAnnoyance (float ann)
    {
        annoyance += ann;

        if(ann>0) calmSince = Time.time;

        if (annoyance >300 && !devmode)
        {
            PLACEHOLDER_DATA.data.ending = PLACEHOLDER_DATA.Endings.Stress;
            PersonSafe.safe.TriggerEnd();
        }

        if (annoyance > 300) annoyance = 300;
        if (annoyance < 0) annoyance = 0;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
