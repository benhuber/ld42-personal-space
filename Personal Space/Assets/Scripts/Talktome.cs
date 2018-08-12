using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talktome : MonoBehaviour {

    public Dialogbox.Dialogstate[] ds;
    public Dialogbox.Dialogtransition[] dt;

    public bool instigatesDialog = true;
    public bool resets = false;
    public float resettime = 10f;
    bool keypressed;

    [HideInInspector]
    public GameObject Dialog;
    public LayerMask Lplayer;

    bool inDialog = false;
    public float talkradius = .6f;

    bool done = false;
    float doneuntill;

    private void Update()
    {
        keypressed = Input.GetKey(KeyCode.E);
    }

    public void FixedUpdate () {
        if (Time.fixedTime > doneuntill) done = false;
        if (done) return;

        Collider2D player = Physics2D.OverlapCircle(transform.position, talkradius, Lplayer);
        if (player != null)
        {
            if (!instigatesDialog && !keypressed) return;
            Dialog.SetActive(true);
            Dialogbox.Dialogsystem.StartDialog(ds, dt);
            done = true;
            doneuntill = Time.fixedTime + resettime;
        }
	}



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, talkradius);
    }
}
