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

    public float timepertalk = 60f;
    float time = 0f;
    bool cakefalg = false;
    int nthDialog = 0;

    Annoying annoy;

    void Start()
    {
        //BC
        bc = GetComponent<BackgroundChatter>();
        col = Color.blue;
        //DIALOG
        Dialog = Dialogbox.Dialogsystem.gameObject;
        NextBehavior();

        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true };
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Rosie", Message = "You're here!! I'm so happy you made it. Have you seen Mew? Can you maybe check, if they are alright. They like you so much. ", optionA = "Sure, I wanted to find Miss Mister Mew anyway.", optionB = "Uhm, they were stressing out a bit, so I let them out of the flat.", oA_changeval = -10f, oB_changeval = 20f, end = false };
        Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = Dend };
        ds = new Dialogbox.Dialogstate[2];
        ds[0] = ds0;
        ds[1] = Dend;
        dt = new Dialogbox.Dialogtransition[1];
        dt[0] = dt0;

        time = timepertalk;
        annoy = GetComponent<Annoying>();
    }

    public void NextBehavior()
    {
        if (PlayerStatus.thePlayer.foundCake)
        {
            Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true };
            Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Rosie", Message = "\t Happy birthday, Rosie! Have some cake.\n Ohhh, you found the cake! This is so awesome. Thank you, my wonderful friend!", optionA = "*Have some Cake together*", optionB = "", oA_changeval = -50f, oB_changeval = 0f, end = false };
            Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = Dend };
            ds = new Dialogbox.Dialogstate[2];
            ds[0] = ds0;
            ds[1] = Dend;
            dt = new Dialogbox.Dialogtransition[1];
            dt[0] = dt0;
            cakefalg = true;
        }
        if (nthDialog == 1)
        {
            bc.Display("tense debate about policebrutality", col, 3f, friendclips[nthDialog - 1]);
            annoy.value = .6f;
        }
        if (nthDialog == 2)
        {
            bc.Display("pleasant debate about horror movies", col, 3f, friendclips[nthDialog - 1]);
            annoy.value = 0f;
        }
        if (nthDialog == 3)
        {
            bc.Display("boring debate about school", col, 3f, friendclips[nthDialog - 1]);
            annoy.value = .2f;
        }
        if (nthDialog == 4)
        {
            bc.Display("scientific debate about pokemon", col, 3f, friendclips[nthDialog - 1]);
            annoy.value = 0f;
        }
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        time += Time.fixedDeltaTime;
        if (time > timepertalk) {
            nthDialog++;
            NextBehavior();
            time = 0;
        }
        if (PlayerStatus.thePlayer.foundCake &&!cakefalg) NextBehavior();


    }


}
