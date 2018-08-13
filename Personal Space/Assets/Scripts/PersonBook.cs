using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBook : Talktome
{

    // DIALOG
    public Sprite Portrait;
    //BC
    BackgroundChatter bc;
    Color col;
    public AudioClip hohoclip;
    //PATHFINDING
    public Transform[] PathA;

    PersonMotor pm;

    bool started = false;
    public float Startwalkingafter = 165f;

    int i = 0;

    private void Awake() {
        name = "Nasim";
    }

    new void Start()
    {
        base.Start();
        //BC
        bc = GetComponent<BackgroundChatter>();
        //DIALOG
        Dialog = Dialogbox.Dialogsystem.gameObject;
        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true };
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Nasim", Message = "Hey, you made it. Did you bring my book along by any chance?", optionA = "Yes, thanks for lending it.", optionB = "What..? Oh, yes. Here you go.", oA_changeval = 0f, oB_changeval = 0f, end = false };
        Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = Dend };
        ds = new Dialogbox.Dialogstate[2];
        ds[0] = ds0;
        ds[1] = Dend;
        dt = new Dialogbox.Dialogtransition[1];
        dt[0] = dt0;
        //PATHFINDING
        pm = GetComponent<PersonMotor>();

    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        //BC
        if (!started && myTime > Startwalkingafter) {
            pm.StartWalking(PathA);
            started = true;
        }

    }
}
