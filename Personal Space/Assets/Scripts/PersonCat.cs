using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCat : Talktome{

    public Sprite Portrait;
    PLACEHOLDER_DATA.Endings oldending;

    bool talked;
    Annoying annoy;


    new void Start()
    {
        base.Start();
        //DIALOG
        Dialog = Dialogbox.Dialogsystem.gameObject;


        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true, callback = talkedToFriend };
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Miss Mr. Mew",
            Message = "Mr. Mew is sitting on the Couch, observing the Crowd.",
            optionA = "*Pet Miss Mr. Mew*", optionB = "",
            oA_changeval = -10f, oB_changeval = 20f, end = false, callback = talkedToFriend };
        Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = Dend };
        ds = new Dialogbox.Dialogstate[2];
        ds[0] = ds0;
        ds[1] = Dend;
        dt = new Dialogbox.Dialogtransition[1];
        dt[0] = dt0;

        annoy = GetComponent<Annoying>();
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
