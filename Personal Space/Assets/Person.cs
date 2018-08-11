using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    Rigidbody2D rb;
    public GameObject[] nodes;
    private GameObject selectedNode;

    public float idleTime = 15f;
    private float idled;

    public float speed = 1f;
    public float distThreshhold = 1f;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        idled = Random.Range(0.1f, idleTime);

    }
	
	void FixedUpdate () {
        idled += Time.fixedDeltaTime;
        if (idled > idleTime) Move();
	}

    void Move()
    {
        if (selectedNode != null)
        {
            if (Vector2.Distance(selectedNode.transform.position, transform.position) < distThreshhold)
            {
                rb.velocity = Vector2.zero;
                idled = 0f;
                selectedNode = null;
                return;
            }
        }
        else SelectNode();
        rb.velocity = (selectedNode.transform.position - transform.position).normalized * speed;
    }

    void SelectNode()
    {
        int i = Mathf.RoundToInt(Random.Range(0, nodes.Length-1));
        selectedNode = nodes[i];
    }
}
