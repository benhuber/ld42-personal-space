﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFridge : Talktome {

    public Sprite Portrait;
    PLACEHOLDER_DATA.Endings oldending;

    bool talked;


    new void Start()
    {
        base.Start();
        //DIALOG
        Dialog = Dialogbox.Dialogsystem.gameObject;


        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true, callback = talkedToFriend };
        Dialogbox.Dialogstate Dend2 = new Dialogbox.Dialogstate { end = true};
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate
        {
            Avatar = Portrait,
            Title = "Looking into the Fridge",
            Message = "There is a delicious cake in the fridge. It looks like doouble-fudge-hazelnut with strawberry-cream filling.",
            optionA = "*take the cake*",
            optionB = "*close the fridge*",
            oA_changeval = -10f,
            oB_changeval = 0f,
            end = false,
        };
        Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = Dend2 };
        ds = new Dialogbox.Dialogstate[2];
        ds[0] = ds0;
        ds[1] = Dend;
        dt = new Dialogbox.Dialogtransition[1];
        dt[0] = dt0;

    }


    void talkedToFriend()
    {
        if (!talked)
        {
            talked = true;
            PlayerStatus.thePlayer.foundCake = true;
            done = true;
            resets = false;
        }
    }
}
