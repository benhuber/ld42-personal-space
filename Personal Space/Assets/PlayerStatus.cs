using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    [SerializeField]
    private float annoyance = 0f;

    public float radius = 3f;
    public LayerMask people;

    public Slider Sannoyance;

    private float calmSince;

	void FixedUpdate () {
        CheckArea();
	}

    private void CheckArea()
    {
        
        Collider2D[] areaPeople = Physics2D.OverlapCircleAll(transform.position, radius, people);
        foreach(Collider2D p in areaPeople)
        {
            annoyance += Vector2.Distance(p.transform.position, transform.position);
            calmSince = Time.time;
        }

        if (annoyance > 1) {
            if (calmSince + 4f < Time.time) annoyance -= .6f;
            else if (calmSince + 2f < Time.time) annoyance -= .4f;
            else if (calmSince + 1f < Time.time) annoyance -= .2f;
        }
        
        Sannoyance.value = annoyance;
    }


    public void ChangeAnnoyance (float ann)
    {
        annoyance += ann;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
