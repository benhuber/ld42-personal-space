using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersonMotor : MonoBehaviour
{
    public delegate void callbacktype();

    Rigidbody2D rb;
    GameObject[] nodes;
    public GameObject selectedNode;
    callbacktype callwhendone;

    public float speed = 1f;
    public float distThreshhold = .2f;

    public bool walking = false;
    int i = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero;
        if (!walking) return;
        if (Vector2.Distance(transform.position, selectedNode.transform.position) < distThreshhold)
        {
            i++;
            if (i >= nodes.Length)
            {
                StopWalking();
                return;
            }
            else selectedNode = nodes[i];
        }
        rb.velocity = (selectedNode.transform.position - transform.position).normalized * speed;
    }

    public void StartWalking (GameObject[] newnodes, callbacktype callback )
    {
        i = 0;
        nodes = newnodes;
        selectedNode = nodes[i];
        walking = true;
        callwhendone = callback;
    }

    public void StopWalking()
    {
        walking = false;
        callwhendone();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(selectedNode != null) Gizmos.DrawLine(selectedNode.transform.position, transform.position);
    }





}
