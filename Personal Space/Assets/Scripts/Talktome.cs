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

    tut displayhelper;
    static List<Talktome> interactive = new List<Talktome>();
    public void Start()
    {
        displayhelper = tut.me;
    }

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
            if (!instigatesDialog && !keypressed)
            {
                displayhelper.interactDisplay.SetActive(true);
                interactive.Add(this);
                return;
            }
            
            Dialog.SetActive(true);
            Dialogbox.Dialogsystem.StartDialog(ds, dt);
            done = true;
            doneuntill = Time.fixedTime + resettime;
        }
        else
        {
            if (interactive.Contains(this)) interactive.Remove(this);
        }
        if(interactive.Count == 0) displayhelper.interactDisplay.SetActive(false);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, talkradius);
    }
}
