using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public string RoomName;
    public List<GameObject> nodes = new List<GameObject>();
    Rect area;

    Vector2 debugpos;

    private void Awake()
    {
        area = new Rect(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.localScale.x, transform.localScale.y));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(area.position, area.position+new Vector2(area.width, 0));
        Gizmos.DrawLine(area.position, area.position+new Vector2(0, area.height));
        Gizmos.DrawLine(area.position + new Vector2(area.width, area.height), area.position + new Vector2(area.width, 0));
        Gizmos.DrawLine(area.position + new Vector2(area.width, area.height), area.position + new Vector2(0, area.height));
    }

    public bool Contains (Vector2 pos)
    {
        debugpos = pos;
        return (area.Contains(pos));
    }

}
