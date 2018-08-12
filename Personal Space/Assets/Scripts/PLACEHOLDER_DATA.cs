using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLACEHOLDER_DATA : MonoBehaviour {

    public enum Endings {Default, AllFriendsDone, Stay, Stress, Time}
    public Endings ending;
    public static PLACEHOLDER_DATA data;

	// Use this for initialization
	void Awake () {
        data = this;
	}
}
