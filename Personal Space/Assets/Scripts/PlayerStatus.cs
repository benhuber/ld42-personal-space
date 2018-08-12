using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    public static PlayerStatus thePlayer;

    [SerializeField][Range(0f, 300f)]
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
            ChangeAnnoyance((radius-Vector2.Distance(p.transform.position, transform.position))*a.value);
            calmSince = Time.time;
        }

        if (annoyance > 1) {
            if (calmSince + 4f < Time.time) ChangeAnnoyance(.6f);
            else if (calmSince + 2f < Time.time) ChangeAnnoyance(.4f);
            else if (calmSince + 1f < Time.time) ChangeAnnoyance(.2f);
        }
        
        Sannoyance.value = annoyance;
    }


    public void ChangeAnnoyance (float ann)
    {
        annoyance += ann;
        if (annoyance > 300) annoyance = 300;
        if (annoyance < 0) annoyance = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
