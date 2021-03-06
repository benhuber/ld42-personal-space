﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogbox : MonoBehaviour {

    public static Dialogbox Dialogsystem;

    Dialogstate[] dialogstates;
    Dialogstate currentState;
    Dialogtransition[] dialogtransitions;

    public TextMeshProUGUI Title;
    public TextMeshProUGUI Message;
    public TextMeshProUGUI B_opA;
    public TextMeshProUGUI B_opB;

    public Mask mask;
    public Image Imask;

    public Image Portrait;
    public Sprite defaultimg;

    // Use this for initialization
    void Awake() {
        Dialogsystem = this;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
  

    public void StartDialog(Dialogstate[] ds, Dialogtransition[] dt)
    {
        dialogstates = ds;
        dialogtransitions = dt;
        Time.timeScale = 0f;
        currentState = dialogstates[0];
        DisplayDialog();
    }

    public void DisplayDialog()
    {
        if (currentState.Avatar != null) Portrait.sprite = currentState.Avatar;
        else Portrait.sprite = defaultimg;
        Title.text = currentState.Title;
        Message.text = currentState.Message;
        B_opA.text = currentState.optionA;
        B_opB.text = currentState.optionB;
        B_opA.transform.parent.gameObject.SetActive(currentState.optionA != "");
        B_opB.transform.parent.gameObject.SetActive(currentState.optionB != "");
        mask.enabled=(currentState.mask);
        Imask.enabled = (currentState.mask);

    }

    public void ChoseOption(bool opA)
    {


        if (opA) PlayerStatus.thePlayer.ChangeAnnoyance(currentState.oA_changeval);
        else PlayerStatus.thePlayer.ChangeAnnoyance(currentState.oB_changeval);

        foreach ( Dialogtransition t in dialogtransitions)
        {
            if (currentState.Equals(t.origial))
            {
                if (opA)
                {
                    if (t.oA_followup.callback != null) t.oA_followup.callback();
                    if (t.oA_followup.end) EndDialog();
                    else currentState = t.oA_followup;
                    DisplayDialog();
                }
                else
                {
                    if (t.oB_followup.callback != null) t.oB_followup.callback();
                    if (t.oB_followup.end) EndDialog();
                    else currentState = t.oB_followup;
                    DisplayDialog();
                }
                return;
            }
        }

    }

    public void EndDialog()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);

    }

    public class Dialogstate{
        public string Title;
        public string Message;
        public string optionA;
        public string optionB;
        public float oA_changeval;
        public float oB_changeval;
        public bool end;
        public Sprite Avatar;
        public delegate void callbacktype();
        public callbacktype callback;
        public bool mask = false;
    }

    public struct Dialogtransition
    {
        public Dialogstate origial;
        public Dialogstate oA_followup;
        public Dialogstate oB_followup;
    }
}
