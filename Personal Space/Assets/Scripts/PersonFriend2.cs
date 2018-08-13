using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFriend2 : Talktome{

    //BC
    BackgroundChatter bc;
    Color col;
    public AudioClip[] friendclips;

    // DIALOG
    public Sprite Portrait;
    PLACEHOLDER_DATA.Endings oldending;

    //PATHFINDING
    public Transform[] PathA;
    PersonMotor pm;

    public float timepertalk = 60f;
    float time = 0f;
    bool cakefalg = false;
    int behavior = 0;

    Annoying annoy;

    bool talked = false;
    bool changedDialog = false;

    private void Awake()
    {
        name = "Jill";
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
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Jill", Message = "Did you see that? One of Rosie's housemates is actually keeping a bird in a cage. It didn't look very happy. And it has to share the flat with a cat, too.", optionA = "Yeah, that seems stressful for the bird. They probably keep the door to that room shut, normally.", optionB = "", oA_changeval = 0, oB_changeval = 0, end = false };
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
            behavior = 1;
            bc.Display("enthusiastic chat \nabout cake", col, 3f, friendclips[0], NextBehavior);
            annoy.value = 0f;
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
            bc.Display("debate about animal rights", col, 3f, friendclips[1], NextBehavior);
            annoy.value = 0f;
            return;
        }
    }

    new void FixedUpdate()
    {
        if (PlayerStatus.thePlayer.freedBird && !changedDialog)
        {
            changedDialog = true;
            Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true, callback = talkedToFriend };
            Dialogbox.Dialogstate DendP = new Dialogbox.Dialogstate { end = true, callback = talkedToFriend };
            Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Jill", Message = "Did you see that? One of Rosie's housemates is actually keeping a bird in a cage. It didn't look very happy. And it has to share the flat with a cat, too.", optionA = "Yeah, that seems stressful for the bird. They probably keep the door to that room shut, normally.", optionB = "Actually, I just let the canary out of the window.", oA_changeval = 0, oB_changeval = 0, end = false };
            Dialogbox.Dialogstate ds1 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Jill",
                Message = "Are you serious? You are my hero!",
                optionB = "I just hope he's gonna be alright out there.",
                optionA = "Thanks, I'm okay. Bit annoyed about the bird maybe. But there is a person talking non-stop about cake and pizza at this party. That's kinda fun",
                oA_changeval = 0, oB_changeval = 0, end = false };

            Dialogbox.Dialogstate ds3 = new Dialogbox.Dialogstate
            {
                Avatar = Portrait,
                Title = "Jill",
                Message = "Thanks, I'm okay. Bit annoyed about the bird maybe. But there is a person talking non-stop about cake and pizza at this party. That's kinda fun.",
                optionA = "Haha, maybe I should find them and exchange recipes.",
                optionB = "Yes, and did you see the person with the kangaroo costume?",
                oA_changeval = 0,
                oB_changeval = 0,
                end = false
            };
            Dialogbox.Dialogstate ds4 = new Dialogbox.Dialogstate
            {
                Avatar = Portrait,
                Title = "Jill",
                Message = "Quite impressive that they haven't melted in here, yet. The last time I spotted them, they were raving on the dance floor.",
                optionA = "When I last saw them they were snoring in a corner.",
                optionB = "Wow, I love it, when ppl dress up and stay at ease with themselves.",
                oA_changeval = 0,
                oB_changeval = 0,
                end = false
            };
            Dialogbox.Dialogstate ds5 = new Dialogbox.Dialogstate
            {
                Avatar = Portrait,
                Title = "Jill",
                Message = "They might have a more sinister reason to be dressed up.",
                optionB = "That sounds daunting. But I still have some stuff to do here. Cya.",
                optionA = "Sinister? Come on! Now you are just pulling my leg.",
                oA_changeval = 0,
                oB_changeval = 0,
                end = false
            };
            Dialogbox.Dialogstate ds6 = new Dialogbox.Dialogstate
            {
                Avatar = Portrait,
                Title = "Jill",
                Message = "Actually, I discovered earlier tonight that the person in the kangaroo costume is Rosie's cousin from Tanzania. One of Rosie's friends invited her as a surprise. But don't tell anyone.",
                optionA = "TThe secret is safe with me.",
                optionB = "",
                oA_changeval = 0,
                oB_changeval = 0,
                end = false
            };

            Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = ds1 };
            Dialogbox.Dialogtransition dt1 = new Dialogbox.Dialogtransition { origial = ds1, oA_followup = ds3, oB_followup = Dend };
            Dialogbox.Dialogtransition dt2 = new Dialogbox.Dialogtransition { origial = ds3, oA_followup = Dend, oB_followup = ds4 };
            Dialogbox.Dialogtransition dt4 = new Dialogbox.Dialogtransition { origial = ds4, oA_followup = Dend, oB_followup = ds5 };
            Dialogbox.Dialogtransition dt5 = new Dialogbox.Dialogtransition { origial = ds5, oA_followup = ds6, oB_followup = Dend };
            Dialogbox.Dialogtransition dt6 = new Dialogbox.Dialogtransition { origial = ds6, oA_followup = Dend, oB_followup = Dend };


            ds = new Dialogbox.Dialogstate[2];
            ds[0] = ds0;
            ds[1] = Dend;
            dt = new Dialogbox.Dialogtransition[1];
            dt[0] = dt0;
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

    void PersistenceArchieve()
    {

    }


}
