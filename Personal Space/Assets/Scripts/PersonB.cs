using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonB : Talktome
{
    // DIALOG
    public Sprite Portrait;
    //BC
    BackgroundChatter bc;
    Color col;
    public AudioClip hohoclip;
    //PATHFINDING
    public Transform[] PathA;
    public Transform[] PathB;
    public Transform[] PathC;
    public Transform[] PathD;
    public Transform[] PathE;
    PersonMotor pm;

    public List<Transform[]> PATHS = new List<Transform[]>();
    int i = 0;

    void Start()
    {
        //BC
        bc = GetComponent<BackgroundChatter>();
        //DIALOG
        Dialog = Dialogbox.Dialogsystem.gameObject;
        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true };
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate {Avatar = Portrait, Title = "Art Dealer", Message = "Do you want to buy that picture?", optionA = "How much is it?", optionB = "what..no!", oA_changeval = 0f, oB_changeval = 0f, end = false };
        Dialogbox.Dialogstate ds1 = new Dialogbox.Dialogstate {Avatar = Portrait, Title = "Art Dealer", Message = "500.000€", optionA = "Can we talk about sth else", optionB = "no bye,", oA_changeval = 0f, oB_changeval = 0f, end = false };
        Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = ds1, oB_followup = Dend };
        Dialogbox.Dialogtransition dt1 = new Dialogbox.Dialogtransition { origial = ds1, oA_followup = ds0, oB_followup = Dend };
        ds = new Dialogbox.Dialogstate[3];
        ds[0] = ds0;
        ds[1] = ds1;
        ds[2] = Dend;
        dt = new Dialogbox.Dialogtransition[2];
        dt[0] = dt0;
        dt[1] = dt1;
        //PATHFINDING
        pm = GetComponent<PersonMotor>();
        PATHS.Add(PathA);
        PATHS.Add(PathB);
        PATHS.Add(PathC);
        PATHS.Add(PathD);
        PATHS.Add(PathE);
        pm.StartWalking(PATHS[0], NextBehavior);
    }

    public void NextBehavior()
    {
        i++;
        if (i >= PATHS.Count) i = 0;
        pm.StartWalking(PATHS[i], NextBehavior);
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
        //BC
        if (bc.AmIIdle())
        {
            if (col == Color.red) col = Color.green;
            else col = Color.red;
            bc.Display("HOHOHOHOHOHO", col, 3f, hohoclip);
        }

        
    }


    


}
