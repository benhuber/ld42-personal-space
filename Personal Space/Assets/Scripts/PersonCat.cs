using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCat : Talktome{

    public Sprite Portrait;
    public Transform door;
    GameObject catEvent;
    PLACEHOLDER_DATA.Endings oldending;
    CrowdMovement movement;


    bool talked;
    bool atDoor = false;


    new void Start()
    {
        base.Start();
        catEvent = transform.Find("Cat").gameObject;
        movement = GetComponent<CrowdMovement>();
        movement.enabled = false;

        //DIALOG
        Dialog = Dialogbox.Dialogsystem.gameObject;
        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true, callback = talkedToFriend };
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Miss Mr. Mew",
            Message = "Miss Mr. Mew is sitting on the Couch, observing the Crowd.",
            optionA = "*Pet Miss Mr. Mew*", optionB = "",
            oA_changeval = -10f, oB_changeval = 20f, end = false, callback = talkedToFriend };
        Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = Dend };
        ds = new Dialogbox.Dialogstate[2];
        ds[0] = ds0;
        ds[1] = Dend;
        dt = new Dialogbox.Dialogtransition[1];
        dt[0] = dt0;
        resets = false;
    }

    new private void FixedUpdate() {
        base.FixedUpdate();

        if (myTime > 20f && !atDoor) {
            if ((transform.position - door.position).magnitude < 1.5f) {
                movement.enabled = false;
                atDoor = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2();
                RegisterNewDialog();
            } else if ((transform.position - door.position).magnitude < 10f) {
                movement.walkSpeed = 3f;
            } else {
                if (!movement.enabled) {
                    done = true;
                    InteractionManager.RemoveInteraction(this.transform);
                    catEvent.SetActive(false);
                    movement.enabled = true;
                    movement.ForceWalkTo(door.position);
                }
            }
        }
    }

    void RegisterNewDialog() {
        Dialogbox.Dialogstate Dend = new Dialogbox.Dialogstate { end = true, callback = Vanish };
        Dialogbox.Dialogstate DendIgnore = new Dialogbox.Dialogstate { end = true, callback = null };
        Dialogbox.Dialogstate ds0 = new Dialogbox.Dialogstate { Avatar = Portrait, Title = "Miss Mr. Mew",
            Message = "Miss Mr. Mew seems annoyed with the party. They scratch at the door.",
            optionA = "*open the door for Mew*", optionB = "*ignore Mew*",
            oA_changeval = -10f, oB_changeval = 20f, end = false};

        Dialogbox.Dialogtransition dt0 = new Dialogbox.Dialogtransition { origial = ds0, oA_followup = Dend, oB_followup = DendIgnore };
        ds = new Dialogbox.Dialogstate[]{ds0, Dend, DendIgnore};
        dt = new Dialogbox.Dialogtransition[]{dt0};
        done = false;
        resets = true;
        resettime = 0f;
    }

    void Vanish() {
        talkedToFriend();
        MessageHandler.me.EnqueMessage("Achievement 'Cat Person': Miss Mr. Mew likes you.");
        var data = FindObjectOfType<PersistentDataComponent>();
        data.CompleteAnAchievment(PersistentDataComponent.EAchievement.EAchievement_CatPerson);
        Destroy(this.gameObject); 
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
