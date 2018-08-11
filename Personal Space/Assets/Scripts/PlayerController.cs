using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [HideInInspector]
    public Vector2 direction;

	void Update () {

        float dir_x = Input.GetAxisRaw("Horizontal");
        float dir_y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(dir_x, dir_y);
    }
}
