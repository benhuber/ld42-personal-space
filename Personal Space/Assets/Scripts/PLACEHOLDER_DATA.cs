using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLACEHOLDER_DATA : MonoBehaviour {

    public enum Endings {Default, AllFriendsDone, Stay, Stress, Time}
    public Endings ending;
    public static PLACEHOLDER_DATA data;
    public int numberOfFriendsSpokenTo=0;

    //archievements
    public bool NightOwl = false;
    public bool FreeLikeABird = false;
    public bool ThereWillBeCake = false;
    public bool Bookclub = false;
    public bool Prosocial = false;
    public bool FancyDancer = false;
 

	// Use this for initialization
	void Awake () {
        data = this;
	}
}
