using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour {

    public Room RoomA;
    public Room RoomB;
    public List<GameObject> nodes = new List<GameObject>();

    private void Awake()
    {
        nodes.Add(this.gameObject);
    }

}
