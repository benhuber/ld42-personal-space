﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersonMotor : MonoBehaviour
{
    public delegate void callbacktype();

    Rigidbody2D rb;
    Transform[] nodes;
    public Transform selectedNode;
    callbacktype callwhendone;
    public GameObject GFX;

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
        if (!walking || selectedNode == null) return;
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
        Vector2 velocity = (selectedNode.transform.position - transform.position).normalized * speed;
        rb.velocity = velocity;
        if (velocity.magnitude > .1f) GFX.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(velocity.x, velocity.y, 0));
    }

    public void StartWalking (Transform[] newnodes, callbacktype callback )
    {
        i = 0;
        nodes = newnodes;
        selectedNode = nodes[i];
        walking = true;
        callwhendone = callback;
    }

    public void StartWalking(Transform[] newnodes)
    {
        i = 0;
        nodes = newnodes;
        selectedNode = nodes[i];
        walking = true;
    }

    public void StopWalking()
    {
        walking = false;
        if(callwhendone != null) callwhendone();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(selectedNode != null) Gizmos.DrawLine(selectedNode.transform.position, transform.position);
    }





}
