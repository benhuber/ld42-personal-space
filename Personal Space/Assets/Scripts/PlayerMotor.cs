using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour {

    private Rigidbody2D rb;
    private PlayerController pc;

    public float speed = 1f;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
	}

	void FixedUpdate () {
        Move();
	}

    private void Move()
    {
        Vector2 velocity = pc.direction * speed;
        rb.velocity = velocity;
        if(velocity.magnitude  > .1f)rb.rotation = Vector2.SignedAngle(Vector2.up, velocity);

    }
}
