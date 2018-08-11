using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonA : Talktome
{
    public Sprite Portrait;

    void Start()
    {
        Dialog = Dialogbox.Dialogsystem.gameObject;

        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true };
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Random Asshat", Message = "Why don't you smile some more`?", optionA = "*smile*", optionB = "Fuck off!", oA_changeval = 100f, oB_changeval = 0f, end = false };
        Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = Dend };
        ds = new Dialogbox.Dialogstate[2];
        ds[0] = ds0;
        ds[1] = Dend;
        dt = new Dialogbox.Dialogtransition[1];
        dt[0] = dt0;
    }


}
