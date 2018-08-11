using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talktome : MonoBehaviour {

    public GameObject Dialog;
    public LayerMask Lplayer;

    bool inDialog = false;
    public float talkradius = 2f;

    bool done = false;

	void Start () {
		
	}

    void FixedUpdate () {
        if (done) return;
        Collider2D player = Physics2D.OverlapCircle(transform.position, talkradius, Lplayer);
        if (player != null)
        {
            Dialog.SetActive(true);
            Dialogbox.Dialogsystem.StartDialog();
            done = true;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, talkradius);
    }

}
