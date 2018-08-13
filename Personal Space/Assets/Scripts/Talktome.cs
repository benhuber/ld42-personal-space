using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talktome : MonoBehaviour {

    public Dialogbox.Dialogstate[] ds;
    public Dialogbox.Dialogtransition[] dt;

    public bool instigatesDialog = true;
    public bool resets = false;
    public float resettime = 10f;

    public string name = "this person";

    [HideInInspector]
    public GameObject Dialog;
    public LayerMask Lplayer;

    bool inDialog = false;
    public float talkradius = .6f;

    bool done = false;
    float doneuntill;

    public void Start()
    {
        RegisterIfNeeded();
    }

    void RegisterIfNeeded() {
        if (!done && !instigatesDialog) {
            InteractionManager.RegisterInteraction(OnInteract, this.transform, "talk to "+name, 1);
        }
    }

    void OnInteract() {
        Dialog.SetActive(true);
        Dialogbox.Dialogsystem.StartDialog(ds, dt);
        done = true;
        InteractionManager.RemoveInteraction(this.transform);
        doneuntill = Time.fixedTime + resettime;
    }

    [HideInInspector] public float myTime = 0f;

    public void FixedUpdate () {
        myTime += Time.fixedDeltaTime;

        if (instigatesDialog && !done) {
            Collider2D[] personalSpaceColliders = Physics2D.OverlapCircleAll(transform.position, 2f, Lplayer);
            if (personalSpaceColliders.Length > 0) {
                OnInteract();
            }
        }
        if (Time.fixedTime > doneuntill && resets) {
            done = false;
            RegisterIfNeeded();
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, talkradius);
    }
}
