using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathManager : MonoBehaviour {

    public Room[] Rooms;
    public RoomTransition[] transitions;


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

    public List<Vector3> GetRoomTransition(Room A, Room B)
    {
        foreach (RoomTransition rt in transitions)
        {
            if (rt.RoomA == A && rt.RoomB == B) {
                return TransitionToPositions(rt, false);
            } else if (rt.RoomA == B && rt.RoomB == A) {
                return TransitionToPositions(rt, true);
            }
        }
        return null;
    }
    public List<Vector3> GetRoomTransition(string nameA, string nameB)
    {
        Room A = GetRoom(nameA);
        Room B = GetRoom(nameB);
        return GetRoomTransition(A, B);        
    }

    public List<Vector3> TransitionToPositions(RoomTransition t, bool inverted) {
        if (inverted) {
            var result = new List<Vector3>();
            foreach (var n in t.nodes) {
                result.Add(n);
            }
            result.Reverse();
            return result;
        } else {
            return t.nodes;
        }
    }

    public List<Vector3> GetPathFromAToB(Room A, Room B)
    {
        int tCount = transitions.Length;
        for (int i=0; i<tCount-1; ++i) {
            var tmp = transitions[i];
            int idx = Random.Range(i+1,tCount);
            transitions[i] = transitions[idx];
            transitions[idx] = tmp;
        }
        return FindPath(A, B, new List<Vector3>(), new List<Room>(){A});
    }
    public List<Vector3> GetPathFromAToB(string nameA, string nameB)
    {
        Room A = GetRoom(nameA);
        Room B = GetRoom(nameB);
        return GetPathFromAToB(A, B);
    }


    private List<Vector3> FindPath( Room A, Room B, List<Vector3> pathSoFar, List<Room> roomsSoFar)
    {
        List<Vector3> trivial = GetRoomTransition(A.RoomName, B.RoomName);
        if (trivial != null)
        {
            pathSoFar.AddRange(trivial);
            return pathSoFar;
        }

        foreach(RoomTransition rt in transitions)
        {
            if (rt.RoomA == A && !roomsSoFar.Contains(rt.RoomB)) {
                int oldPathLength = pathSoFar.Count;
                var newPoints = TransitionToPositions(rt, false);
                pathSoFar.AddRange(newPoints);
                roomsSoFar.Add(rt.RoomB);
                List<Vector3> t = FindPath(rt.RoomB, B, pathSoFar, roomsSoFar);
                if (t != null) return t;
                roomsSoFar.Remove(rt.RoomB);
                pathSoFar.RemoveRange(oldPathLength, newPoints.Count);
            }
            if(rt.RoomB == A && !roomsSoFar.Contains(rt.RoomA)) {
                int oldPathLength = pathSoFar.Count;
                var newPoints = TransitionToPositions(rt, true);
                pathSoFar.AddRange(newPoints);
                roomsSoFar.Add(rt.RoomA);
                List<Vector3> t = FindPath(rt.RoomA, B, pathSoFar, roomsSoFar);
                if (t != null) return t;
                roomsSoFar.Remove(rt.RoomB);
                pathSoFar.RemoveRange(oldPathLength, newPoints.Count);
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
