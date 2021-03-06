using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointOfInterest : MonoBehaviour {
    public static List<PointOfInterest> poi = new List<PointOfInterest>();
    public static List<PointOfInterest> GetPoIWithTag(string tag) {
        return poi.Where(x => x.tags.Contains(tag)).ToList();
    }
    public static PointOfInterest RandomPoI() {
        return poi[Random.Range(0,poi.Count)];
    }

    public List<string> tags = new List<string>();
    [HideInInspector]
    public Room myRoom;

    private void Start() {
        myRoom = PathManager.manager.GetMyRoom(this.transform.position);
        if (myRoom == null) {
            Debug.LogError("Point of Interest outside of all rooms!");
        }
    }

    private void Awake() {
        poi.Add(this);
    }
}