using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour {

    public Room RoomA;
    public Room RoomB;
    [HideInInspector] public List<Vector3> nodes = new List<Vector3>();

    private void Awake() {
        nodes.Add(this.transform.position);
        for (int i=0; i<transform.childCount; ++i) {
            nodes.Add(transform.GetChild(i).position);
        }
    }

}
