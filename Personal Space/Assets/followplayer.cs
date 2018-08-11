using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour {
    
	// Update is called once per frame
	void FixedUpdate() {
        Vector2 player = PlayerStatus.thePlayer.transform.position;
        transform.position = new Vector3(player.x,player.y, -10);
	}
}
