using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfindingagent : MonoBehaviour {

    public bool lookfor;
    public string Roomname;

    PathManager pm;

    private void Start()
    {
        pm = PathManager.manager;
    }

    // Update is called once per frame
    void Update () {
        if (lookfor) Findpathto();
        

	}

    public void Findpathto()
    {
        lookfor = false;
        Debug.Log("my room: " + (pm.GetMyRoom(transform.position)!=null));
        Debug.Log("room by name: " + (pm.GetRoom(Roomname) != null));


        pm.GetPathFromAToB(pm.GetMyRoom(transform.position), pm.GetRoom(Roomname));
    }
}
