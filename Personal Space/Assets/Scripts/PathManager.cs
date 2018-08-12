using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour {

    public Room[] Rooms;
    public RoomTransition[] Transitions;


    public static PathManager manager;
    private void Awake()
    {
        manager = this;
    }

    public Room GetRoom(string name)
    {
        foreach(Room r in Rooms)
        {
            if (r.RoomName == name) return r;
        }
        return null;
    }

    public RoomTransition GetRoomTransition(Room A, Room B)
    {
        foreach (RoomTransition rt in Transitions)
        {
            if (rt.RoomA == A && rt.RoomB == B) return rt;
            if (rt.RoomA == B && rt.RoomB == A)
            {
                RoomTransition r = new RoomTransition();
                r.RoomA = B;
                r.RoomB = A;
                List<GameObject> newnodeorder = new List<GameObject>();
                foreach (GameObject n in rt.nodes) newnodeorder.Add(n);
                newnodeorder.Reverse();
                r.nodes = newnodeorder;
                return r;
            }
        }
        return null;
    }
    public RoomTransition GetRoomTransition(string nameA, string nameB)
    {
        Room A = GetRoom(nameA);
        Room B = GetRoom(nameB);
        return GetRoomTransition(A, B);        
    }

    public List<RoomTransition> GetPathFromAToB(Room A, Room B)
    {
        List<RoomTransition> r = FindPath(A, B, new List<RoomTransition>());
        string a = "";
        if (r != null)
        {
            foreach (RoomTransition rt in r)
            {
                a += rt.RoomA + "<->" + rt.RoomB + "|";
            }
            Debug.Log(a);
        }
        return r;

    }
    public List<RoomTransition> GetPathFromAToB(string nameA, string nameB)
    {
        Room A = GetRoom(nameA);
        Room B = GetRoom(nameB);
        return GetPathFromAToB(A, B);
        
    }


    private List<RoomTransition> FindPath( Room A, Room B, List<RoomTransition> Pathsofar)
    {
        RoomTransition trivial = GetRoomTransition(A.RoomName, B.RoomName);
        if (trivial != null)
        {
            Pathsofar.Add(trivial);
            return Pathsofar;
        }

        foreach(RoomTransition rt in Transitions)
        {
            if (rt.RoomA == A && !Pathsofar.Contains(rt)) {
                List<RoomTransition> newsofar = Pathsofar;
                newsofar.Add(rt);
                List<RoomTransition> t = FindPath(rt.RoomB, B, newsofar);
                if (t != null) return t;
            }
            if(rt.RoomB == A && !Pathsofar.Contains(rt)) {
                List<RoomTransition> newsofar = Pathsofar;
                newsofar.Add(rt);
                List<RoomTransition> t = FindPath(rt.RoomA, B, newsofar);
                if (t != null) return t;
            }
        }
        return null;
    }

    public Room GetMyRoom(Vector2 pos)
    {
        foreach(Room r in Rooms){
            if (r.Contains(pos)) return r;
        }
        return null;
    }
}
