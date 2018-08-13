using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFriend1 : Talktome {

    //BC
    BackgroundChatter bc;
    Color col;
    public AudioClip[] friendclips;

    // DIALOG
    public Sprite Portrait;
    PLACEHOLDER_DATA.Endings oldending;

    //PATHFINDING
    public Transform[] PathA;
    public Transform[] PathB;
    PersonMotor pm;

    public float timepertalk = 60f;
    float time = 0f;
    bool cakefalg = false;
    int behavior = 0;

    Annoying annoy;

    bool talked = false;

    private void Awake() {
        name = "Rosie";
    }

    new void Start()
    {
        base.Start();
        //BC
        bc = GetComponent<BackgroundChatter>();
        col = Color.blue;
        //DIALOG
        Dialog = Dialogbox.Dialogsystem.gameObject;
        

        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true, callback = talkedToFriend };
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Rosie", Message = "You're here!! I'm so happy you made it. Have you seen Mew? Can you maybe check, if they are alright. They like you so much. ", optionA = "Sure, I wanted to find Miss Mister Mew anyway.", optionB = "Uhm, they were stressing out a bit, so I let them out of the flat.", oA_changeval = -10f, oB_changeval = 20f, end = false, callback = talkedToFriend };
        Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = Dend };
        ds = new Dialogbox.Dialogstate[2];
        ds[0] = ds0;
        ds[1] = Dend;
        dt = new Dialogbox.Dialogtransition[1];
        dt[0] = dt0;

        time = timepertalk;
        annoy = GetComponent<Annoying>();

        //PATHFINDING
        pm = GetComponent<PersonMotor>();


        NextBehavior();
    }

    public void NextBehavior()
    {
        
        if (myTime < 135 && !(behavior == 1)) //BEHAVIOR 1
        {
            behavior= 1;
            bc.Display("tense debate about the police", col, 3f, friendclips[0], NextBehavior);
            annoy.value = .6f;
            return;
        }
        if (myTime < 180 && !(behavior == 2)) //BEHAVIOR 2
        {
            behavior = 2;
            pm.StartWalking(PathA, NextBehavior);
            annoy.value = 0f;
            return;
        }
        if (myTime < 285 && !(behavior == 3)) //BEHAVIOR 3
        {
            behavior = 3;
            bc.Display("cheerfull exchange about the cake", col, 3f, friendclips[1], NextBehavior);
            annoy.value = 0f;
            return;
        }
        if (myTime >= 285 && !(behavior == 4)) //BEHAVIOR 4
        {
            behavior = 4;
            pm.StartWalking(PathB, NextBehavior);
            annoy.value = 0f;
            return;
        }


    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        if (PlayerStatus.thePlayer.foundCake)
        {
            Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true, callback = talkedToFriend };
            Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Rosie", Message = "\t Happy birthday, Rosie! Have some cake.\n Ohhh, you found the cake! This is so awesome. Thank you, my wonderful friend!", optionA = "*Have some Cake together*", optionB = "", oA_changeval = -50f, oB_changeval = 0f, end = false };
            Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = Dend };
            ds = new Dialogbox.Dialogstate[2];
            ds[0] = ds0;
            ds[1] = Dend;
            dt = new Dialogbox.Dialogtransition[1];
            dt[0] = dt0;
            cakefalg = true;
        }

    }

    void talkedToFriend()
    {
        if (!talked)
        {
            talked = true;
            PLACEHOLDER_DATA.data.numberOfFriendsSpokenTo++;
            MessageHandler.me.EnqueMessage("Task accomplished: " + PLACEHOLDER_DATA.data.numberOfFriendsSpokenTo + "/3 frieds met");
        }
    }


}
