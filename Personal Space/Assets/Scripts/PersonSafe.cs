using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSafe : Talktome {

    // DIALOG
    public Sprite Portrait;

    PLACEHOLDER_DATA.Endings oldending;

    public static PersonSafe safe;

    void Awake()
    {
        safe = this;
        name = "Darya";
    }

    new void Start()
    {
        base.Start();
        //DIALOG
        Dialog = Dialogbox.Dialogsystem.gameObject;
        NextBehavior();
    }

    public void NextBehavior()
    {
        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true };
        if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.AllFriendsDone)
        {
            Dialogbox.Dialogstate Dend_fin = new Dialogbox.Dialogstate { end = true, callback = EndingManager.em.PlayEnd };
            Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Darya", Message = "What have you been up to?\n\t Oh, I'm glad I got to catch up with all the people I wanted to meet here.\nThat sounds good. Shall we leave though?", optionA = " Yeah, let's go before we run out of space here.", optionB = "...", oA_changeval = 0f, oB_changeval = 0f, end = false };
            Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend_fin, oB_followup = Dend };
            ds = new Dialogbox.Dialogstate[3];
            ds[0] = ds0;
            ds[1] = Dend_fin;
            ds[2] = Dend;
            dt = new Dialogbox.Dialogtransition[1];
            dt[0] = dt0;
        }
        else if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.Stress)
        {
            Dialogbox.Dialogstate Dend_fin = new Dialogbox.Dialogstate { end = true, callback = EndingManager.em.PlayEnd };
            Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Darya", Message = "Hey! I'm here. I'm here. Come on, let's get out of here.", optionA = " Thanks", optionB = "", oA_changeval = 0f, oB_changeval = 0f, end = false };
            Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend_fin, oB_followup = Dend };
            ds = new Dialogbox.Dialogstate[3];
            ds[0] = ds0;
            ds[1] = Dend_fin;
            ds[2] = Dend;
            dt = new Dialogbox.Dialogtransition[1];
            dt[0] = dt0;
        }
        else if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.Time)
        {
            Dialogbox.Dialogstate Dend_fin = new Dialogbox.Dialogstate { end = true, callback = EndingManager.em.PlayEnd };
            Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Darya", Message = "Hey, it's getting too crowded for me in here. Would you join me, if I leave?", optionA = " Of course. You watch my back, I watch yours.", optionB = "...", oA_changeval = 0f, oB_changeval = 0f, end = false };
            Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend_fin, oB_followup = Dend };
            ds = new Dialogbox.Dialogstate[3];
            ds[0] = ds0;
            ds[1] = Dend_fin;
            ds[2] = Dend;
            dt = new Dialogbox.Dialogtransition[1];
            dt[0] = dt0;
        }
        else if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.Default)
        {
            Dialogbox.Dialogstate Dend_fin = new Dialogbox.Dialogstate { end = true, callback = EndingManager.em.PlayEnd };
            Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Darya", Message = "So, how do you like the party?", optionA = "I don't know. I didn't feel like coming here in the first place. It's so crowded. Let's leave!", optionB = "The party might be fun after all. I think I want to hang out a bit longer.\n ", oA_changeval = 0f, oB_changeval = 0f, end = false };
            Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend_fin, oB_followup = Dend };
            ds = new Dialogbox.Dialogstate[3];
            ds[0] = ds0;
            ds[1] = Dend_fin;
            ds[2] = Dend;
            dt = new Dialogbox.Dialogtransition[1];
            dt[0] = dt0;
        }
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        if (oldending != PLACEHOLDER_DATA.data.ending) NextBehavior();
        oldending = PLACEHOLDER_DATA.data.ending;
    }

    public void TriggerEnd()
    {
        NextBehavior();
        Dialog.SetActive(true);
        Dialogbox.Dialogsystem.StartDialog(ds, dt);
    }


}
